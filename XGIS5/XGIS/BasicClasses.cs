﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace XGIS
{
    public class XMyFile
    {
        public void WriteFile(XLayer layer, string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fsr);
            //写文件头
            WriteFileHeader(layer, bw);
            //写图层名称
            XTools.WriteString(layer.Name, bw);
            //写属性字段结构
            foreach (XField field in layer.Fields)
            {
                XTools.WriteString(field.datatype.ToString(), bw);
                XTools.WriteString(field.name, bw);
            }
            //写空间对象类型
            foreach (XFeature f in layer.Features)
                WriteFeature(f, bw);
            //关闭文件
            bw.Close();
            fsr.Close();
        }
        public XLayer ReadFile(string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fsr);
            //读文件头
            MyFileHeader mfh = (MyFileHeader)XTools.FromBytes(br, typeof(MyFileHeader));
            //读图层名称
            string name = XTools.ReadString(br);
            //读属性字段结构
            List<XField> fields = ReadFields(mfh.FieldCount, br);
            //定义图层
            SHAPETYPE ShapeType =
                (SHAPETYPE)Enum.Parse(typeof(SHAPETYPE), 
                mfh.ShapeType.ToString());
            XExtent extent = new XExtent(
                new XVertex(mfh.MinX, mfh.MinY), 
                new XVertex(mfh.MaxX, mfh.MaxY));
            XLayer layer = new XLayer(name, ShapeType, extent, fields);
            //读空间对象类型
            for (int i = 0; i < mfh.FeatureCount; i++)
            {
                XSpatial spatial = ReadSpatial(ShapeType, br);
                XAttribute attribute = ReadAttribute(br, fields);
                layer.AddFeature(new XFeature(spatial, attribute));
            }
            //关闭文件并返回结果
            br.Close();
            fsr.Close();
            return layer;
        }

        private XAttribute ReadAttribute(BinaryReader br, List<XField> fields)
        {
            int count = br.ReadInt32();
            XAttribute a = new XAttribute();
            for (int i = 0; i < count; i++)
                a.AddValue(fields[i].StringToObject(XTools.ReadString(br)));
            return a;
        }

        private XSpatial ReadSpatial(SHAPETYPE shapeType, BinaryReader br)
        {
            if (shapeType == SHAPETYPE.point)
                return new XPointSpatial(new XVertex(br));
            if (shapeType == SHAPETYPE.line)
                return new XLineSpatial(ReadMultipleVertexes(br));
            if (shapeType == SHAPETYPE.polygon)
                return new XPolygonSpatial(ReadMultipleVertexes(br));
            return null;

        }

        private List<XVertex> ReadMultipleVertexes(BinaryReader br)
        {
            int count = br.ReadInt32();
            List<XVertex> vs=new List<XVertex>();
            for (int i = 0; i < count; i++)
                vs.Add(new XVertex(br));
            return vs;
        }

        private List<XField> ReadFields(int fieldCount, BinaryReader br)
        {
            List<XField> fields = new List<XField>();
            for (int fieldindex = 0; fieldindex < fieldCount; fieldindex++)
            {
                Type fieldtype = Type.GetType(XTools.ReadString(br));
                string fieldname = XTools.ReadString(br);
                fields.Add(new XField(fieldtype, fieldname));
            }
            return fields;

        }

        private void WriteFeature(XFeature feature, BinaryWriter bw)
        {
            if (feature.Spatial is XPointSpatial)
            {
                ((XPointSpatial)feature.Spatial).Centroid.WriteVertex(bw);
            }
            if (feature.Spatial is XLineSpatial)
            {
                WriteMultipleVertexes(((XLineSpatial)feature.Spatial).
                    AllVertexes, bw);
            }
            if (feature.Spatial is XPolygonSpatial)
            {
                WriteMultipleVertexes(((XPolygonSpatial)feature.Spatial).
                    AllVertexes, bw);
            }
            WriteAttribute(feature.Attribute, bw);
        }

        private void WriteAttribute(XAttribute a, BinaryWriter bw)
        {
            bw.Write(a.Values.Count);
            foreach (object v in a.Values)
                XTools.WriteString(v.ToString(), bw);
        }

        private void WriteMultipleVertexes(List<XVertex> vs, BinaryWriter bw)
        {
            bw.Write(vs.Count);
            foreach (XVertex v in vs)
                v.WriteVertex(bw);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct MyFileHeader
        {
            public double MinX, MinY, MaxX, MaxY;
            public int FeatureCount, ShapeType, FieldCount;
        };
        void WriteFileHeader(XLayer layer, BinaryWriter bw)
        {
            MyFileHeader mfh = new MyFileHeader();
            mfh.MinX = layer.Extent.GetMinX();
            mfh.MinY = layer.Extent.GetMinY();
            mfh.MaxX = layer.Extent.GetMaxX();
            mfh.MaxY = layer.Extent.GetMaxY();
            mfh.FeatureCount = layer.FeatureCount();
            mfh.ShapeType = (int)(layer.ShapeType);
            mfh.FieldCount = layer.Fields.Count;
            bw.Write(XTools.ToBytes(mfh));
        }

    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct DBFHeader
    {
        public char FileType, Year, Month, Day;
        public Int32 RecordCount;
        public Int16 HeaderLength, RecordLength;
        public long Unused1, Unused2;
        public char Unused3, Unused4;
        public Int16 Unused5;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DBFField
    {
        public byte b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11;
        public char FieldType;
        public int DisplacementInRecord;
        public byte LengthOfField;
        public byte NumberOfDecimalPlaces;
        public byte Unused19, Unused20, Unused21, Unused22,
                     Unused23, Unused24, Unused25, Unused26, Unused27,
                     Unused28, Unused29, Unused30, Unused31, Unused32;
    }
    
    public class XField
    {
        public Type datatype;
        public string name;

        DBFField dbfField;

        public XField(BinaryReader br)
        {
            //获得DBF字段
            dbfField = (DBFField)XTools.FromBytes(br, typeof(DBFField));
            //字段名
            byte[] bs = new byte[] {dbfField.b1,dbfField.b2,dbfField.b3, dbfField.b4,dbfField.b5,
                dbfField.b6,dbfField.b7,dbfField.b8,dbfField.b9,dbfField.b10,dbfField.b11};
            name = XTools.BytesToString(bs).Trim();
            //字段类型
            switch (dbfField.FieldType)
            {
                case 'C':  //字符型  允许输入各种字符
                    datatype = Type.GetType("System.String");
                    break;
                case 'D':  //日期型  用于区分年、月、日的数字和一个字符，按照YYYYMMDD格式。
                    datatype = Type.GetType("System.String");
                    break;
                case 'N':  //数值型
                    if (dbfField.NumberOfDecimalPlaces == 0)
                        datatype = Type.GetType("System.Int32");
                    else
                        datatype = Type.GetType("System.Double");
                    break;
                case 'F':
                    datatype = Type.GetType("System.Double");
                    break;
                case 'B':  //二进制 允许输入各种字符
                    datatype = Type.GetType("System.String");
                    break;
                case 'G':  //General or OLE                            
                    datatype = Type.GetType("System.String");
                    break;
                case 'L':  //逻辑型，表示没有初始化
                    datatype = Type.GetType("System.String");
                    break;
                case 'M': //Memo
                    datatype = Type.GetType("System.String");
                    break;
                default:
                    break;
            }

        }

        public XField(Type _datatype, string _name)
        {
            datatype = _datatype;
            name = _name;
        }

        internal object DBFValueToObject(BinaryReader br)
        {
            byte[] temp = br.ReadBytes(dbfField.LengthOfField);
            string sv = XTools.BytesToString(temp).Trim();
            if (datatype == Type.GetType("System.String"))
                return sv;
            else if (datatype == Type.GetType("System.Double"))
                return double.Parse(sv);
            else if (datatype == Type.GetType("System.Int32"))
                return int.Parse(sv);
            return sv;
        }

        internal object StringToObject(string value)
        {
            if (datatype == Type.GetType("System.String"))
                return value;
            else if (datatype == Type.GetType("System.Double"))
                return double.Parse(value);
            else if (datatype == Type.GetType("System.Int32"))
                return int.Parse(value);
            //可持续写下去，处理所有可能的数据类型，比如Boolean，Int64等
            return value;

        }
    }
    public class XLayer
    {
        public string Name;
        public XExtent Extent;
        public bool DrawAttributeOrNot;
        public int LabelIndex;
        public SHAPETYPE ShapeType;
        public List<XFeature> Features = new List<XFeature>();
        public List<XField> Fields;
        public XLayer(string _Name, SHAPETYPE _ShapeType, 
            XExtent _Extent, List<XField> _Fields)
        {
            Name = _Name;
            ShapeType = _ShapeType;
            Extent = _Extent;
            Fields = _Fields;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangeFieldOrder(string fieldName, bool upDonw)
        {

        }

        public void AddField(XField field)
        {
            
        }

        public void Draw(Graphics _Graphics, XView _View)
        {
            for (int i = 0; i < Features.Count; i++)
            {
                Features[i].Draw(_Graphics, _View, DrawAttributeOrNot, LabelIndex);
            }
        }
        public void AddFeature(XFeature feature)
        {
            Features.Add(feature);
        }
        public int FeatureCount()
        {
            return Features.Count;
        }

        internal XFeature GetFeature(int i)
        {
            return Features[i];
        }
    }

    public enum SHAPETYPE
    {
        point = 1,
        line = 3,
        polygon = 5,
        mix = 7
    };


    public enum XMapActions
    {
        none,
        zoomin, zoomout,
        moveup, movedown, moveleft, moveright
    };

    public class XShapeFile
    {
        private static void ReadDBFFile(string dbffilename, 
            List<XField> fields, List<XAttribute> attributes)
        {
            FileStream fsr = new FileStream(dbffilename, FileMode.Open);
            BinaryReader br = new BinaryReader(fsr);
            DBFHeader dh = (DBFHeader)XTools.FromBytes(br, typeof(DBFHeader));
            int FieldCount = (dh.HeaderLength - 33) / 32;


            //读字段结构
            fields.Clear();
            for (int i = 0; i < FieldCount; i++)
                fields.Add(new XField(br));






            byte END = br.ReadByte();  //1个字节作为记录项终止标识。



            //读具体数值
            attributes.Clear();
            for (int i = 0; i < dh.RecordCount; i++)
            {
                XAttribute attribute = new XAttribute();

                char tempchar = (char)br.ReadByte();  //每个记录的开始都有一个起始字节

                for (int j = 0; j < FieldCount; j++)
                    attribute.AddValue(fields[j].DBFValueToObject(br));


                attributes.Add(attribute);
            }
            br.Close();
            fsr.Close();
        }

        List<XPolygonSpatial> ReadPolygons(byte[] RecordContent)
        {
            int N = BitConverter.ToInt32(RecordContent, 32);
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N + 1];
            for (int i = 0; i < N; i++)
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            parts[N] = M;
            List<XPolygonSpatial> polygons = new List<XPolygonSpatial>();
            for (int i = 0; i < N; i++)
            {
                List<XVertex> vertexs = new List<XVertex>();
                for (int j = parts[i]; j < parts[i + 1]; j++)
                {
                    double x = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16);
                    double y = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16 + 8);
                    vertexs.Add(new XVertex(x, y));
                }
                polygons.Add(new XPolygonSpatial(vertexs));
            }
            return polygons;
        }
        List<XLineSpatial> ReadLines(byte[] RecordContent)
        {
            int N = BitConverter.ToInt32(RecordContent, 32);
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N + 1];
            for (int i = 0; i < N; i++)
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            parts[N] = M;
            List<XLineSpatial> lines = new List<XLineSpatial>();
            for (int i = 0; i < N; i++)
            {
                List<XVertex> vertexs = new List<XVertex>();
                for (int j = parts[i]; j < parts[i + 1]; j++)
                {
                    double x = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16);
                    double y = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16 + 8);
                    vertexs.Add(new XVertex(x, y));
                }
                lines.Add(new XLineSpatial(vertexs));
            }
            return lines;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
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
        };

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct RecordHeader
        {
            public int RecordNumber;
            public int RecordLength;
            public int ShapeType;
        };


        int FromBigToLittle(int bigvalue)
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
        XPointSpatial ReadPoint(byte[] RecordContent)
        {
            double x = BitConverter.ToDouble(RecordContent, 0);
            double y = BitConverter.ToDouble(RecordContent, 8);
            return new XPointSpatial(new XVertex(x, y));
        }

        public XLayer ReadShapefile(string shpfilename)
        {
            //打开文件和读取工具
            FileStream fsr = new FileStream(shpfilename, FileMode.Open);
            BinaryReader br = new BinaryReader(fsr);
            //读取文件头
            ShapefileHeader sfh = (ShapefileHeader)
                XTools.FromBytes(br, typeof(ShapefileHeader));
            //获得空间对象类型
            SHAPETYPE ShapeType = (SHAPETYPE)Enum.Parse(typeof(SHAPETYPE), sfh.ShapeType.ToString());
            //获得空间范围
            XExtent extent = new XExtent(new XVertex(sfh.Xmin, sfh.Ymin),
                new XVertex(sfh.Xmax, sfh.Ymax));
            //读dbf
            string dbfFileName = shpfilename.Replace(".shp", ".dbf");
            List<XField> fields = new List<XField>();
            List<XAttribute> attributes = new List<XAttribute>();
            ReadDBFFile(dbfFileName, fields, attributes);
            //构建图层
            XLayer layer = new XLayer(shpfilename, ShapeType, extent, fields);
            int index = 0;
            while (br.PeekChar() != -1)
            {
                //读记录头
                RecordHeader rh = (RecordHeader)
                        XTools.FromBytes(br, typeof(RecordHeader));
                int RecordLength = FromBigToLittle(rh.RecordLength) * 2 - 4;
                byte[] RecordContent = br.ReadBytes(RecordLength);
                //开始读实际的空间数据
                if (ShapeType==SHAPETYPE.point)
                {
                    XPointSpatial point = ReadPoint(RecordContent);
                    layer.AddFeature(new XFeature(point, attributes[index]));
                }
                else if (ShapeType==SHAPETYPE.line)
                {
                    List<XLineSpatial> lines = ReadLines(RecordContent);
                    foreach (XLineSpatial line in lines)
                        layer.AddFeature(new XFeature(line, new XAttribute(attributes[index])));
                }
                else if (ShapeType == SHAPETYPE.polygon)
                {
                    List<XPolygonSpatial> polygons = ReadPolygons(RecordContent);
                    foreach (XPolygonSpatial polygon in polygons)
                        layer.AddFeature(new XFeature(polygon, new XAttribute(attributes[index])));
                }
                index++;
            }
            //关闭读取工具和文件
            br.Close();
            fsr.Close();
            return layer;
        }




    }
    public class XTools
    {
        public static Object FromBytes(BinaryReader br, Type type)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(type));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            Object result = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), type);
            handle.Free();
            return result;
        }
        public static byte[] ToBytes(object c)
        {
            byte[] bytes = new byte[Marshal.SizeOf(c.GetType())];
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(c, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return bytes;
        }

        public static double CalculateLength(List<XVertex> _Vertexes)
        {
            double length = 0;
            for (int i = 0; i < _Vertexes.Count - 1; i++)
                length += _Vertexes[i].Distance(_Vertexes[i + 1]);
            return length;
        }

        public static double CalculateArea(List<XVertex> _Vertexes)
        {
            double area = 0;
            for (int i = 0; i < _Vertexes.Count - 1; i++)
                area += VectorProduct(_Vertexes[i], _Vertexes[i + 1]);
            area += VectorProduct(_Vertexes[_Vertexes.Count - 1], _Vertexes[0]);
            return area / 2;
        }

        public static double VectorProduct(XVertex v1, XVertex v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        public static XVertex CalculateCentroid(List<XVertex> _Vertexes)
        {
            if (_Vertexes.Count == 0) return null;
            double x = 0, y = 0;
            for (int i = 0; i < _Vertexes.Count; i++)
            {
                x += _Vertexes[i].X;
                y += _Vertexes[i].Y;
            }
            return new XVertex(x / _Vertexes.Count, y / _Vertexes.Count);
        }
        public static XExtent CalculateExtent(List<XVertex> _Vertexes)
        {
            if (_Vertexes.Count == 0) return null;
            double minx, miny, maxx, maxy;
            minx = miny = Double.MaxValue;
            maxx = maxy = Double.MinValue;
            foreach (XVertex v in _Vertexes)
            {
                minx = Math.Min(v.X, minx);
                miny = Math.Min(v.Y, miny);
                maxx = Math.Max(v.X, maxx);
                maxy = Math.Max(v.Y, maxy);
            }
            return new XExtent(new XVertex(minx, miny), new XVertex(maxx, maxy));
        }


        public static double DegreeToRadian(double degree)
        {
            return degree / 180 * Math.PI;
        }
        public static double LatitudeToMecatorY(double latitude)
        {
            return Math.Log(Math.Tan(DegreeToRadian(45 + latitude / 2)));
        }

        public static double LongitudeToMecatorX(double longitude)
        {
            return longitude;
        }

        public static double GetMecatorHeight(double lat1, double lat2)
        {
            return LatitudeToMecatorY(lat1) - LatitudeToMecatorY(lat2);
        }

        public static double GetMecatorWidth(double lon1, double lon2)
        {
            return LongitudeToMecatorX(lon1) - LongitudeToMecatorX(lon2);
        }

        internal static string BytesToString(byte[] byteArray)
        {
            int count = byteArray.Length;
            for (int i = 0; i < byteArray.Length; i++)
            {
                if (byteArray[i] == 0)
                {
                    count = i;
                    break;
                }
            }
            return Encoding.GetEncoding("gb2312").GetString(byteArray, 0, count);
        }

        internal static void WriteString(string s, BinaryWriter bw)
        {
            byte[] sbytes = Encoding.GetEncoding("gb2312").GetBytes(s);
            bw.Write(sbytes.Length);
            bw.Write(sbytes);
        }

        internal static string ReadString(BinaryReader br)
        {
            int length = br.ReadInt32();
            if (length == 0) return "";
            byte[] sbytes = br.ReadBytes(length);
            return Encoding.GetEncoding("gb2312").GetString(sbytes);

        }
    }
    public class XView
    {
        XExtent CurrentMapExtent;
        Rectangle MapWindowSize;
        double MapMinX, MapMinY;
        int WinW, WinH;
        double MapW, MapH;
        double ScaleX, ScaleY;

        public XView(XExtent _Extent, Rectangle _Rectangle)
        {
            Update(_Extent, _Rectangle);
        }
        public void UpdateExtent(XMapActions _Action)
        {
            CurrentMapExtent.Change(_Action);
            Update(CurrentMapExtent, MapWindowSize);
        }

        public void Update(XExtent _Extent, Rectangle _Rectangle)
        {
            CurrentMapExtent = _Extent;
            MapWindowSize = _Rectangle;
            MapMinX = CurrentMapExtent.GetMinX();
            MapMinY = CurrentMapExtent.GetMinY();
            WinW = MapWindowSize.Width;
            WinH = MapWindowSize.Height;
            MapW = CurrentMapExtent.GetWidth();
            MapH = CurrentMapExtent.GetHeight();
            ScaleX = MapW / WinW;
            ScaleY = MapH / WinH;
        }
        public Point ToScreenPoint(XVertex _Location)
        {
            if (FormXGIS.IsMecator)
            {
                double ScreenX =XTools.GetMecatorWidth (_Location.X, MapMinX) / ScaleX;
                double ScreenY = WinH - XTools.GetMecatorHeight(_Location.Y, MapMinY) / ScaleY;
                return new Point((int)ScreenX, (int)ScreenY);
            }
            else
            {
                double ScreenX = (_Location.X - MapMinX) / ScaleX;
                double ScreenY = WinH - (_Location.Y - MapMinY) / ScaleY;
                return new Point((int)ScreenX, (int)ScreenY);
            }
        }

        public XVertex ToMapVertex(Point _Point)
        {
            if (FormXGIS.IsMecator)
            {
                double MapX = XTools.GetMecatorWidth(ScaleX * _Point.X,- MapMinX);
                double MapY = XTools.GetMecatorHeight(ScaleY * (WinH - _Point.Y),-MapMinY);
                return new XVertex(MapX, MapY);
            }
            else
            {
                double MapX = ScaleX * _Point.X + MapMinX;
                double MapY = ScaleY * (WinH - _Point.Y) + MapMinY;
                return new XVertex(MapX, MapY);
            }
        }

        internal void UpdateExtent(XExtent extent)
        {
            CurrentMapExtent.CopyFrom(extent);
            Update(CurrentMapExtent, MapWindowSize);
        }

        internal int ToScreenDistance(double _MapDistance)
        {
            XVertex v1 = new XVertex(MapMinX, MapMinY);
            XVertex v2 = new XVertex(MapMinX + _MapDistance, MapMinY);

            Point p1 = ToScreenPoint(v1);
            Point p2 = ToScreenPoint(v2);
            
            return (int)Math.Sqrt((p1.X-p2.X)* (p1.X - p2.X)+ (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        internal Point[] ToScreenPoints(List<XVertex> allVertexes)
        {
            Point[] points = new Point[allVertexes.Count];
            for (int i = 0; i < allVertexes.Count; i++)
                points[i] = ToScreenPoint(allVertexes[i]);
            return points;
        }
    }

    public class XExtent
    {
        public XVertex BottomLeft;
        public XVertex UpRight;

        public XExtent(XVertex _BottomLeft, XVertex _UpRight)
        {
            BottomLeft = _BottomLeft;
            UpRight = _UpRight;
        }

        public double GetMinX()
        {
            return BottomLeft.X;
        }
        public double GetMaxX()
        {
            return UpRight.X;
        }
        public double GetMinY()
        {
            return BottomLeft.Y;
        }
        public double GetMaxY()
        {
            return UpRight.Y;
        }
        public double GetWidth()
        {
            if (FormXGIS.IsMecator)
            {
                return XTools.GetMecatorWidth(UpRight.X, BottomLeft.X);
            }
            else
            {
                return UpRight.X - BottomLeft.X;
            }
        }
        public double GetHeight()
        {
            if (FormXGIS.IsMecator)
            {
                return XTools.GetMecatorHeight(UpRight.Y,BottomLeft.Y);
            }
            else
            {
                return UpRight.Y - BottomLeft.Y;
            }
        }

        double ZoomingFactor = 2;
        double MovingFactor = 0.25;
        internal void Change(XMapActions action)
        {
            double newminx = BottomLeft.X, newminy = BottomLeft.Y,
                                       newmaxx = UpRight.X, newmaxy = UpRight.Y;
            switch (action)
            {
                case XMapActions.zoomin:
                    newminx = ((GetMinX() + GetMaxX()) - GetWidth() / ZoomingFactor) / 2;
                    newminy = ((GetMinY() + GetMaxY()) - GetHeight() / ZoomingFactor) / 2;
                    newmaxx = ((GetMinX() + GetMaxX()) + GetWidth() / ZoomingFactor) / 2;
                    newmaxy = ((GetMinY() + GetMaxY()) + GetHeight() / ZoomingFactor) / 2;
                    break;
                case XMapActions.zoomout:
                    newminx = ((GetMinX() + GetMaxX()) - GetWidth() * ZoomingFactor) / 2;
                    newminy = ((GetMinY() + GetMaxY()) - GetHeight() * ZoomingFactor) / 2;
                    newmaxx = ((GetMinX() + GetMaxX()) + GetWidth() * ZoomingFactor) / 2;
                    newmaxy = ((GetMinY() + GetMaxY()) + GetHeight() * ZoomingFactor) / 2;
                    break;
                case XMapActions.moveup:
                    newminy = GetMinY() - GetHeight() * MovingFactor;
                    newmaxy = GetMaxY() - GetHeight() * MovingFactor;
                    break;
                case XMapActions.movedown:
                    newminy = GetMinY() + GetHeight() * MovingFactor;
                    newmaxy = GetMaxY() + GetHeight() * MovingFactor;
                    break;
                case XMapActions.moveleft:
                    newminx = GetMinX() + GetWidth() * MovingFactor;
                    newmaxx = GetMaxX() + GetWidth() * MovingFactor;
                    break;
                case XMapActions.moveright:
                    newminx = GetMinX() - GetWidth() * MovingFactor;
                    newmaxx = GetMaxX() - GetWidth() * MovingFactor;
                    break;
            }
            UpRight.X = newmaxx;
            UpRight.Y = newmaxy;
            BottomLeft.X = newminx;
            BottomLeft.Y = newminy;
        }

        internal void CopyFrom(XExtent extent)
        {
            UpRight.CopyFrom(extent.UpRight);
            BottomLeft.CopyFrom(extent.BottomLeft);
        }
    }


    public class XAttribute
    {
        public ArrayList Values = new ArrayList();
        //private XAttribute xAttribute;

        public XAttribute(XAttribute a)
        {
            foreach (object value in a.Values)
                Values.Add(value);
        }

        public XAttribute()
        {

        }

        public void AddValue(object _Object)
        {
            Values.Add(_Object);
        }
        public object GetValue(int _Index)
        {
            return Values[_Index];
        }
        public void Draw(Graphics _Graphics, XView _View, XVertex _Location, int _Index)
        {
            Point screenPoint = _View.ToScreenPoint(_Location);
            _Graphics.DrawString(GetValue(_Index).ToString(), 
                new Font("宋体", 20),
                new SolidBrush(Color.Green), screenPoint);
        }
    }

    public abstract class XSpatial
    {
        public XVertex Centroid;
        public XExtent Extent;

        public abstract void Draw(Graphics _Graphics, XView _View);

        internal abstract double Distance(XVertex onevertex);
    }

    public class XFeature
    {
        public XSpatial Spatial;
        public XAttribute Attribute;

        public XFeature(XSpatial _Spatial, XAttribute _Attribute)
        {
            Spatial = _Spatial;
            Attribute = _Attribute;
        }

        public void Draw(Graphics _Graphics, XView _View, bool _DrawAttributeOrNot, int _Index)
        {
            Spatial.Draw(_Graphics, _View);
            if (_DrawAttributeOrNot)
                Attribute.Draw(_Graphics,_View, Spatial.Centroid, _Index);
        }
        public object GetAttribute(int _Index)
        {
            return Attribute.GetValue(_Index);
        }

        internal double Distance(XVertex onevertex)
        {
            return Spatial.Distance(onevertex);
        }
    }

    public class XVertex
    {
        public double X;
        public double Y;

        public XVertex(BinaryReader br)
        {
            X = br.ReadDouble();
            Y = br.ReadDouble();
        }

        public XVertex(double _X, double _Y)
        {
            X = _X;
            Y = _Y;
        }

        internal void CopyFrom(XVertex v)
        {
            X = v.X;
            Y = v.Y;
        }

        internal double Distance(XVertex _AnotherVertex)
        {
            return Math.Sqrt((X - _AnotherVertex.X) * (X - _AnotherVertex.X) +
                (Y - _AnotherVertex.Y) * (Y - _AnotherVertex.Y));
        }

        internal void WriteVertex(BinaryWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
        }
    }
    public class XPointSpatial:XSpatial
    {
        public XPointSpatial(XVertex _Location)
        {
            Centroid = _Location;
            Extent = new XExtent(_Location, _Location);
        }

        public override void Draw(Graphics _Graphics, XView _View)
        {
            Point screenPoint = _View.ToScreenPoint(Centroid);
            _Graphics.FillEllipse(new SolidBrush(Color.Red),
                new Rectangle(screenPoint.X - 3, screenPoint.Y- 3, 6, 6));

        }

        internal override double Distance(XVertex onevertex)
        {
            return Centroid.Distance(onevertex);
        }
    }
    public class XLineSpatial:XSpatial
    {
        public List<XVertex> AllVertexes;
        public double Length;

        public XLineSpatial(List<XVertex> _Vertexes)
        {
            Centroid = XTools.CalculateCentroid(_Vertexes);
            Extent = XTools.CalculateExtent(_Vertexes);
            AllVertexes = _Vertexes;
            Length = XTools.CalculateLength(_Vertexes);
        }

        public override void Draw(Graphics _Graphics, XView _View)
        {
            _Graphics.DrawLines(new Pen(Color.Red, 2),
                _View.ToScreenPoints(AllVertexes));
        }

        internal override double Distance(XVertex onevertex)
        {
            throw new NotImplementedException();
        }
    }

    public class XPolygonSpatial:XSpatial
    {
        public List<XVertex> AllVertexes;
        public double Area;

        public XPolygonSpatial(List<XVertex> _Vertexes)
        {
            Centroid = XTools.CalculateCentroid(_Vertexes);
            Extent = XTools.CalculateExtent(_Vertexes);
            AllVertexes = _Vertexes;
            Area = XTools.CalculateArea(_Vertexes);
        }
        public override void Draw(Graphics _Graphics, XView _View)
        {
            Point[] points = _View.ToScreenPoints(AllVertexes);
            _Graphics.FillPolygon(new SolidBrush(Color.Yellow), points);
            _Graphics.DrawPolygon(new Pen(Color.White, 2), points);
        }

        internal override double Distance(XVertex onevertex)
        {
            throw new NotImplementedException();
        }
    }

    public class XGeoJson
    {
        
        public List<XLayer> ReadJSONFile(string filename)
        {
            List<XLayer> layers = new List<XLayer>();
            StreamReader sr = new StreamReader(filename, Encoding.UTF8);
            Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.Linq.JObject.Parse(sr.ReadToEnd());//读入一个json文件
            Newtonsoft.Json.Linq.JToken features = o["features"];//读取所有空间对象数组
            List<Newtonsoft.Json.Linq.JToken> flst = features.ToList();
            List<XField> fields = new List<XField>();/*ReadFields(mfh.FieldCount, br);*/
            XLayer pointlayer = new XLayer(filename, SHAPETYPE.point, null, fields);
            XLayer linelayer = new XLayer(filename, SHAPETYPE.line, null, fields);
            XLayer polygonlayer = new XLayer(filename, SHAPETYPE.polygon, null, fields);
            double maxx = Double.MinValue;
            double minx = Double.MaxValue;
            double maxy = Double.MinValue;
            double miny = Double.MaxValue;
            for (int index = 0; index < flst.Count; index++)
            {
                List<XVertex> _Vertexes = new List<XVertex>();//记录feature的节点
                string featuretype = (string)(flst[index]["geometry"]["type"]);//读取对象类型 包括Point LineString Polygon
                Newtonsoft.Json.Linq.JToken properties = flst[index]["properties"];//读取对象的所有字段与属性

                //if (featuretype == "Polygon") flst[index]["geometry"]["coordinates"][0]
                if (featuretype == "Point")//如果是点对象 则按点对象的数据结构进行处理
                {
                    var location = flst[index]["geometry"]["coordinates"];
                    double lon = (double)location.First;
                    double lat = (double)location.Last;
                    Console.WriteLine(lon);
                    if (lon > maxx) maxx = lon;
                    else if (lon < minx) minx = lon;

                    if (lat > maxy) maxy = lat;
                    else if (lat < miny) miny = lat;

                    _Vertexes.Add(new XVertex(lon, lat));
                    ReadJSONField(properties, pointlayer);//右边返回一个Fields泛型赋给左边图层字段
                }

                else if (featuretype == "LineString")//如果是线对象 则按线对象的数据结构进行处理
                {
                    foreach (var location in flst[index]["geometry"]["coordinates"])
                    {
                        double lon = (double)location.First;
                        double lat = (double)location.Last;
                        //Console.WriteLine(lon);
                        if (lon > maxx) maxx = lon;
                        else if (lon < minx) minx = lon;

                        if (lat > maxy) maxy = lat;
                        else if (lat < miny) miny = lat;

                        _Vertexes.Add(new XVertex(lon, lat));
                        ReadJSONField(properties, linelayer);//右边返回一个Fields泛型赋给左边图层字段
                    }
                }

                else if (featuretype == "Polygon")//如果是多边形对象 则按多边形对象的数据结构进行处理
                {
                    foreach (var singlepoly in flst[index]["geometry"]["coordinates"])
                    {
                        foreach (var location in singlepoly)
                        {
                            double lon = (double)location.First;
                            double lat = (double)location.Last;
                            Console.WriteLine(lon);
                            if (lon > maxx) maxx = lon;
                            else if (lon < minx) minx = lon;

                            if (lat > maxy) maxy = lat;
                            else if (lat < miny) miny = lat;

                            _Vertexes.Add(new XVertex(lon, lat));
                            ReadJSONField(properties, polygonlayer);//右边返回一个Fields泛型赋给左边图层字段
                        }
                    }
                }


                if (featuretype == "Point")
                {
                    XFeature onefeature = new XFeature(new XPointSpatial(_Vertexes[0]), null);//暂时默认无属性
                    pointlayer.AddFeature(onefeature);//给线图层添加对象
                }
                else if (featuretype == "LineString")
                {
                    XFeature onefeature = new XFeature(new XLineSpatial(_Vertexes), null);//暂时默认无属性
                    linelayer.AddFeature(onefeature);//给线图层添加对象
                }
                else if (featuretype == "Polygon")
                {
                    XFeature onefeature = new XFeature(new XPolygonSpatial(_Vertexes), null);//暂时默认无属性
                    polygonlayer.AddFeature(onefeature);//给线图层添加对象
                }

            }
            //Console.WriteLine(minx.ToString(), miny.ToString(), maxx.ToString(), maxy.ToString());
            XExtent layersextent = new XExtent(new XVertex(minx, miny), new XVertex(maxx, maxy));
            layers.Add(polygonlayer);
            layers.Add(linelayer);
            layers.Add(pointlayer);
            foreach (XLayer ll in layers)
            {
                ll.Extent = layersextent;//给三种layer赋上共同的extent 
                ll.DrawAttributeOrNot = false;
            }
            sr.Close();
            return layers;
        }

        public void WriteJSONFile(XLayer layer, string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Create);
            StreamWriter writer = new StreamWriter(fsr);
            writer.Write("{");
            writer.Write("\"type\": \"" + "FeatureCollection" + "\",");
            writer.Write("\"features\": [");
            //开始写入所有的feature
            foreach (XFeature feature in layer.Features)
            {
                writer.Write("{");
                writer.Write("\"type\": \"" + "Feature" + "\",");
                //写字段与属性
                writer.Write("\"properties\": {");
                for (int fcount = 0; fcount < layer.Fields.Count; fcount++)
                {
                    writer.Write("\"" + layer.Fields[fcount].name + "\": \"" +
                        (feature.Attribute.Values[fcount]).ToString() + "\"");
                    if (fcount != layer.Fields.Count - 1) writer.Write(",");
                }

                writer.Write("},");
                //写地理信息
                writer.Write("\"geometry\": {");
                string ftype = null;
                if ((int)layer.ShapeType == 1)  ftype = "Point";
                else if ((int)layer.ShapeType == 3) ftype = "LineString";
                else if ((int)layer.ShapeType == 5) ftype = "Polygon";

                writer.Write("\"type\": \"" + ftype + "\",");
                writer.Write("\"coordinates\": [");
                if ((int)layer.ShapeType == 1)//如果是点，则按照写点坐标的方式写
                {
                    writer.Write((feature.Spatial.Centroid.X).ToString() + ","
                        + (feature.Spatial.Centroid.Y).ToString());
                }
                else if ((int)layer.ShapeType == 3)//如果是线，则按照写线中点坐标的方式写
                {
                    for (int i = 0; i < ((XLineSpatial)feature.Spatial).AllVertexes.Count; i++)
                    {
                        writer.Write("["+(((XLineSpatial)feature.Spatial).AllVertexes[i].X).ToString() + ","
                        + (((XLineSpatial)feature.Spatial).AllVertexes[i].Y).ToString()+"]");
                        if (i < ((XLineSpatial)feature.Spatial).AllVertexes.Count - 1) writer.Write(",");
                    }
                }
                else if ((int)layer.ShapeType == 5)//如果是面，则按照写面中点坐标的方式写
                {
                    writer.Write("[");
                    for (int i = 0; i < ((XPolygonSpatial)feature.Spatial).AllVertexes.Count; i++)
                    {
                        writer.Write("[" + (((XPolygonSpatial)feature.Spatial).AllVertexes[i].X).ToString() + ","
                        + (((XPolygonSpatial)feature.Spatial).AllVertexes[i].Y).ToString() + "]");
                        if (i < ((XPolygonSpatial)feature.Spatial).AllVertexes.Count - 1) writer.Write(",");
                    }
                    writer.Write("]");
                }

                writer.Write("]");

                writer.Write("}");

                writer.Write("}");
                if(feature != layer.Features[layer.Features.Count-1]) writer.Write(",");
            }

            writer.Write("]");
            writer.Write("}");
            writer.Close();
            fsr.Close();
        }

        void ReadJSONField(Newtonsoft.Json.Linq.JToken properties,XLayer layer)
        {
            if (properties!=null)
            {
                foreach (Newtonsoft.Json.Linq.JProperty fa in properties)
                {
                    //Newtonsoft.Json.Linq.JProperty fieldname = (Newtonsoft.Json.Linq.JProperty)fa.ElementAt(0);
                    //var fieldname = (string)fa.ElementAt(0);
                    string name = fa.Name;
                    XField newfield = new XField(typeof(String), name);
                    if (!(layer.Fields.Contains(newfield)))//图层若不存在该field则加入该field
                    {
                        layer.Fields.Add(newfield);
                    }
                }
            }
            
        }

    }

}
