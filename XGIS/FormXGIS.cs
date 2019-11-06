using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XGIS
{
    public partial class FormXGIS : Form
    {
        XView view;
        XLayer layer;
        public static bool IsMecator = false; 
        public FormXGIS()
        {
            InitializeComponent();
            view = new XView(
                    new XExtent(
                        new XVertex(0, 0),
                        new XVertex(1, 1)),
                    this.ClientRectangle
                );
        }


        private void FormXGIS_MouseClick(object sender, MouseEventArgs e)
        {
            if (layer.FeatureCount() == 0) return;
            XVertex onevertex = view.ToMapVertex(e.Location);
            double mindistance = Double.MaxValue;
            int findid = -1;
            int index = 0;
            foreach(XFeature feature in layer.Features)
            {
                double distance = feature.Distance(onevertex);
                if (distance < mindistance)
                {
                    mindistance = distance;
                    findid = index;
                }
                index++;
            }
            int ScreenDistance = view.ToScreenDistance(mindistance);
            if (ScreenDistance < 5)
                MessageBox.Show("找到的Feature的序号是"+ findid);
        }

        //private int ScreenDistance(Point _P1, Point _P2)
        //{
        //    return Math.Abs(_P1.X - _P2.X) + Math.Abs(_P1.Y - _P2.Y);
        //}

        private void BUpdateMap_Click(object sender, EventArgs e)
        {
            ////从文本框中获取新的地图范围
            //double minx = Double.Parse(tbMinX.Text);
            //double miny = Double.Parse(tbMinY.Text);
            //double maxx = Double.Parse(tbMaxX.Text);
            //double maxy = Double.Parse(tbMaxY.Text);
            ////更新view
            //view.Update(
            //    new XExtent(
            //        new XVertex(minx, miny), 
            //        new XVertex(maxx, maxy)), 
            //    ClientRectangle);
            ////更新地图
            //UpdateMap();
        }

        private void UpdateMap()
        {
            Graphics graphics = CreateGraphics();
            //用黑色填充整个窗口
            graphics.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            //根据新的view在绘图窗口中画上数组中的每个空间对象
            layer.Draw(graphics, view);
            graphics.Dispose();
        }

        private void MapExplore(object sender, EventArgs e)
        {
            XMapActions action = XMapActions.none;

            if ((Button)sender == bZoomIn) action = XMapActions.zoomin;
            else if ((Button)sender == bZoomOut) action = XMapActions.zoomout;
            else if ((Button)sender == bMoveUp) action = XMapActions.moveup;
            else if ((Button)sender == bMoveDown) action = XMapActions.movedown;
            else if ((Button)sender == bMoveLeft) action = XMapActions.moveleft;
            else if ((Button)sender == bMoveRight) action = XMapActions.moveright;

            view.UpdateExtent(action);
            UpdateMap();

        }

        private void BOpenShapeFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Shapefile文件|*.shp";
            openFileDialog.RestoreDirectory = false;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            XShapeFile sf = new XShapeFile();
            layer = sf.ReadShapefile(openFileDialog.FileName);
            layer.DrawAttributeOrNot = false;
            MessageBox.Show("读入" + layer.FeatureCount() + "个实体");
            view.UpdateExtent(layer.Extent);
            UpdateMap();
        }

        private void BFullExtent_Click(object sender, EventArgs e)
        {
            view.UpdateExtent(layer.Extent);
            UpdateMap();
        }

        private void BOpenAttribute_Click(object sender, EventArgs e)
        {
            FormAttribute form = new FormAttribute(layer);
            form.Show();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void DeleteField_Click(object sender, EventArgs e)
        {
            string fieldname = textBox_del.Text;
            for (int i = layer.Fields.Count-1; i >= 0; i--)
            {
                if (layer.Fields[i].name == fieldname)
                {
                    layer.Fields.Remove(layer.Fields[i]);
                    //删除layer每个feature要素对应被删字段的值
                    int atrlength = layer.Features[0].Attribute.Values.Count;
                    foreach (XFeature feature in layer.Features)
                    {
                        if (feature.Attribute.Values.Count != atrlength) continue;//如果已经修改过feature的属性则继续下一个层循环
                        feature.Attribute.Values.RemoveAt(i);//删除对应字段的属性值
                    }
                    //layer.Features[0].Attribute.deleteValue(i);
                    break;
                }
                
            }
            FormAttribute form = new FormAttribute(layer);
            form.Show();
            MessageBox.Show(fieldname + " is successfully deleted from the layer's field");
        }

        private void TextBox_delete_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormXGIS_Load(object sender, EventArgs e)
        {

        }

        private void AddField_Click(object sender, EventArgs e)
        {
            string fieldname = textBox_add.Text;
            XField addfield = new XField(Type.GetType("System.String"), fieldname);//默认为string类型
            layer.Fields.Add(addfield);
            //增加layer每个feature要素对应需要增加字段的值
            int atrlength = layer.Features[0].Attribute.Values.Count;
            foreach (XFeature feature in layer.Features)
            {
                if (feature.Attribute.Values.Count != atrlength) continue;//如果已经修改过feature的属性则继续下一个层循环
                feature.Attribute.Values.Add(null);//增加对应字段的属性值 初始为null
            }
            FormAttribute form = new FormAttribute(layer);
            form.Show();
            MessageBox.Show(fieldname + " is successfully added into the layer's field");
        }

        private void ChangeFieldName_Click(object sender, EventArgs e)
        {
            string oldfieldname = textBox_oldField.Text;
            string newfieldname = textBox_newField.Text;
            for (int i = layer.Fields.Count - 1; i >= 0; i--)
            {
                if (layer.Fields[i].name == oldfieldname)
                {
                    layer.Fields[i].name = newfieldname;
                }
            }
            FormAttribute form = new FormAttribute(layer);
            form.Show();
            MessageBox.Show("field " + oldfieldname + " is successfully change to " + newfieldname);
        }

        private void TextBox_add_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Moveforward_Click(object sender, EventArgs e)
        {
            string fieldname = textBox_movefield.Text;
            for (int i = layer.Fields.Count - 1; i >= 0; i--)
            {
                if (layer.Fields[i].name == fieldname)
                {
                    int transfield = i - 1;
                    if (sender == moveforward)
                    {
                        transfield = i - 1;
                    }
                    else if (sender == moveback)
                    {
                        transfield = i + 1;
                    }
                    //交换字段名
                    string t = layer.Fields[i].name;
                    layer.Fields[i].name = layer.Fields[transfield].name;
                    layer.Fields[transfield].name = t;

                    Object attrpre = null;//layer.Features[0].Attribute.Values[i];
                    foreach (XFeature feature in layer.Features)
                    {
                        if (feature.Attribute.Values[i] != attrpre) //如果属性还未进行移动交换
                        {
                            Object to = feature.Attribute.Values[i];
                            feature.Attribute.Values[i] = feature.Attribute.Values[transfield];
                            feature.Attribute.Values[transfield] = to;
                        }
                        attrpre = feature.Attribute.Values[i];
                    }
                    break;//一定要跳出循环
                }
            }

            FormAttribute form = new FormAttribute(layer);
            form.Show();

        }
    }
}
