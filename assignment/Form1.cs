using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment
{
    public partial class Form1 : Form
    {
        List<GISVertex> linevertexes = new List<GISVertex>(); //声明一个节点数组 用以储存线的节点
        List<GISVertex> polyvertexes = new List<GISVertex>(); //声明一个节点数组 用以储存面的节点
        List<GISFeature> features = new List<GISFeature>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            GISVertex onevertex = new GISVertex(x, y);
            GISPoint onepoint = new GISPoint(onevertex);


            //获取属性信息
            string attribute = textBox3.Text;
            GISAttribute oneattribute = new GISAttribute();
            oneattribute.AddValue(attribute);

            //新建一个GISFeature 并添加到features数组中
            GISFeature onefeature = new GISFeature(onepoint, oneattribute);
            features.Add(onefeature);

            //画出这个GISFeature
            Graphics graphics = this.CreateGraphics();
            onefeature.draw(graphics, true, 0);
            //参数分别是画笔 是否绘制属性 属性列表values的索引

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            GISVertex onevertex = new GISVertex(x, y);
            linevertexes.Add(onevertex);
            GISLine oneline  = new GISLine(linevertexes);


            //获取属性信息
            //string attribute = textBox3.Text;
            //GISAttribute oneattribute = new GISAttribute();
            //oneattribute.AddValue(attribute);

            //新建一个GISFeature 并添加到features数组中
            GISFeature onefeature = new GISFeature(oneline, null);
            features.Add(onefeature);

            //画出这个GISFeature
            Graphics graphics = this.CreateGraphics();
            onefeature.draw(graphics, false, 0); //显然此处不画属性
            //参数分别是画笔 是否绘制属性 属性列表values的索引
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            GISVertex onevertex = new GISVertex(x, y);
            polyvertexes.Add(onevertex);
            GISPolygon onepoly = new GISPolygon(polyvertexes);


            //获取属性信息
            //string attribute = textBox3.Text;
            //GISAttribute oneattribute = new GISAttribute();
            //oneattribute.AddValue(attribute);

            //新建一个GISFeature 并添加到features数组中
            GISFeature onefeature = new GISFeature(onepoly, null);
            features.Add(onefeature);

            //画出这个GISFeature
            Graphics graphics = this.CreateGraphics();
            onefeature.draw(graphics, false, 0);
            //参数分别是画笔 是否绘制属性 属性列表values的索引

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {//点击空间对象显示属性信息
            //根据鼠标的点击创建节点信息
            GISVertex onevertex = new GISVertex((double)e.X, (double)e.Y);
            double mindistance = Double.MaxValue;
            int findid = -1;
            //通过循环计算找出features数组中元素的中心点与点击位置最近的点
            for (int i = 0; i < features.Count; i++)
            {
                double distance = features[i].spatialpart.centroid.Distance(onevertex);
                if (distance < mindistance)
                {
                    mindistance = distance;
                    findid = i;
                }
            }
            if (mindistance > 5 || findid == -1)
            {
                MessageBox.Show("没有点实体或鼠标点击位置不准确！");
            }
            else
                MessageBox.Show(features[findid].getAttribute(0).ToString());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //获取属性信息
            string attribute = textBox3.Text;
            GISAttribute oneattribute = new GISAttribute();
            oneattribute.AddValue(attribute);

            GISLine oneline = new GISLine(linevertexes); //创建line作为空间对象找到endnode作为属性绘制的位置
            Graphics graphics = this.CreateGraphics();
            oneattribute.draw(graphics, oneline.endNode(), 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //获取属性信息
            string attribute = textBox3.Text;
            GISAttribute oneattribute = new GISAttribute();
            oneattribute.AddValue(attribute);

            GISPolygon onepoly = new GISPolygon(polyvertexes);
            Graphics graphics = this.CreateGraphics();
            oneattribute.draw(graphics, onepoly.endNode(), 0);
        }
    }
}
