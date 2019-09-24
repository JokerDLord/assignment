using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

namespace assignment
{
    class GISVertex //节点类拥有算距离的方法 
    {
        public double x;
        public double y;

        public GISVertex(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public double Distance(GISVertex anothervertex)
        {
            return Math.Sqrt((x - anothervertex.x) * (x - anothervertex.x)
                - (y - anothervertex.y) * (y - anothervertex.y));
        }
    }
    class GISPoint : GISSpatial
    {
        //public GISVertex Location; **previous code
        //public string Attribute;

        public GISPoint(GISVertex onevertex)
        {
            centroid = onevertex;
            extent = new GISExtent(onevertex, onevertex);
        }
        public override void draw(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.Red),
                new Rectangle((int)(centroid.x) - 3, (int)(centroid.y) - 3, 6, 6));
        }
        public double Distance(GISVertex anothervertex)
        {
            return centroid.Distance(anothervertex);
        }

        public override GISVertex endNode()
        {
            return centroid;
        }
    }
    class GISLine : GISSpatial
    {
        List<GISVertex> AllVertexes;
        public GISLine(List<GISVertex> _vertexes)
        {
            AllVertexes = _vertexes;
        }
        public override void draw(Graphics graphics)
        {
            Point[] points = GISSpatial.Vertexes2Points(AllVertexes);
            if (points.Length == 1) return; //只有一个点直接跳出
            graphics.DrawLines(new Pen(Color.Black, 1), points);
        }

        public override GISVertex endNode() //绘制属性的节点位置
        {
            return AllVertexes[AllVertexes.Count - 1];
        }
    }
    class GISPolygon : GISSpatial
    {
        List<GISVertex> AllVertexes;
        public GISPolygon(List<GISVertex> _vertexes)
        {
            AllVertexes = _vertexes;
        }
        public override void draw(Graphics graphics)
        {
            Point[] points = GISSpatial.Vertexes2Points(AllVertexes);
            if (points.Length < 3)
                return; //只有一个点直接跳出
            graphics.FillPolygon(new SolidBrush(Color.Pink), points);
            graphics.DrawPolygon(new Pen(Color.Black, 2), points);
        }

        public override GISVertex endNode()
        {
            return AllVertexes[AllVertexes.Count - 1];
        }
    }


    class GISFeature
    {
        public GISSpatial spatialpart;
        public GISAttribute attributepart; //空间与属性信息

        public GISFeature(GISSpatial spatial, GISAttribute attribute) //构造函数传入空间与属性信息
        {
            spatialpart = spatial;
            attributepart = attribute;
        }
        public void draw(Graphics graphics, bool DrawAttributeOrNot, int index)
        //画空间与属性信息
        {
            spatialpart.draw(graphics);
            if (DrawAttributeOrNot)
            {
                /*
                if (spatialpart is GISLine)
                    attributepart.draw2(graphics, spatialpart.endNode(), index);
                else if (spatialpart is GISPolygon)
                    attributepart.draw3(graphics, spatialpart.endNode(), index);
                else
                */
                attributepart.draw(graphics, spatialpart.endNode(), index);
            }
            //此处引用了attributed的方法
        }
        public object getAttribute(int index)//获取属性值
        {
            return attributepart.GetValue(index);
        }
    }

    class GISAttribute
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
        public void draw(Graphics graphics, GISVertex location, int index)
        {
            graphics.DrawString(values[index].ToString(), new Font("宋体", 20),
                new SolidBrush(Color.Green), new PointF((int)(location.x), (int)(location.y)));
        }
        /*
        public void draw2(Graphics graphics, GISVertex location, int index) //线属性绘制方法
        {
            graphics.DrawString(values[index].ToString(), new Font("宋体", 20),
                new SolidBrush(Color.Green), new PointF((int)(location.x), (int)(location.y)));
        }
        public void draw3(Graphics graphics, GISVertex location, int index) //面属性的绘制方法
        {
            graphics.DrawString(values[index].ToString(), new Font("宋体", 20),
                new SolidBrush(Color.Green), new PointF((int)(location.x), (int)(location.y)));
        }
        */
    }

    abstract class GISSpatial //抽象的空间信息类
    {
        public GISVertex centroid; //中心点 属于节点类的对象
        public GISExtent extent;   //空间范围 最小外接矩形
        public abstract void draw(Graphics graphics);
        public static Point[] Vertexes2Points(List<GISVertex> vertexes)
        {
            Point[] points = new Point[vertexes.Count];
            for (int i = 0; i < vertexes.Count; i++)
            {
                points[i] = new Point((int)vertexes[i].x, (int)vertexes[i].y);
            }
            return points;
        }

        public abstract GISVertex endNode();
    }//有了抽象的空间spatial类之后 就可以重新定义三个对象实体类

    class GISExtent //这是一个重要的类 空间范围
    {
        public GISVertex bottomleft;
        public GISVertex upright; //外接矩形的左下和右上角的节点
        public GISExtent(GISVertex _bottomleft, GISVertex _upright)
        {
            bottomleft = _bottomleft;
            upright = _upright;
        }
    }
}
