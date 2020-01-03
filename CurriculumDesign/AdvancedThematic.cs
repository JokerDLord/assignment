using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYGIS
{
    public partial class AdvancedThematic : Form
    {
        GISDocument Document;
        GISPanel Mapwindow;
        GISPanel PreviewWindow;
        GISLayer layer;
        public AdvancedThematic(GISDocument _document, GISPanel mapwindow, GISLayer _layer)
        {
            InitializeComponent();
            Document = _document;
            Mapwindow = mapwindow;
            layer = _layer;
        }

        private void Btchangethematic_Click(object sender, EventArgs e)
        {
            Color maxvcolor = btmaxvcolor.BackColor;
            Color minvcolor = btminvcolor.BackColor;
            //如果选择的是默认的分位数分级方法
            if (cbleveltype.SelectedIndex == 0)
            {
                if (layer.MakeGradualColor(cbattribute.SelectedIndex, Int32.Parse(tblevelnumber.Text), maxvcolor, minvcolor) == false)
                {
                    MessageBox.Show("基于该属性无法绘制分层设色地图!!");
                    return;
                }
            }
            //如果选择的是等间隔分级方法
            else if (cbleveltype.SelectedIndex == 1)
            {
                if (layer.MakeGradualColorByGap(cbattribute.SelectedIndex, Int32.Parse(tblevelnumber.Text), maxvcolor, minvcolor) == false)
                {
                    MessageBox.Show("基于该属性无法绘制等间隔的分层设色地图!!");
                    return;
                }

            }

            //更新地图绘制
            if (sender.Equals(preview)) PreviewWindow.UpdateMap();
            else if (sender.Equals(btchangethematic)) Mapwindow.UpdateMap();
                
        }

        private void AdvancedThematic_Shown(object sender, EventArgs e)
        {
            //当窗口显示时，即根据图层读取字段，填充到cbattribute里面
            for (int i = 0; i < layer.Fields.Count; i++)
            {
                Console.WriteLine(layer.Fields[i].name);
                cbattribute.Items.Add(layer.Fields[i].name);
                cbattributedot.Items.Add(layer.Fields[i].name);
                cbattributeEsymbol.Items.Add(layer.Fields[i].name);
            }
            //cbattribute.SelectedIndex = (layer.Fields.Count > 0) ? layer.ThematicFieldIndex : -1;
            //填充预览窗口
            Mapwindow.CloneGP(gisPanel1advanced);
            gisPanel1advanced.UpdateMap();
            PreviewWindow = gisPanel1advanced;
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            
        }

        private void btchangedotdensity_Click(object sender, EventArgs e)
        {
            int dotdensity = Convert.ToInt32(tbdotdensity.Text);
            int dotsize = Convert.ToInt32(tbdotsize.Text);
            Color dotcolor = btdotfillcolor.BackColor;
            //获取归一化后的属性数据 作为每一个控件对象内点数量的权重
            List<int> normalizations = layer.getNormalized(cbattributedot.SelectedIndex, dotdensity);
            if (normalizations == null)
            {
                MessageBox.Show("基于该属性无法绘制点密度地图!!");
                return;
            }

            //新建一个dotlayer，在点密度中的点将以一个点图层的形式显示出来
            GISLayer dotlayer = new GISLayer("dotdensity", SHAPETYPE.point, layer.Extent);
            dotlayer.Selectable = false;//设置为不可选择
            dotlayer.ThematicType = THEMATICTYPE.DotDensity;//设置为点密度型专题地图
            dotlayer.defaultThmatic = new GISThematic(dotcolor, dotsize, dotcolor);
            List<GISFeature> dotpoints = GISTools.MakeRandomDensityDot(layer,
                cbattributedot.SelectedIndex, normalizations);
            dotlayer.Features = dotpoints;

            //先移除之前已有的点密度图层
            for (int i = Document.layers.Count-1; i > 0; i--)
            {
                if (Document.layers[i].Name == "dotdensity") Document.layers.RemoveAt(i);
            }
            Document.layers.Add(dotlayer);//将dotlayer添加到document中 在更新地图绘制时会自动绘制layers里面的所有图层
            //更新地图绘制
            if (sender.Equals(previewds)) PreviewWindow.UpdateMap();
            else if (sender.Equals(btchangedotdensity)) Mapwindow.UpdateMap();
        }


        private void SettingColor_Click(object sender, EventArgs e)
        {
            ColorDialog colord = new ColorDialog();
            colord.Color = ((Button)sender).BackColor;
            if (colord.ShowDialog() == DialogResult.OK)
            {
                //在打开的颜色对话框中设置颜色，并用新的颜色更新按钮背景
                ((Button)sender).BackColor = colord.Color;
                //Clicked(sender, e);
            }
        }

        private void AdvancedThematic_Load(object sender, EventArgs e)
        {

        }

        private void BtchangeEsymbol_Click(object sender, EventArgs e)
        {
            int symbolsize = Convert.ToInt32(tbsymbolsize.Text);
            int cls = Convert.ToInt32(tbclass.Text);
            Color symbolcolor = btsymbolfillcolor.BackColor;
            List<int> sizes = layer.getSymbolSize(cbattributeEsymbol.SelectedIndex, symbolsize, cls);

            //新建一个esymbollayer，在点密度中的点将以一个点图层的形式显示出来
            GISLayer esymbollayer = new GISLayer("Esymbol", SHAPETYPE.point, layer.Extent);
            esymbollayer.Selectable = false;//设置为不可选择
            esymbollayer.ThematicType = THEMATICTYPE.EqualSymbol;//设置为等比变换型专题地图
            //为esymbollayer中每一个对象设置一个thematic
            for (int i = 0; i < layer.Features.Count; i++)
            {
                esymbollayer.Thematics.Add(i,new GISThematic(symbolcolor, sizes[i], symbolcolor));
                Console.WriteLine(sizes[i]);
            }

            //创建symbols点对象，作为esymbollayer的features
            //默认将符号放于polygon的重心位置
            List<GISFeature> symbols = new List<GISFeature>();
            for (int i = 0; i < layer.Features.Count; i++)
            {
                GISPolygon polygon = (GISPolygon)layer.Features[i].spatialpart;
                GISVertex vertex = new GISVertex(polygon.centroid);
                symbols.Add(new GISFeature(new GISPoint(vertex), null));
            }

            esymbollayer.Features = symbols;

            //先移除之前已有的点密度图层
            for (int i = Document.layers.Count - 1; i > 0; i--)
            {
                if (Document.layers[i].Name == "Esymbol") Document.layers.RemoveAt(i);
            }
            Document.layers.Add(esymbollayer);//将esymbollayer添加到document中 在更新地图绘制时会自动绘制layers里面的所有图层
            //更新地图绘制
            if (sender.Equals(previewes)) PreviewWindow.UpdateMap();
            else if (sender.Equals(btchangeEsymbol)) Mapwindow.UpdateMap();
            

        }

        private void Label21_Click(object sender, EventArgs e)
        {

        }
    }
}
