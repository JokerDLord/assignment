﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MYGIS;

namespace Lesson_3
{
    public partial class Form1 : Form
    {
        List<GISFeature> features = new List<GISFeature>();
        GISView view = null;
        public Form1()
        {
            InitializeComponent();
            view = new GISView(new GISExtent(new GISVertex(0, 0), new GISVertex(500, 500)), ClientRectangle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(tbX.Text);
            double y = Convert.ToDouble(tbY.Text);
            GISVertex onevertex = new GISVertex(x, y);
            GISPoint onepoint = new GISPoint(onevertex);
            //获取属性信息
            string attribute = tbAttribute.Text;
            GISAttribute oneattribute = new GISAttribute();
            oneattribute.AddValue(attribute);
            //新建一个GISFeature 并添加到features数组中
            GISFeature onefeature = new GISFeature(onepoint, oneattribute);
            features.Add(onefeature);
            //画出这个GISFeature
            Graphics graphics = this.CreateGraphics();
            onefeature.draw(graphics, view, true, 0); 
            //参数分别是画笔 是否绘制属性 属性列表values的索引
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {//点击空间对象显示属性信息
            //根据鼠标的点击创建节点信息
            if (features.Count == 0) return;
            GISVertex mouselocation = view.ToMapVertex(e.Location);
            double mindistance = Double.MaxValue;
            int id = -1;
            //通过遍历找出features数组中元素的中心点与点击位置最近的点
            for (int i = 0; i < features.Count; i++)
            {
                double onedistance = features[i].spatialpart.centroid.Distance(mouselocation);
                if (onedistance < mindistance)
                {
                    id = i;
                    mindistance = onedistance;
                }
            }
            //判断是否存在空间对象

            Point nearestpoint = view.ToScreenPoint(features[id].spatialpart.centroid);
            int screendistance = ScreenDistance(e.Location,nearestpoint);
            if (screendistance < 5)
            {
                MessageBox.Show("该空间对象的属性是 " + features[id].getAttribute(0).ToString());
            }            
        }

        private int ScreenDistance(Point _P1, Point _P2)
        {
            return Math.Abs(_P1.X - _P2.X) + Math.Abs(_P1.Y - _P2.Y);
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            //从文本框获取更新的地图范围
            double minx = Double.Parse(tbMinX.Text);
            double miny = Double.Parse(tbMinY.Text);
            double maxx = Double.Parse(tbMaxX.Text);
            double maxy = Double.Parse(tbMaxY.Text);
            //更新view
            view.Update(new GISExtent(minx, maxx, miny, maxy), ClientRectangle);
            UpdateMap();

        }

        private void UpdateMap()
        {
            Graphics graphics = CreateGraphics();
            //用黑色填充整个窗口
            graphics.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            //根据新的view在窗口上绘制数组中的每个空间对象
            for (int i = 0; i < features.Count; i++)
            {
                features[i].draw(graphics, view, true, 0);
            }
            graphics.Dispose();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void random_Click(object sender, EventArgs e)
        {

        }

        private void tbrandom_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(tbrandom.Text);
            Random randobj = new Random();
            List<GISFeature> randgisfeature = new List<GISFeature>();
            randgisfeature = GetRandomFeature(randobj, randgisfeature, number);
            //画出所有的gisfeature
            Graphics graphics = this.CreateGraphics();
            foreach (GISFeature onefeature in randgisfeature)
            {
                onefeature.draw(graphics, view, true, 1);
            }
            //onefeature.draw(graphics, view, true, 0);

        }

        private List<GISFeature> GetRandomFeature(Random randobj, List<GISFeature> randgisfeature, int number) 
        {
            //根据指定数量创造随机gisfeature对象
            for (int i = 0; i < number; i++)
            {
                int lon = randobj.Next(-180, 180);
                int lat = randobj.Next(-85, 85);
                GISVertex onevertex = new GISVertex(lon, lat);
                GISPoint onepoint = new GISPoint(onevertex);
                //获取属性信息
                string attribute1 = lon.ToString()+","+lat.ToString();
                string attribute2 = "地理坐标为：" + onevertex.mercatorx.ToString() + "," + onevertex.mercatory.ToString();
                GISAttribute oneattribute = new GISAttribute();
                oneattribute.AddValue(attribute1);
                oneattribute.AddValue(attribute2);
                //新建一个GISFeature 并添加到features数组中
                GISFeature onefeature = new GISFeature(onepoint, oneattribute);
                randgisfeature.Add(onefeature);
            }
            return randgisfeature;

        }

        private void tbY_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
