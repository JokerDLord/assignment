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
    //删除 添加 改名 更改顺序
    public partial class FormAttribute : Form
    {
        public FormAttribute(XLayer _Layer)
        {
            InitializeComponent();
            for (int i = 0; i < _Layer.Fields.Count; i++)
            {
                dgvAttribute.Columns.Add(_Layer.Fields[i].name, _Layer.Fields[i].name);
            }
            for (int i = 0; i < _Layer.FeatureCount(); i++)
            {
                dgvAttribute.Rows.Add();
                for (int j = 0; j < _Layer.Fields.Count; j++)
                {
                    dgvAttribute.Rows[i].Cells[j].Value = _Layer.GetFeature(i).GetAttribute(j);
                }
            }

        }
    }
}
