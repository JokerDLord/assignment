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
            //如果选择的是默认的分位数分级方法
            if (cbleveltype.SelectedIndex == 0)
            {
                if (layer.MakeGradualColor(cbattribute.SelectedIndex, Int32.Parse(tblevelnumber.Text)) == false)
                {
                    MessageBox.Show("基于该属性无法绘制分层设色地图!!");
                    return;
                }
            }
            //如果选择的是等间隔分级方法
            else if (cbleveltype.SelectedIndex == 1)
            {
                if (layer.MakeGradualColorByGap(cbattribute.SelectedIndex, Int32.Parse(tblevelnumber.Text)) == false)
                {
                    MessageBox.Show("基于该属性无法绘制等间隔的分层设色地图!!");
                    return;
                }

            }

            //更新地图绘制
            Mapwindow.UpdateMap();
        }

        private void AdvancedThematic_Shown(object sender, EventArgs e)
        {
            //当窗口显示时，即根据图层读取字段，填充到cbattribute里面
            for (int i = 0; i < layer.Fields.Count; i++)
            {
                Console.WriteLine(layer.Fields[i].name);
                cbattribute.Items.Add(layer.Fields[i].name);
                cbattributedot.Items.Add(layer.Fields[i].name);
            }
            //cbattribute.SelectedIndex = (layer.Fields.Count > 0) ? layer.ThematicFieldIndex : -1;
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void btchangedotdensity_Click(object sender, EventArgs e)
        {
            int dotdensity = Convert.ToInt32(tbdotdensity.Text);
            int dotsize = Convert.ToInt32(tbdotsize.Text);
            Color dotcolor = btdotfillcolor.BackColor;
            
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
    }
}
