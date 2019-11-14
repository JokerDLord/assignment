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
            this.bFullExtent = new System.Windows.Forms.Button();
            this.bZoomIn = new System.Windows.Forms.Button();
            this.bZoomOut = new System.Windows.Forms.Button();
            this.bMoveUp = new System.Windows.Forms.Button();
            this.bMoveDown = new System.Windows.Forms.Button();
            this.bMoveLeft = new System.Windows.Forms.Button();
            this.bMoveRight = new System.Windows.Forms.Button();
            this.bOpenShapeFile = new System.Windows.Forms.Button();
            this.bOpenAttribute = new System.Windows.Forms.Button();
            this.bWriteMyFile = new System.Windows.Forms.Button();
            this.bReadMyFile = new System.Windows.Forms.Button();
            this.BOpenJSON = new System.Windows.Forms.Button();
            this.bWriteJSON = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bFullExtent
            // 
            this.bFullExtent.Location = new System.Drawing.Point(239, 12);
            this.bFullExtent.Name = "bFullExtent";
            this.bFullExtent.Size = new System.Drawing.Size(75, 23);
            this.bFullExtent.TabIndex = 2;
            this.bFullExtent.Text = "显示全图";
            this.bFullExtent.UseVisualStyleBackColor = true;
            this.bFullExtent.Click += new System.EventHandler(this.BFullExtent_Click);
            // 
            // bZoomIn
            // 
            this.bZoomIn.Location = new System.Drawing.Point(319, 12);
            this.bZoomIn.Name = "bZoomIn";
            this.bZoomIn.Size = new System.Drawing.Size(75, 23);
            this.bZoomIn.TabIndex = 86;
            this.bZoomIn.Text = "放大";
            this.bZoomIn.UseVisualStyleBackColor = true;
            this.bZoomIn.Click += new System.EventHandler(this.MapExplore);
            // 
            // bZoomOut
            // 
            this.bZoomOut.Location = new System.Drawing.Point(400, 11);
            this.bZoomOut.Name = "bZoomOut";
            this.bZoomOut.Size = new System.Drawing.Size(75, 23);
            this.bZoomOut.TabIndex = 86;
            this.bZoomOut.Text = "缩小";
            this.bZoomOut.UseVisualStyleBackColor = true;
            this.bZoomOut.Click += new System.EventHandler(this.MapExplore);
            // 
            // bMoveUp
            // 
            this.bMoveUp.Location = new System.Drawing.Point(481, 11);
            this.bMoveUp.Name = "bMoveUp";
            this.bMoveUp.Size = new System.Drawing.Size(75, 23);
            this.bMoveUp.TabIndex = 86;
            this.bMoveUp.Text = "上移";
            this.bMoveUp.UseVisualStyleBackColor = true;
            this.bMoveUp.Click += new System.EventHandler(this.MapExplore);
            // 
            // bMoveDown
            // 
            this.bMoveDown.Location = new System.Drawing.Point(562, 12);
            this.bMoveDown.Name = "bMoveDown";
            this.bMoveDown.Size = new System.Drawing.Size(75, 23);
            this.bMoveDown.TabIndex = 86;
            this.bMoveDown.Text = "下移";
            this.bMoveDown.UseVisualStyleBackColor = true;
            this.bMoveDown.Click += new System.EventHandler(this.MapExplore);
            // 
            // bMoveLeft
            // 
            this.bMoveLeft.Location = new System.Drawing.Point(643, 12);
            this.bMoveLeft.Name = "bMoveLeft";
            this.bMoveLeft.Size = new System.Drawing.Size(75, 23);
            this.bMoveLeft.TabIndex = 86;
            this.bMoveLeft.Text = "左移";
            this.bMoveLeft.UseVisualStyleBackColor = true;
            this.bMoveLeft.Click += new System.EventHandler(this.MapExplore);
            // 
            // bMoveRight
            // 
            this.bMoveRight.Location = new System.Drawing.Point(724, 12);
            this.bMoveRight.Name = "bMoveRight";
            this.bMoveRight.Size = new System.Drawing.Size(75, 23);
            this.bMoveRight.TabIndex = 86;
            this.bMoveRight.Text = "右移";
            this.bMoveRight.UseVisualStyleBackColor = true;
            this.bMoveRight.Click += new System.EventHandler(this.MapExplore);
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
            this.bOpenAttribute.Location = new System.Drawing.Point(805, 12);
            this.bOpenAttribute.Name = "bOpenAttribute";
            this.bOpenAttribute.Size = new System.Drawing.Size(75, 23);
            this.bOpenAttribute.TabIndex = 88;
            this.bOpenAttribute.Text = "属性信息";
            this.bOpenAttribute.UseVisualStyleBackColor = true;
            this.bOpenAttribute.Click += new System.EventHandler(this.BOpenAttribute_Click);
            // 
            // bWriteMyFile
            // 
            this.bWriteMyFile.Location = new System.Drawing.Point(12, 68);
            this.bWriteMyFile.Name = "bWriteMyFile";
            this.bWriteMyFile.Size = new System.Drawing.Size(130, 23);
            this.bWriteMyFile.TabIndex = 2;
            this.bWriteMyFile.Text = "写入MyFile";
            this.bWriteMyFile.UseVisualStyleBackColor = true;
            this.bWriteMyFile.Click += new System.EventHandler(this.BWriteMyFile_Click);
            // 
            // bReadMyFile
            // 
            this.bReadMyFile.Location = new System.Drawing.Point(169, 68);
            this.bReadMyFile.Name = "bReadMyFile";
            this.bReadMyFile.Size = new System.Drawing.Size(130, 23);
            this.bReadMyFile.TabIndex = 2;
            this.bReadMyFile.Text = "打开MyFile";
            this.bReadMyFile.UseVisualStyleBackColor = true;
            this.bReadMyFile.Click += new System.EventHandler(this.BReadMyFile_Click);
            // 
            // BOpenJSON
            // 
            this.BOpenJSON.Location = new System.Drawing.Point(169, 106);
            this.BOpenJSON.Name = "BOpenJSON";
            this.BOpenJSON.Size = new System.Drawing.Size(130, 23);
            this.BOpenJSON.TabIndex = 89;
            this.BOpenJSON.Text = "打开JSON";
            this.BOpenJSON.UseVisualStyleBackColor = true;
            this.BOpenJSON.Click += new System.EventHandler(this.BOpenJSON_Click);
            // 
            // bWriteJSON
            // 
            this.bWriteJSON.Location = new System.Drawing.Point(12, 106);
            this.bWriteJSON.Name = "bWriteJSON";
            this.bWriteJSON.Size = new System.Drawing.Size(130, 23);
            this.bWriteJSON.TabIndex = 90;
            this.bWriteJSON.Text = "写入JSON";
            this.bWriteJSON.UseVisualStyleBackColor = true;
            this.bWriteJSON.Click += new System.EventHandler(this.BWriteJSON_Click);
            // 
            // FormXGIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 679);
            this.Controls.Add(this.bWriteJSON);
            this.Controls.Add(this.BOpenJSON);
            this.Controls.Add(this.bOpenAttribute);
            this.Controls.Add(this.bOpenShapeFile);
            this.Controls.Add(this.bMoveRight);
            this.Controls.Add(this.bMoveLeft);
            this.Controls.Add(this.bMoveDown);
            this.Controls.Add(this.bMoveUp);
            this.Controls.Add(this.bZoomOut);
            this.Controls.Add(this.bZoomIn);
            this.Controls.Add(this.bReadMyFile);
            this.Controls.Add(this.bWriteMyFile);
            this.Controls.Add(this.bFullExtent);
            this.Name = "FormXGIS";
            this.Text = "FormXGIS2019";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormXGIS_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bFullExtent;
        private System.Windows.Forms.Button bZoomIn;
        private System.Windows.Forms.Button bZoomOut;
        private System.Windows.Forms.Button bMoveUp;
        private System.Windows.Forms.Button bMoveDown;
        private System.Windows.Forms.Button bMoveLeft;
        private System.Windows.Forms.Button bMoveRight;
        private System.Windows.Forms.Button bOpenShapeFile;
        private System.Windows.Forms.Button bOpenAttribute;
        private System.Windows.Forms.Button bWriteMyFile;
        private System.Windows.Forms.Button bReadMyFile;
        private System.Windows.Forms.Button BOpenJSON;
        private System.Windows.Forms.Button bWriteJSON;
    }
}

