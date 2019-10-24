﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MYGIS
{
    public class GISVertex //节点类拥有算距离的方法 
    {
        public double x;
        public double y;

        public GISVertex(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        //从文件中读取结点实例
        public GISVertex(BinaryReader br)
        {
            x = br.ReadDouble();
            y = br.ReadDouble();
        }

        public double Distance(GISVertex anothervertex)
        {
            return Math.Sqrt((x - anothervertex.x) * (x - anothervertex.x)
                - (y - anothervertex.y) * (y - anothervertex.y));
        }
        public void CopyFrom(GISVertex v)
        {
            x = v.x;
            y = v.y;
        }

        public void WriteVertex(BinaryWriter bw)
        {
            bw.Write(x);
            bw.Write(y);
        }

        public bool IsSame(GISVertex vertex)
        {
            return x == vertex.x && y == vertex.y;
        }

    }

    public class GISPoint:GISSpatial
    {
        //public GISVertex Location; **previous code
        //public string Attribute;

        public GISPoint(GISVertex onevertex)
        {
            centroid = onevertex;
            extent = new GISExtent(onevertex, onevertex);
        }
        public override void draw(Graphics graphics, GISView view, bool Selected)
        {
            Point screenpoint = view.ToScreenPoint(centroid);
            graphics.FillEllipse(new SolidBrush(Selected ? GISConst.SelectedPointColor : GISConst.PointColor),
                new Rectangle(screenpoint.X - GISConst.PointSize, screenpoint.Y - GISConst.PointSize,
                GISConst.PointSize * 2, GISConst.PointSize * 2));
        }
        public double Distance(GISVertex anothervertex)
        {
            return centroid.Distance(anothervertex);
        }

    }
    public class GISLine : GISSpatial
    {
        public List<GISVertex> Vertexes;
        public double length;
        public GISLine(List<GISVertex> _vertexes)
        {
            Vertexes = _vertexes;
            centroid = GISTools.CalculateCentroid(_vertexes);
            extent = GISTools.CalculateExtent(_vertexes);
            length = GISTools.CalculateLength(_vertexes);
        }
        public override void draw(Graphics graphics, GISView view, bool Selected)
        {
            Point[] points = GISTools.GetScreenPoints(Vertexes, view);
            graphics.DrawLines(new Pen(Selected ? GISConst.SelectedLineColor : GISConst.LineColor,
                GISConst.LineWidth), points);
        }
        public GISVertex FromNode()
        {
            return Vertexes[0];
        }
        public GISVertex ToNode()
        {
            return Vertexes[Vertexes.Count - 1];
        }

        //点到线实体的距离计算 //可以算出最短的距离 但无法获知最近的位置
        public double Distance(GISVertex vertex)
        {
            double distance = Double.MaxValue;
            for (int i = 0; i < Vertexes.Count - 1; i++)
            {
                distance = Math.Min(GISTools.PointToSegment
                    (Vertexes[i], Vertexes[i + 1], vertex), distance);
            }
            return distance;
        }
    }
    public class GISPolygon:GISSpatial
    {
        public List<GISVertex> Vertexes;
        public double Area;
        public GISPolygon(List<GISVertex> _vertexes)
        {
            Vertexes = _vertexes;
            centroid = GISTools.CalculateCentroid(_vertexes);
            extent = GISTools.CalculateExtent(_vertexes);
            Area = GISTools.CalculateArea(_vertexes);
        }
        public override void draw(Graphics graphics, GISView view, bool Selected)
        {
            Point[] points = GISTools.GetScreenPoints(Vertexes, view);
            graphics.FillPolygon(new SolidBrush(Selected ? 
                GISConst.SelectedPolygonFillColor : GISConst.PolygonFillColor), points);
            graphics.DrawPolygon(new Pen(GISConst.PolygonBoundaryColor, GISConst.PolygonBoundaryWidth), points);
        }

        public bool include(GISVertex vertex)
        {
            int count = 0;
            for (int i = 0; i < Vertexes.Count; i++)
            {
                //满足点在线上直接返回false
                if (Vertexes[i].IsSame(vertex)) return false;
                //由序号i和下一个点构成一条线段 可以算出下一个点next的公式
                int next = (i + 1) % Vertexes.Count;
                //确定线段的坐标极值
                double minx = Math.Min(Vertexes[i].x, Vertexes[next].x);
                double miny = Math.Min(Vertexes[i].y, Vertexes[next].y);
                double maxx = Math.Max(Vertexes[i].x, Vertexes[next].x);
                double maxy = Math.Max(Vertexes[i].y, Vertexes[next].y);
                //如果线段平行于射线
                if (miny == maxy)
                {
                    //满足点在线上直接返回false
                    if (miny == vertex.y && vertex.x >= minx && vertex.x <= maxx) return false;
                    //满足射线与线段平行无焦点且不再线段上
                    else continue;
                }
                //点和线段都在坐标极值之外则不可能有交点
                if (vertex.x > maxx || vertex.y > maxy || vertex.y < miny) continue;
                //计算交点横坐标 纵坐标显然就为y
                double x0 = Vertexes[i].x + (vertex.y - Vertexes[i].y) * (Vertexes[next].x - Vertexes[i].x) /
                    (Vertexes[next].y - Vertexes[i].y);
                //交点在射线反方向按照没有交点计算
                if (x0 < vertex.x) continue;
                //交点是vertex且在线段上，按照不包括处理(前面已经判断，但是此处仍要排除此种情况)
                if (x0 == vertex.x) return false;
                //射线穿过的下断点不计数
                if (vertex.y == miny) continue;
                //其他情况交点数+1
                count++;
            }
            return count % 2 != 0;
        }
    }


    public class GISFeature
    {
        public GISSpatial spatialpart;
        public GISAttribute attributepart; //空间与属性信息
        public bool Selected = false;//记录实例的选择状态
        public int ID;//唯一标识符 在同一个图层中把多个GISFeature区别开来

        public GISFeature(GISSpatial spatial, GISAttribute attribute) //构造函数传入空间与属性信息
        {
            spatialpart = spatial;
            attributepart = attribute;
        }
        public void draw(Graphics graphics,GISView view, bool DrawAttributeOrNot, int index)
            //画空间与属性信息
        {
            spatialpart.draw(graphics,view,Selected);
            if (DrawAttributeOrNot)
                attributepart.draw(graphics,view, spatialpart.centroid, index);
            //此处引用了attributed的方法
        }
        public object getAttribute(int index)//获取属性值
        {
            return attributepart.GetValue(index);
        }
    }

    public class GISAttribute
    {
        public ArrayList values = new ArrayList();

        public void AddValue(object o)
        {
            values.Add(o);
        }

        public object GetValue(int index)
        {
            return values[index];
        }

        public void draw(Graphics graphics,GISView view, GISVertex location, int index)
        {
            Point screenpoint = view.ToScreenPoint(location);//转换坐标到屏幕点
            graphics.DrawString(values[index].ToString(), 
                new Font("宋体", 20),
                new SolidBrush(Color.Green), 
                new PointF(screenpoint.X, screenpoint.Y));
        }

        public int ValueCount()
        {
            return values.Count;
        }
    }

    public abstract class GISSpatial //抽象的空间信息类
    {
        public GISVertex centroid; //中心点 属于节点类的对象
        public GISExtent extent;   //空间范围 最小外接矩形
        public abstract void draw(Graphics graphics, GISView view, bool Selected);
    }//有了抽象的空间spatial类之后 就可以重新定义三个对象实体类

    public class GISExtent //这是一个重要的类 空间范围
    {
        public GISVertex bottomleft;
        public GISVertex upright; //外接矩形的左下和右上角的节点
        public GISExtent(GISVertex _bottomleft, GISVertex _upright)
        {
            bottomleft = _bottomleft;
            upright = _upright;
        }
        public GISExtent(double x1, double x2, double y1, double y2)
        {
            upright = new GISVertex(Math.Max(x1, x2), Math.Max(y1, y2));
            bottomleft = new GISVertex(Math.Min(x1, x2), Math.Min(y1, y2));
        }
        public double getMinX()
        {
            return bottomleft.x;
        }
        public double getMaxX()
        {
            return upright.x;
        }
        public double getMinY()
        {
            return bottomleft.y;
        }
        public double getMaxY()
        {
            return upright.y;
        }
        public double getWidth()
        {
            return upright.x - bottomleft.x;
        }
        public double getHeight()
        {
            return upright.y - bottomleft.y;
        }

        double ZoomingFactor = 1.2;//定义缩放因子
        double MovingFactor = 0.25;//移动因子

        public void ChangeExtent(GISMapActions action)
        {
            double newminx = bottomleft.x, newminy = bottomleft.y,
                newmaxx = upright.x, newmaxy = upright.y;
            switch (action)
            {
                case GISMapActions.zoomin:
                    newminx = ((getMinX() + getMaxX()) - getWidth() / ZoomingFactor) / 2;
                    newminy = ((getMinY() + getMaxY()) - getHeight() / ZoomingFactor) / 2;
                    newmaxx = ((getMinX() + getMaxX()) + getWidth() / ZoomingFactor) / 2;
                    newmaxy = ((getMinY() + getMaxY()) + getHeight() / ZoomingFactor) / 2;
                    break;
                case GISMapActions.zoomout:
                    newminx = ((getMinX() + getMaxX()) - getWidth() * ZoomingFactor) / 2;
                    newminy = ((getMinY() + getMaxY()) - getHeight() * ZoomingFactor) / 2;
                    newmaxx = ((getMinX() + getMaxX()) + getWidth() * ZoomingFactor) / 2;
                    newmaxy = ((getMinY() + getMaxY()) + getHeight() * ZoomingFactor) / 2;
                    break;
                case GISMapActions.moveup:
                    newminy = getMinY() - getHeight() * MovingFactor;
                    newmaxy = getMaxY() - getHeight() * MovingFactor;
                    break;
                case GISMapActions.movedown:
                    newminy = getMinY() + getHeight() * MovingFactor;
                    newmaxy = getMaxY() + getHeight() * MovingFactor;
                    break;
                case GISMapActions.moveleft:
                    newminx = getMinX() + getWidth() * MovingFactor;
                    newmaxx = getMaxX() + getWidth() * MovingFactor;
                    break;
                case GISMapActions.moveright:
                    newminx = getMinX() - getWidth() * MovingFactor;
                    newmaxx = getMaxX() - getWidth() * MovingFactor;
                    break;
            }
            upright.x = newmaxx;
            upright.y = newmaxy;
            bottomleft.x = newminx;
            bottomleft.y = newminy;
        }
        public void CopyFrom(GISExtent extent)
        {
            upright.CopyFrom(extent.upright);
            bottomleft.CopyFrom(extent.bottomleft);
        }

        //判断两个空间对象是否相交 排除所有不相交情况就得到相交
        public bool IntersectOrNot(GISExtent extent)
        {
            return !(getMaxX() < extent.getMinX() || getMinX() > extent.getMaxX() ||
                getMaxY() < extent.getMinY() || getMinY() > extent.getMaxY());
        }
    }

    public class GISView
    {
        GISExtent CurrentMapExtent;
        Rectangle MapWindowsSize;
        double MapMinX, MapMinY;
        int WinW, WinH;
        double MapW, MapH;
        double ScaleX, ScaleY;
        /*显然有公式
         ScreenX = (MapX-MapMinX)/ScaleX
         ScreenY = WinH-(MapY-MapMinY)/ScaleY   */


        public GISView(GISExtent _extent, Rectangle _rectangle)
        {
            //CurrentMapExtent = _extent;//需要更新extent类使其提供高、宽信息
            //MapWindowsSize = _rectangle;
            Update(_extent, _rectangle);
        }
        public void Update(GISExtent _extent, Rectangle _rectangle)
        {
            CurrentMapExtent = _extent;
            MapWindowsSize = _rectangle;
            MapMinX = CurrentMapExtent.getMinX();
            MapMinY = CurrentMapExtent.getMinY();
            WinW = MapWindowsSize.Width;
            WinH = MapWindowsSize.Height;
            MapW = CurrentMapExtent.getWidth();
            MapH = CurrentMapExtent.getHeight();
            ScaleX = MapW / WinW;
            ScaleY = MapH / WinH;
        }
        public Point ToScreenPoint(GISVertex onevertex)//地图点到屏幕点转换
        {
            double ScreenX = (onevertex.x - MapMinX) / ScaleX;
            double ScreenY = WinH - (onevertex.y - MapMinY) / ScaleY;
            return new Point((int)ScreenX, (int)ScreenY);
        }
        public GISVertex ToMapVertex(Point point)
        {
            double MapX = ScaleX * point.X + MapMinX;
            double MapY = ScaleY * (WinH - point.Y) + MapMinY;
            return new GISVertex(MapX, MapY);
        }
        public void ChangeView(GISMapActions action)
        {
            CurrentMapExtent.ChangeExtent(action);
            Update(CurrentMapExtent, MapWindowsSize);
        }
        public void UpdateExtent(GISExtent extent)
        {
            CurrentMapExtent.CopyFrom(extent);
            Update(CurrentMapExtent, MapWindowsSize);
        }

        //计算屏幕距离函数
        public double ToScreenDistance(GISVertex v1, GISVertex v2)
        {
            Point p1 = ToScreenPoint(v1);
            Point p2 = ToScreenPoint(v2);
            double ScreenDistance = Math.Sqrt((double)((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)));
            Console.WriteLine("ScreenDistance is " + ScreenDistance.ToString());
            return ScreenDistance;
        }

        //新的计算屏幕距离函数 由于点和线最短距离的位置不知 
        //所以直接构造两个vertex调用原有toscreendistance计算即可
        public double ToScreenDistance(double distance)
        {
            return ToScreenDistance(new GISVertex(0, 0), new GISVertex(0, distance));
        }
    }

    //定义一个枚举类型用于记录各种地图浏览操作
    public enum GISMapActions
    {
        zoomin,zoomout,
        moveup,movedown,moveleft,moveright
    };

    public class GISShapefile //用于shapefile文件读取的类
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)] //头文件结构体
        struct ShapefileHeader
        {
            public int Unused1, Unused2, Unused3, Unused4;
            public int Unused5, Unused6, Unused7, Unused8;
            public int ShapeType;
            public double Xmin;
            public double Ymin;
            public double Xmax;
            public double Ymax;
            public double Unused9, Unused10, Unused11, Unused12;
        }
        static ShapefileHeader ReadFileHeader(BinaryReader br) //用于读取文件头的函数
        {//*************************************
            /*
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(ShapefileHeader)));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);//handle读取buff数组在内存中的指针
            //指针指向的内存被映射给一个结构体实例header
            ShapefileHeader header = (ShapefileHeader)Marshal.PtrToStructure
                (handle.AddrOfPinnedObject(), typeof(ShapefileHeader));
            handle.Free(); //释放内存 将其还给C#管理
            return header;
            */
            return (ShapefileHeader)GISTools.FromBytes(br, typeof(ShapefileHeader));
        }

        public static GISLayer ReadShapefile(string shpfilename)
        {
            FileStream fsr = new FileStream(shpfilename, FileMode.Open);//打开shp文件
            BinaryReader br = new BinaryReader(fsr);//获取文件流后用二进制读取工具
            ShapefileHeader sfh = ReadFileHeader(br);//调用之前的函数 获取头文件
            SHAPETYPE ShapeType = (SHAPETYPE)Enum.Parse( //类型整数变对应的枚举值
                typeof(SHAPETYPE), sfh.ShapeType.ToString());
            GISExtent extent = new GISExtent(sfh.Xmax, sfh.Xmin, sfh.Ymax, sfh.Ymin);
            string dbffilename = shpfilename.Replace(".shp", ".dbf");//更改后缀
            DataTable table = ReadDBF(dbffilename);
            GISLayer layer = new GISLayer(shpfilename, ShapeType, extent, ReadFields(table)); //gislayer的构造参数分别是名字 图层类型 范围 *GISField的泛型
            int rowindex = 0; //当前读取的记录位置
            while (br.PeekChar() != -1)
            {
                RecordHeader rh = ReadRecordHeader(br);
                int RecordLength = FromBigToLittle(rh.RecordLength) * 2 - 4;
                byte[] RecordContent = br.ReadBytes(RecordLength);//将记录内容读入字节数组

                if (ShapeType == SHAPETYPE.point)
                {
                    GISPoint onepoint = ReadPoint(RecordContent);
                    GISFeature onefeature = new GISFeature(onepoint, ReadAttribute(table,rowindex));
                    layer.AddFeature(onefeature);
                }
                if (ShapeType == SHAPETYPE.line)
                {
                    List<GISLine> lines = ReadLines(RecordContent);
                    for (int i = 0; i < lines.Count; i++)
                    {
                        GISFeature onefeature = new GISFeature(lines[i], ReadAttribute(table, rowindex));
                        layer.AddFeature(onefeature);
                    }
                }
                if (ShapeType == SHAPETYPE.polygon)
                {
                    List<GISPolygon> polygons = ReadPolygons(RecordContent);
                    for (int i = 0; i < polygons.Count; i++)
                    {
                        GISFeature onefeature = new GISFeature(polygons[i], ReadAttribute(table, rowindex));
                        layer.AddFeature(onefeature);
                    }
                }
                rowindex++;

            }

            br.Close();
            fsr.Close();//归还文件权限于操作系统
            return layer;//最后返回一个图层文件
        }
        static GISPoint ReadPoint(byte[] RecordContent)
        {//从字节数组指定位置的八个字节转换为双精度浮点数
            double x = BitConverter.ToDouble(RecordContent, 0);
            double y = BitConverter.ToDouble(RecordContent, 8);
            return new GISPoint(new GISVertex(x, y));
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)] //逐条记录的记录头的结构体
        struct RecordHeader
        {
            public int RecordNumber;
            public int RecordLength;
            public int ShapeType;
        }
        static RecordHeader ReadRecordHeader(BinaryReader br) 
        {//用于读取记录头的函数 几乎与读文件头的函数相同
         //*************************************
         /*
             byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(RecordHeader)));
             GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);//handle读取buff数组在内存中的指针
             //指针指向的内存被映射给一个结构体实例header
             RecordHeader header = (RecordHeader)Marshal.PtrToStructure
                 (handle.AddrOfPinnedObject(), typeof(RecordHeader));
             handle.Free(); //释放内存 将其还给C#管理
             return header;
             */
            return (RecordHeader)GISTools.FromBytes(br, typeof(RecordHeader));
        }
        //通用转换函数 可用于BigInteger颠倒字节顺序重新构造正确数值
        static int FromBigToLittle(int bigvalue)
        {
            byte[] bigbytes = new byte[4];
            GCHandle handle = GCHandle.Alloc(bigbytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(bigvalue, handle.AddrOfPinnedObject(), false);
            handle.Free();
            byte b2 = bigbytes[2];
            byte b3 = bigbytes[3];
            bigbytes[3] = bigbytes[0];
            bigbytes[2] = bigbytes[1];
            bigbytes[1] = b2;
            bigbytes[0] = b3;
            return BitConverter.ToInt32(bigbytes, 0);
        }

        static List<GISLine> ReadLines(byte[] RecordContent)//读取线文件
        {
            int N = BitConverter.ToInt32(RecordContent, 32);//面线数量
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N + 1];//前n个节点记录每个独立实体的起始点位置，最后一个元素值为M
            for (int i = 0; i < N; i++) //相当于从40开始 每四个字节都记录的一个对象的起始点位置
            {
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            }
            parts[N] = M;
            List<GISLine> lines = new List<GISLine>();
            for (int i = 0; i < N; i++)
            {
                List<GISVertex> vertexs = new List<GISVertex>();
                for (int j = parts[i]; j < parts[i + 1]; j++) //之后都是顺序记录着的所有节点的xy坐标
                {
                    double x = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16);
                    double y = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16 + 8);
                    vertexs.Add(new GISVertex(x, y));
                }
                lines.Add(new GISLine(vertexs));
            }
            return lines;

        }

        static List<GISPolygon> ReadPolygons(byte[] RecordContent)//读取线文件
        {
            int N = BitConverter.ToInt32(RecordContent, 32);//面线数量
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N + 1];//前n个节点记录每个独立实体的起始点位置，最后一个元素值为M
            for (int i = 0; i < N; i++) //相当于从40开始 每四个字节都记录的一个对象的起始点位置
            {
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            }
            parts[N] = M;
            List<GISPolygon> polygons = new List<GISPolygon>();
            for (int i = 0; i < N; i++)
            {
                List<GISVertex> vertexs = new List<GISVertex>();
                for (int j = parts[i]; j < parts[i + 1]; j++) //之后都是顺序记录着的所有节点的xy坐标
                {
                    double x = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16);
                    double y = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16 + 8);
                    vertexs.Add(new GISVertex(x, y));
                }
                polygons.Add(new GISPolygon(vertexs));
            }
            return polygons;

        }

        static DataTable ReadDBF(string dbffilename)
        {
            System.IO.FileInfo f = new FileInfo(dbffilename);//通过系统自带的fileinfo对象获取文件所在的路径及文件名
            DataSet ds = null;
            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                f.DirectoryName + ";Extended Properties=DBASE III";
            using (OleDbConnection con = new OleDbConnection(constr))
            {
                var sql = "select * from " + f.Name; //通过一条sql语句把所有的数据选择出来并加载到dataset实例ds中
                OleDbCommand cmd = new OleDbCommand(sql, con);
                con.Open();
                ds = new DataSet(); ;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
            }
            return ds.Tables[0];
        }

        static List<GISField> ReadFields(DataTable table)//参数是上一个函数读出的datatable对象
        {
            List<GISField> fields = new List<GISField>();
            foreach (DataColumn column in table.Columns) //遍历column逐个获得字段类型和字段名称
            {
                fields.Add(new GISField(column.DataType, column.ColumnName));
            }
            return fields; //返回值就是gislayer需要的字段结构
        }

        static GISAttribute ReadAttribute(DataTable table, int RowIndex)
        {
            GISAttribute attribute = new GISAttribute();
            DataRow row = table.Rows[RowIndex]; //该函数读取给定序号的rowindex的一行数据
            for (int i = 0; i < table.Columns.Count; i++)
            {
                attribute.AddValue(row[i]);
            }
            return attribute;
        }

        public static void test()
        {
            Console.WriteLine("this is used to test the GitHub");
        }
    }
    public enum SHAPETYPE
    {
        point = 1,
        line = 3,
        polygon = 5
    };

    public class GISLayer
    {
        public string Name;
        public GISExtent Extent;
        public bool DrawAttributeOrNot = false;
        public int LabelIndex;
        public SHAPETYPE ShapeType;
        List<GISFeature> Features = new List<GISFeature>(); //私有的 不宜改动
        public List<GISField> Fields;
        public List<GISFeature> Selection = new List<GISFeature>();
        

        public GISLayer(string _name, SHAPETYPE _shapetype, GISExtent _extent, List<GISField> _fields)
        {
            Name = _name;
            ShapeType = _shapetype;
            Extent = _extent;
            Fields = _fields;
        }
        public GISLayer(string _name, SHAPETYPE _shapetype, GISExtent _extent)
        {
            Name = _name;
            ShapeType = _shapetype;
            Extent = _extent;
            Fields = new List<GISField>();
        }
        public void draw(Graphics graphics, GISView view)
        {
            for (int i = 0; i < Features.Count; i++)
            {
                Features[i].draw(graphics,view,DrawAttributeOrNot,LabelIndex);
            }
        }
        public void AddFeature(GISFeature feature)
        {
            //确保每个新增的feature的id都为已有对象的最大id加1 由此固定了id与feature的关系
            if (Features.Count == 0) feature.ID = 0;
            else feature.ID = Features[Features.Count - 1].ID + 1;
            Features.Add(feature);
        }
        public int FeatureCount()
        {
            return Features.Count;
        }
        public GISFeature GetFeature(int i)
        {
            return Features[i];
        }

        public List<GISFeature> GetAllFeatures()
        {
            return Features;
        }

        public SelectResult Select(GISVertex vertex, GISView view)
        {
            GISSelect gs = new GISSelect();
            SelectResult sr = gs.Select(vertex, Features, ShapeType, view);
            if (sr == SelectResult.OK) //判断空间对象selected属性 如之前未被选中，就令其值为true并加入数组中
            {
                if (ShapeType == SHAPETYPE.polygon)
                {
                    for (int i = 0; i < gs.SelectedFeatures.Count; i++)
                    {
                        if (gs.SelectedFeatures[i].Selected == false)
                        {
                            gs.SelectedFeatures[i].Selected = true;
                            Selection.Add(gs.SelectedFeatures[i]);
                        }
                    }
                }
                else
                {
                    if (gs.SelectedFeature.Selected == false)
                    {
                        gs.SelectedFeature.Selected = true;
                        Selection.Add(gs.SelectedFeature);
                    }
                }
            }
            return sr;
        }

        public void ClearSelection()
        {
            for (int i = 0; i < Selection.Count; i++)
                Selection[i].Selected = false;
            Selection.Clear();
        }

        //通过id找到gisfeature 并给他的selected赋值
        public void AddSelectedFeatureByID(int id)
        {
            GISFeature feature = GetFeatureByID(id);
            feature.Selected = true;
            Selection.Add(feature);
        }
        public GISFeature GetFeatureByID(int id)
        {
            foreach (GISFeature feature in Features)
                if (feature.ID == id) return feature;
            return null;
        }



    }
    public class GISTools
    {
        public static GISVertex CalculateCentroid(List<GISVertex> _vertexes)
        {
            if (_vertexes.Count == 0) return null;
            double x = 0;
            double y = 0;
            for (int i = 0; i < _vertexes.Count; i++)
            {
                x += _vertexes[i].x;
                y += _vertexes[i].y;
            }
            return new GISVertex(x / _vertexes.Count, y / _vertexes.Count);
        }

        public static GISExtent CalculateExtent(List<GISVertex> _vertexes)
        {
            if (_vertexes.Count == 0) return null;
            double minx = Double.MaxValue;
            double miny = Double.MaxValue;
            double maxx = Double.MinValue;
            double maxy = Double.MinValue;
            for (int i = 0; i < _vertexes.Count; i++)
            {
                if (_vertexes[i].x < minx) minx = _vertexes[i].x;
                if (_vertexes[i].y < miny) miny = _vertexes[i].y;
                if (_vertexes[i].x > maxx) maxx = _vertexes[i].x;
                if (_vertexes[i].y < maxy) maxy = _vertexes[i].y;
            }
            return new GISExtent(minx, miny, maxx, maxy);
        }

        public static double CalculateLength(List<GISVertex> _vertexes)
        {
            double length = 0;
            for (int i = 0; i < _vertexes.Count - 1; i++)
            {
                length += _vertexes[i].Distance(_vertexes[i + 1]);
            }
            return length;
        }

        public static double CalculateArea(List<GISVertex> _vertexes)
        {
            double area = 0;
            for (int i = 0; i < _vertexes.Count - 1; i++)
            {
                area += VectorProduct(_vertexes[i], _vertexes[i + 1]);
            }
            area += VectorProduct(_vertexes[_vertexes.Count - 1], _vertexes[0]);//注意此处最后一个点到起始点的矢量也要算
            return area / 2;
        }
        public static double VectorProduct(GISVertex v1, GISVertex v2)
        {
            return v1.x * v2.y - v1.y * v2.x;
        }
        public static Point[] GetScreenPoints(List<GISVertex> _vertexes, GISView view)
        {
            Point[] points = new Point[_vertexes.Count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = view.ToScreenPoint(_vertexes[i]);
            }
            return points;
        }

        public static byte[] ToBytes(object c) //结构体实例转字节数组的方法
        {
            byte [] bytes = new byte[Marshal.SizeOf(c.GetType())]; //定义一个与结构体字节数等长的字节数组
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);//为指定对象c分配句柄 用于内存操作
            Marshal.StructureToPtr(c, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return bytes;
        }

        //string转bw写入函数
        public static void WriteString(string s, BinaryWriter bw)
        {//先写一个整数记录字符串长度 再将string变字节数组写入
            bw.Write(StringLength(s)); //一般情况下字符数都是字节数 但是中文会占用两个字节
            byte[] sbytes = Encoding.Default.GetBytes(s);
            bw.Write(sbytes);
        }

        public static int StringLength(string s)
        {
            int ChineseCount = 0;
            //将字符串转换为ASCII码来编码的字节数组
            byte[] bs = new ASCIIEncoding().GetBytes(s);
            foreach (byte b in bs) {
                //转bs时所有双字节中文会被转换成单字节的0X3F
                if (b == 0X3F) ChineseCount++;
            }
            return ChineseCount + bs.Length;
        }

        //给定数据类型转换为整数
        public static int TypeToInt(Type type)
        {
            ALLTYPES onetype = (ALLTYPES)Enum.Parse(typeof(ALLTYPES), type.ToString().Replace(".", "_"));
            return (int)onetype;
        }

        //从文件中读取某个结构体的实例
        public static Object FromBytes(BinaryReader br, Type type)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(type));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            Object result = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), type);
            handle.Free();
            return result;
        }

        //读取整数确定字符串字节长度，读取相应的字节长度，恢复成字符串
        public static string ReadString(BinaryReader br)
        {
            int length = br.ReadInt32();
            byte[] sbytes = br.ReadBytes(length);//length时每次读取字节流并写入字节数组后后前移的长度
            return Encoding.Default.GetString(sbytes);//将字节编码成字符串
        }

        //读到的整数转换为特定数据类型
        public static Type IntToType(int index)
        {
            string typestring = Enum.GetName(typeof(ALLTYPES), index);//在枚举中检索具有指定值的常数名称
            typestring = typestring.Replace("_", ".");
            return Type.GetType(typestring);
        }

        public static double PointToSegment(GISVertex A, GISVertex B, GISVertex C)
        {
            double dot1 = Dot3Product(A, B, C);
            if (dot1 > 0) return B.Distance(C);
            double dot2 = Dot3Product(B, A, C);
            if (dot2 > 0) return A.Distance(C);
            double dist = Cross3Product(A, B, C) / A.Distance(B);
            return Math.Abs(dist);
        }

        //定义两个矢量运算 点积和叉积
        static double Dot3Product(GISVertex A, GISVertex B, GISVertex C)
        {
            GISVertex AB = new GISVertex(B.x - A.x, B.y - A.y);//矢量也可以通过vertex来记录
            GISVertex BC = new GISVertex(C.x - B.x, C.y - B.y);
            return AB.x * BC.x + AB.y * BC.y;
        }

        static double Cross3Product(GISVertex A, GISVertex B, GISVertex C)
        {
            GISVertex AB = new GISVertex(B.x - A.x, B.y - A.y);//矢量也可以通过vertex来记录
            GISVertex AC = new GISVertex(C.x - A.x, C.y - A.y);
            return VectorProduct(AB, AC);
        }

    }
    public class GISField
    {
        public Type datatype;
        public string name;
        public GISField(Type _dt, string _name)
        {
            datatype = _dt;
            name = _name;
        }
    }

    public class GISMyFile
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]//定义结构体
        struct MyFileHeader
        {
            public double MinX, MinY, MaxX, MaxY;
            public int FeatureCount, Shapetype, FieldCount;
        };

        static void WriteFileHeader(GISLayer layer, BinaryWriter bw)//bw是与文件相连的文件写入工具
        {
            MyFileHeader mfh = new MyFileHeader();
            mfh.MinX = layer.Extent.getMinX();
            mfh.MinY = layer.Extent.getMinY();
            mfh.MaxX = layer.Extent.getMaxX();
            mfh.MaxY = layer.Extent.getMaxY();
            mfh.FeatureCount = layer.FeatureCount();
            mfh.Shapetype = (int)(layer.ShapeType);
            mfh.FieldCount = layer.Fields.Count;
            bw.Write(GISTools.ToBytes(mfh));
        }

        //写文件函数主框架
        public static void WriteFile(GISLayer layer, string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Create); //根据文件名创建文件流
            BinaryWriter bw = new BinaryWriter(fsr);
            WriteFileHeader(layer, bw); //写入头文件
            GISTools.WriteString(layer.Name, bw); //写入图层名字
            WriteFields(layer.Fields, bw); //写入字段
            WriteFeatures(layer, bw);
            bw.Close();
            fsr.Close();
        }

        static void WriteFields(List<GISField> fields, BinaryWriter bw)
        {
            for(int fieldindex = 0;fieldindex<fields.Count;fieldindex++)
            {
                GISField field = fields[fieldindex];
                bw.Write(GISTools.TypeToInt(field.datatype)); //字段类型
                GISTools.WriteString(field.name, bw); //字段名
            }
        }

        //定义写入多节点类
        static void WriteMultipleVertexes(List<GISVertex> vs, BinaryWriter bw)
        {
            bw.Write(vs.Count); //先写入记录vs的总数
            for (int vc = 0; vc < vs.Count; vc++)
                vs[vc].WriteVertex(bw);
        }

        static void WriteAttributes(GISAttribute attribute, BinaryWriter bw)
        {
            for (int i = 0; i < attribute.ValueCount(); i++)
            {
                Type type = attribute.GetValue(i).GetType();
                if (type.ToString() == "System.Boolean")
                    bw.Write((bool)attribute.GetValue(i));
                else if (type.ToString() == "System.Byte")
                    bw.Write((byte)attribute.GetValue(i));
                else if (type.ToString() == "System.Char")
                    bw.Write((char)attribute.GetValue(i));
                else if (type.ToString() == "System.Decimal")
                    bw.Write((decimal)attribute.GetValue(i));
                else if (type.ToString() == "System.Double")
                    bw.Write((double)attribute.GetValue(i));
                else if (type.ToString() == "System.Single")
                    bw.Write((float)attribute.GetValue(i));
                else if (type.ToString() == "System.Int32")
                    bw.Write((int)attribute.GetValue(i));
                else if (type.ToString() == "System.Int64")
                    bw.Write((long)attribute.GetValue(i));
                else if (type.ToString() == "System.UInt16")
                    bw.Write((ushort)attribute.GetValue(i));
                else if (type.ToString() == "System.UInt32")
                    bw.Write((uint)attribute.GetValue(i));
                else if (type.ToString() == "System.UInt64")
                    bw.Write((ulong)attribute.GetValue(i));
                else if (type.ToString() == "System.SByte")
                    bw.Write((sbyte)attribute.GetValue(i));
                else if (type.ToString() == "System.Int16")
                    bw.Write((short)attribute.GetValue(i));
                else if (type.ToString() == "System.String")
                    GISTools.WriteString((string)attribute.GetValue(i), bw);
            }
        }

        //输出图层所有GISFeatrue
        static void WriteFeatures(GISLayer layer, BinaryWriter bw)
        {
            for (int featureindex = 0; featureindex < layer.FeatureCount(); featureindex++)
            {
                GISFeature feature = layer.GetFeature(featureindex);
                if (layer.ShapeType == SHAPETYPE.point)
                {
                    ((GISPoint)feature.spatialpart).centroid.WriteVertex(bw);
                }
                else if (layer.ShapeType == SHAPETYPE.line)
                {
                    GISLine line = (GISLine)(feature.spatialpart);
                    WriteMultipleVertexes(line.Vertexes,bw);
                }
                else if (layer.ShapeType == SHAPETYPE.polygon)
                {
                    GISPolygon polygon = (GISPolygon)(feature.spatialpart);
                    WriteMultipleVertexes(polygon.Vertexes, bw);
                }
                WriteAttributes(feature.attributepart, bw);
            }
        }

        //*************
        //从文件中读取字段信息
        static List<GISField> ReadFields(BinaryReader br, int FieldCount)
        {
            List<GISField> fields = new List<GISField>();
            for (int fieldindex = 0; fieldindex < FieldCount; fieldindex++)
            {
                Type fieldtype = GISTools.IntToType(br.ReadInt32());
                string fieldname = GISTools.ReadString(br);
                fields.Add(new GISField(fieldtype, fieldname));
            }
            return fields;
        }

        static List<GISVertex> ReadMultipleVertexes(BinaryReader br)
        {
            List<GISVertex> vs = new List<GISVertex>();
            int vcount = br.ReadInt32();
            for (int vc = 0; vc < vcount; vc++)
                vs.Add(new GISVertex(br));
            return vs;
        }

        //读取一个gisfeature的所有属性值，放在gisfile中 此函数需要事先知道字段结构，根据字段类型选取适当的读取函数
        static GISAttribute ReadAttributes(List<GISField> fs, BinaryReader br)
        {
            GISAttribute attribute = new GISAttribute();
            for (int i = 0; i < fs.Count; i++)
            {
                Type type = fs[i].datatype;
                if (type.ToString() == "System.Boolean")
                    attribute.AddValue(br.ReadBoolean());
                else if (type.ToString() == "System.Boolean")
                    attribute.AddValue(br.ReadBoolean());
                else if (type.ToString() == "System.Byte")
                    attribute.AddValue(br.ReadByte());
                else if (type.ToString() == "System.Char")
                    attribute.AddValue(br.ReadChar());
                else if (type.ToString() == "System.Decimal")
                    attribute.AddValue(br.ReadDecimal());
                else if (type.ToString() == "System.Double")
                    attribute.AddValue(br.ReadDouble());
                else if (type.ToString() == "System.Single")
                    attribute.AddValue(br.ReadSingle());
                else if (type.ToString() == "System.Int32")
                    attribute.AddValue(br.ReadInt32());
                else if (type.ToString() == "System.Int64")
                    attribute.AddValue(br.ReadInt64());
                else if (type.ToString() == "System.UInt16")
                    attribute.AddValue(br.ReadUInt16());
                else if (type.ToString() == "System.UInt32")
                    attribute.AddValue(br.ReadUInt32());
                else if (type.ToString() == "System.UInt64")
                    attribute.AddValue(br.ReadUInt64());
                else if (type.ToString() == "System.Boolean")
                    attribute.AddValue(br.ReadBoolean());
                else if (type.ToString() == "System.String")
                    attribute.AddValue(GISTools.ReadString(br));
            }
            return attribute;
        }

        //读取所有的gisfeature的空间信息和属性值
        static void ReadFeatures(GISLayer layer, BinaryReader br, int FeatureCount)
        {
            for (int featureindex = 0; featureindex < FeatureCount; featureindex++)
            {
                GISFeature feature = new GISFeature(null,null);
                if (layer.ShapeType == SHAPETYPE.point)
                    feature.spatialpart = new GISPoint(new GISVertex(br));
                else if (layer.ShapeType == SHAPETYPE.line)
                    feature.spatialpart = new GISLine(ReadMultipleVertexes(br));
                else if (layer.ShapeType == SHAPETYPE.polygon)
                    feature.spatialpart = new GISPolygon(ReadMultipleVertexes(br));
                feature.attributepart = ReadAttributes(layer.Fields, br);
                layer.AddFeature(feature);
            }
        }

        public static GISLayer ReadFile(string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fsr);
            MyFileHeader mfh = (MyFileHeader)(GISTools.FromBytes(br, typeof(MyFileHeader)));//读取文件头
            SHAPETYPE ShapeType = (SHAPETYPE)Enum.Parse(typeof(SHAPETYPE), mfh.Shapetype.ToString());//获取空间实体类型shapetype和
            GISExtent Extent = new GISExtent(mfh.MinX, mfh.MaxX, mfh.MinY, mfh.MaxY);//地图范围extent
            string layername = GISTools.ReadString(br);//读取图层名
            List<GISField> Fields = ReadFields(br, mfh.FieldCount);//读取字段信息
            GISLayer layer = new GISLayer(layername, ShapeType, Extent, Fields);
            ReadFeatures(layer, br, mfh.FeatureCount);
            br.Close();
            fsr.Close();
            return layer;
        }
    }

    public enum ALLTYPES //枚举中不允许有. 只有_
    {
        System_Boolean,
        System_Byte,
        System_Char,
        System_Decimal,
        System_Double,
        System_Single,
        System_Int32,
        System_Int64,
        System_SByte,
        System_Int16,
        System_String,
        System_UInt32,
        System_UInt64,
        System_UInt16
    };

    //记录操作后的反馈状态
    public enum SelectResult
    {
        //正常选择状态：选择到一个结果\
        OK,
        //错误选择状态：备选集是空的
        EmptySet,
        //错误选择状态：点击选择时离空间对象太远
        TooFar,
        //错误选择状态：位置空间对象
        UnknownType
    }

    public class GISSelect
    {
        public GISFeature SelectedFeature = null;
        public List<GISFeature> SelectedFeatures = new List<GISFeature>();//用于存放可能不止一个被选中的多边形对象 
        public SelectResult Select(GISVertex vertex, List<GISFeature> features, SHAPETYPE shapetype, GISView view)
        {
            if (features.Count == 0) return SelectResult.EmptySet;
            GISExtent MinSelectExtent = BuildExtent(vertex, view);//建立最小选择范围
            switch (shapetype)
            {
                case SHAPETYPE.point:
                    Console.WriteLine("point");
                    return SelectPoint(vertex, features, view, MinSelectExtent);
                case SHAPETYPE.line:
                    Console.WriteLine("line");
                    return SelectLine(vertex, features, view, MinSelectExtent);
                case SHAPETYPE.polygon:
                    Console.WriteLine("polygon");
                    return SelectPolygon(vertex, features, view, MinSelectExtent);
            }
            return SelectResult.UnknownType;
        }

        //根据鼠标点和距离阈值构造点选范围extent
        public GISExtent BuildExtent(GISVertex vertex, GISView view)
        {
            Point p0 = view.ToScreenPoint(vertex);
            Point p1 = new Point(p0.X + (int)GISConst.MinScreenDistance, p0.Y + (int)GISConst.MinScreenDistance);
            Point p2 = new Point(p0.X - (int)GISConst.MinScreenDistance, p0.Y - (int)GISConst.MinScreenDistance);
            GISVertex gmp1 = view.ToMapVertex(p1);
            GISVertex gmp2 = view.ToMapVertex(p2);
            return new GISExtent(gmp1.x, gmp2.x, gmp1.y, gmp2.y);
        }

        public SelectResult SelectPoint(GISVertex vertex, List<GISFeature> features, 
            GISView view, GISExtent MinSelectExtent)
        {
            Double distance = Double.MaxValue;
            int id = -1;
            for (int i = 0; i < features.Count; i++) //找最近的feature判断是否有效
            {
                if (MinSelectExtent.IntersectOrNot(features[i].spatialpart.extent) == false) continue;
                GISPoint point = (GISPoint)(features[i].spatialpart);
                double dist = point.Distance(vertex);
                if (dist < distance)//每次找到最小的距离并记录id号
                {
                    distance = dist;
                    id = i;
                }
            }
            Console.WriteLine("id:"+ id.ToString());//测试id是否存在
            if (id!=-1)
                Console.WriteLine(features[id].spatialpart.centroid.x.ToString() + "|" + features[id].spatialpart.centroid.y.ToString());
            Console.WriteLine("鼠标点到元素点在地图上相距"+distance.ToString());//此处的distance是features中最近的元素点到鼠标点映射到地图上点的距离
            //精选
            if (id == -1)//经过遍历 没有与minsextent相交的点则跳出
            {
                SelectedFeature = null;
                return SelectResult.TooFar;
            }
            else 
            {
                double screendistance = view.ToScreenDistance(vertex, features[id].spatialpart.centroid);
                //Console.WriteLine(screendistance);
                if (screendistance < GISConst.MinScreenDistance)
                {
                    SelectedFeature = features[id];
                    return SelectResult.OK;
                }
                else 
                {
                    SelectedFeature = null;
                    return SelectResult.TooFar;//即使在粗选相交 精选距离也超过了限制 则返回
                }
            }
        }


        public SelectResult SelectLine(GISVertex vertex, List<GISFeature> features,
            GISView view, GISExtent MinSelectExtent)
        {
            Double distance = Double.MaxValue;
            int id = -1;
            for (int i = 0; i < features.Count; i++) //找最近的feature判断是否有效
            {
                if (MinSelectExtent.IntersectOrNot(features[i].spatialpart.extent) == false) continue;
                GISLine line = (GISLine)(features[i].spatialpart);
                double dist = line.Distance(vertex);
                //Console.WriteLine("dist:" + dist);
                if (dist < distance)//每次找到最小的距离并记录id号
                {
                    distance = dist;
                    id = i;
                }
            }
            Console.WriteLine("id:" + id.ToString());//测试id是否存在
            //Console.WriteLine(distance);
            //精选
            if (id == -1)//经过遍历 没有与minsextent相交的点则跳出
            {
                SelectedFeature = null;
                return SelectResult.TooFar;
            }
            else
            {
                double screendistance = view.ToScreenDistance(distance);
                if (screendistance < GISConst.MinScreenDistance)
                {
                    SelectedFeature = features[id];
                    return SelectResult.OK;
                }
                else
                {
                    SelectedFeature = null;
                    return SelectResult.TooFar;//即使在粗选相交 精选距离也超过了限制 则返回
                }
            }
        }


        public SelectResult SelectPolygon(GISVertex vertex, List<GISFeature> features,
            GISView view, GISExtent MinSelectExtent)
        {
            SelectedFeatures.Clear();
            for (int i = 0; i < features.Count; i++)
            {
                //先粗选点与多边形extent是否有交集
                //Console.WriteLine("1");
                if (MinSelectExtent.IntersectOrNot(features[i].spatialpart.extent) == false) continue;
                GISPolygon polygon = (GISPolygon)(features[i].spatialpart);
                if (polygon.include(vertex))
                    SelectedFeatures.Add(features[i]);
            }
            return (SelectedFeatures.Count > 0) ? SelectResult.OK : SelectResult.TooFar;
        }
    }

    public class GISConst
    {
        public static double MinScreenDistance = 5;
        //点的颜色和半径
        public static Color PointColor = Color.Pink;
        public static int PointSize = 3;
        //线的颜色与宽度
        public static Color LineColor = Color.CadetBlue;
        public static int LineWidth = 2;
        //面的边框颜色、填充颜色及边框宽度
        public static Color PolygonBoundaryColor = Color.White;
        public static Color PolygonFillColor = Color.Gray;
        public static int PolygonBoundaryWidth = 2;
        //被选中点的颜色
        public static Color SelectedPointColor = Color.Red;
        //被选中线的颜色
        public static Color SelectedLineColor = Color.Blue;
        //被选中面的填充颜色
        public static Color SelectedPolygonFillColor = Color.Yellow;
    }

}