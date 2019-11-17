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


        BufferedGraphicsContext backWindow;
        BufferedGraphics backMap;

        public FormXGIS()
        {
            InitializeComponent();
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);

            backWindow = BufferedGraphicsManager.Current;

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
            //计时器
            DateTime pretime = System.DateTime.Now;
            //先清空之前占用的绘图资源
            if (backMap != null)
            {
                if (backMap.Graphics != null)
                    backMap.Graphics.Dispose();
                backMap.Dispose();
            }
            //初始化绘图资源
            Graphics frontGraphics = CreateGraphics();
            backMap = backWindow.Allocate(frontGraphics, ClientRectangle);
            frontGraphics.Dispose();
            //在背景窗口中绘图
            Graphics graphics = backMap.Graphics;
            graphics.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            layer.Draw(graphics, view);
            //把绘图内容搬到前端
            Invalidate();
            DateTime lattime = System.DateTime.Now;
            TimeSpan counter = lattime.Subtract(pretime);
            toolStripStatusLabel1counter.Text = "Display Operation Time:" + counter.ToString();
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

        private void BWriteMyFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;
            XMyFile myFile = new XMyFile();
            myFile.WriteFile(layer, sfd.FileName);
            MessageBox.Show("完成");

        }

        private void BReadMyFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            XMyFile myFile = new XMyFile();
            layer = myFile.ReadFile(openFileDialog.FileName);
            MessageBox.Show("读入" + layer.FeatureCount() + "个实体");
            view.UpdateExtent(layer.Extent);
            UpdateMap();
        }

        private void FormXGIS_Paint(object sender, PaintEventArgs e)
        {
            if (backMap != null)
                backMap.Render(e.Graphics);

        }

        private void FormXGIS_SizeChanged(object sender, EventArgs e)
        {
            if (layer == null) return;
            if (ClientRectangle.Width == 0 || ClientRectangle.Height == 0) return;
            view.UpdateWindowSize(ClientRectangle);
            UpdateMap();

        }

        private void ToolStripStatusLabel2mouselocation_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void FormXGIS_MouseMove(object sender, MouseEventArgs e)
        {
            XVertex v = view.ToMapVertex(e.Location);
            toolStripStatusLabel2mouselocation.Text = "mouse pointer@" + v.X.ToString() + "|" + v.Y.ToString();
        }
    }
}
