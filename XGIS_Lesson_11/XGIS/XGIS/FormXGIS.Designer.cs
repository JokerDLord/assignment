namespace XGIS
{
    partial class FormXGIS
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bOpenShapeFile = new System.Windows.Forms.Button();
            this.bOpenAttribute = new System.Windows.Forms.Button();
            this.bWriteMyFile = new System.Windows.Forms.Button();
            this.bReadMyFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bOpenShapeFile
            // 
            this.bOpenShapeFile.Location = new System.Drawing.Point(33, 12);
            this.bOpenShapeFile.Name = "bOpenShapeFile";
            this.bOpenShapeFile.Size = new System.Drawing.Size(200, 23);
            this.bOpenShapeFile.TabIndex = 87;
            this.bOpenShapeFile.Text = "打开Shapefile";
            this.bOpenShapeFile.UseVisualStyleBackColor = true;
            this.bOpenShapeFile.Click += new System.EventHandler(this.BOpenShapeFile_Click);
            // 
            // bOpenAttribute
            // 
            this.bOpenAttribute.Location = new System.Drawing.Point(546, 12);
            this.bOpenAttribute.Name = "bOpenAttribute";
            this.bOpenAttribute.Size = new System.Drawing.Size(75, 23);
            this.bOpenAttribute.TabIndex = 88;
            this.bOpenAttribute.Text = "属性信息";
            this.bOpenAttribute.UseVisualStyleBackColor = true;
            this.bOpenAttribute.Click += new System.EventHandler(this.BOpenAttribute_Click);
            // 
            // bWriteMyFile
            // 
            this.bWriteMyFile.Location = new System.Drawing.Point(256, 12);
            this.bWriteMyFile.Name = "bWriteMyFile";
            this.bWriteMyFile.Size = new System.Drawing.Size(130, 23);
            this.bWriteMyFile.TabIndex = 2;
            this.bWriteMyFile.Text = "写入MyFile";
            this.bWriteMyFile.UseVisualStyleBackColor = true;
            this.bWriteMyFile.Click += new System.EventHandler(this.BWriteMyFile_Click);
            // 
            // bReadMyFile
            // 
            this.bReadMyFile.Location = new System.Drawing.Point(392, 12);
            this.bReadMyFile.Name = "bReadMyFile";
            this.bReadMyFile.Size = new System.Drawing.Size(130, 23);
            this.bReadMyFile.TabIndex = 2;
            this.bReadMyFile.Text = "打开MyFile";
            this.bReadMyFile.UseVisualStyleBackColor = true;
            this.bReadMyFile.Click += new System.EventHandler(this.BReadMyFile_Click);
            // 
            // FormXGIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 450);
            this.Controls.Add(this.bOpenAttribute);
            this.Controls.Add(this.bOpenShapeFile);
            this.Controls.Add(this.bReadMyFile);
            this.Controls.Add(this.bWriteMyFile);
            this.Name = "FormXGIS";
            this.Text = "FormXGIS2019";
            this.SizeChanged += new System.EventHandler(this.FormXGIS_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormXGIS_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FormXGIS_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormXGIS_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormXGIS_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormXGIS_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bOpenShapeFile;
        private System.Windows.Forms.Button bOpenAttribute;
        private System.Windows.Forms.Button bWriteMyFile;
        private System.Windows.Forms.Button bReadMyFile;
    }
}

