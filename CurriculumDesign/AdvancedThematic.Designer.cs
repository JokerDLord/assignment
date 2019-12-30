namespace MYGIS
{
    partial class AdvancedThematic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbleveltype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tblevelnumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbattribute = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btchangethematic = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbleveltype
            // 
            this.cbleveltype.FormattingEnabled = true;
            this.cbleveltype.Items.AddRange(new object[] {
            "分位数分级法(原默认方法)",
            "等范围分级法(Equal Interval)",
            "自定义间隔法(Define Interval)"});
            this.cbleveltype.Location = new System.Drawing.Point(178, 22);
            this.cbleveltype.Name = "cbleveltype";
            this.cbleveltype.Size = new System.Drawing.Size(148, 20);
            this.cbleveltype.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "分层设色地图分级方法";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 425);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btchangethematic);
            this.tabPage1.Controls.Add(this.tblevelnumber);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.cbattribute);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cbleveltype);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(760, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "分级设色地图(更多设置)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tblevelnumber
            // 
            this.tblevelnumber.Location = new System.Drawing.Point(156, 91);
            this.tblevelnumber.Name = "tblevelnumber";
            this.tblevelnumber.Size = new System.Drawing.Size(170, 21);
            this.tblevelnumber.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "分级数量/分级间隔";
            // 
            // cbattribute
            // 
            this.cbattribute.FormattingEnabled = true;
            this.cbattribute.Location = new System.Drawing.Point(156, 55);
            this.cbattribute.Name = "cbattribute";
            this.cbattribute.Size = new System.Drawing.Size(170, 20);
            this.cbattribute.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "专题属性";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "等比符号地图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(760, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "点密度图";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btchangethematic
            // 
            this.btchangethematic.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btchangethematic.Location = new System.Drawing.Point(202, 131);
            this.btchangethematic.Name = "btchangethematic";
            this.btchangethematic.Size = new System.Drawing.Size(124, 31);
            this.btchangethematic.TabIndex = 25;
            this.btchangethematic.Text = "修改专题地图类型";
            this.btchangethematic.UseVisualStyleBackColor = true;
            this.btchangethematic.Click += new System.EventHandler(this.Btchangethematic_Click);
            // 
            // AdvancedThematic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 449);
            this.Controls.Add(this.tabControl1);
            this.Name = "AdvancedThematic";
            this.Text = "AdvancedThematic";
            this.Shown += new System.EventHandler(this.AdvancedThematic_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbleveltype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbattribute;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tblevelnumber;
        private System.Windows.Forms.Button btchangethematic;
    }
}