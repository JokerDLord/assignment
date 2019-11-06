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
            this.deleteField = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_del = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addField = new System.Windows.Forms.Button();
            this.textBox_add = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_oldField = new System.Windows.Forms.TextBox();
            this.textBox_newField = new System.Windows.Forms.TextBox();
            this.changeFieldName = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_movefield = new System.Windows.Forms.TextBox();
            this.moveforward = new System.Windows.Forms.Button();
            this.moveback = new System.Windows.Forms.Button();
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
            // deleteField
            // 
            this.deleteField.Location = new System.Drawing.Point(1086, 12);
            this.deleteField.Name = "deleteField";
            this.deleteField.Size = new System.Drawing.Size(75, 23);
            this.deleteField.TabIndex = 89;
            this.deleteField.Text = "删除字段";
            this.deleteField.UseVisualStyleBackColor = true;
            this.deleteField.Click += new System.EventHandler(this.DeleteField_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(915, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 90;
            this.label1.Text = "字段名";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // textBox_del
            // 
            this.textBox_del.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_del.Location = new System.Drawing.Point(980, 14);
            this.textBox_del.Name = "textBox_del";
            this.textBox_del.Size = new System.Drawing.Size(100, 21);
            this.textBox_del.TabIndex = 91;
            this.textBox_del.Text = "newarea";
            this.textBox_del.TextChanged += new System.EventHandler(this.TextBox_delete_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(915, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 92;
            this.label2.Text = "字段名";
            // 
            // addField
            // 
            this.addField.Location = new System.Drawing.Point(1086, 54);
            this.addField.Name = "addField";
            this.addField.Size = new System.Drawing.Size(75, 23);
            this.addField.TabIndex = 93;
            this.addField.Text = "增加字段";
            this.addField.UseVisualStyleBackColor = true;
            this.addField.Click += new System.EventHandler(this.AddField_Click);
            // 
            // textBox_add
            // 
            this.textBox_add.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_add.Location = new System.Drawing.Point(980, 51);
            this.textBox_add.Name = "textBox_add";
            this.textBox_add.Size = new System.Drawing.Size(100, 21);
            this.textBox_add.TabIndex = 94;
            this.textBox_add.Text = "newarea2";
            this.textBox_add.TextChanged += new System.EventHandler(this.TextBox_add_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(915, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 95;
            this.label3.Text = "旧字段名";
            // 
            // textBox_oldField
            // 
            this.textBox_oldField.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_oldField.Location = new System.Drawing.Point(980, 87);
            this.textBox_oldField.Name = "textBox_oldField";
            this.textBox_oldField.Size = new System.Drawing.Size(100, 21);
            this.textBox_oldField.TabIndex = 96;
            this.textBox_oldField.Text = "newarea";
            // 
            // textBox_newField
            // 
            this.textBox_newField.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_newField.Location = new System.Drawing.Point(980, 123);
            this.textBox_newField.Name = "textBox_newField";
            this.textBox_newField.Size = new System.Drawing.Size(100, 21);
            this.textBox_newField.TabIndex = 97;
            this.textBox_newField.Text = "newarea_new";
            // 
            // changeFieldName
            // 
            this.changeFieldName.Location = new System.Drawing.Point(1086, 101);
            this.changeFieldName.Name = "changeFieldName";
            this.changeFieldName.Size = new System.Drawing.Size(75, 23);
            this.changeFieldName.TabIndex = 98;
            this.changeFieldName.Text = "更改字段名";
            this.changeFieldName.UseVisualStyleBackColor = true;
            this.changeFieldName.Click += new System.EventHandler(this.ChangeFieldName_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(915, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 99;
            this.label4.Text = "新字段名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(915, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 100;
            this.label5.Text = "移动字段";
            // 
            // textBox_movefield
            // 
            this.textBox_movefield.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_movefield.Location = new System.Drawing.Point(980, 174);
            this.textBox_movefield.Name = "textBox_movefield";
            this.textBox_movefield.Size = new System.Drawing.Size(100, 21);
            this.textBox_movefield.TabIndex = 101;
            this.textBox_movefield.Text = "newarea";
            // 
            // moveforward
            // 
            this.moveforward.Location = new System.Drawing.Point(1086, 152);
            this.moveforward.Name = "moveforward";
            this.moveforward.Size = new System.Drawing.Size(75, 23);
            this.moveforward.TabIndex = 102;
            this.moveforward.Text = "前移";
            this.moveforward.UseVisualStyleBackColor = true;
            this.moveforward.Click += new System.EventHandler(this.Moveforward_Click);
            // 
            // moveback
            // 
            this.moveback.Location = new System.Drawing.Point(1086, 193);
            this.moveback.Name = "moveback";
            this.moveback.Size = new System.Drawing.Size(75, 23);
            this.moveback.TabIndex = 103;
            this.moveback.Text = "后移";
            this.moveback.UseVisualStyleBackColor = true;
            this.moveback.Click += new System.EventHandler(this.Moveforward_Click);
            // 
            // FormXGIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 450);
            this.Controls.Add(this.moveback);
            this.Controls.Add(this.moveforward);
            this.Controls.Add(this.textBox_movefield);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.changeFieldName);
            this.Controls.Add(this.textBox_newField);
            this.Controls.Add(this.textBox_oldField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_add);
            this.Controls.Add(this.addField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_del);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteField);
            this.Controls.Add(this.bOpenAttribute);
            this.Controls.Add(this.bOpenShapeFile);
            this.Controls.Add(this.bMoveRight);
            this.Controls.Add(this.bMoveLeft);
            this.Controls.Add(this.bMoveDown);
            this.Controls.Add(this.bMoveUp);
            this.Controls.Add(this.bZoomOut);
            this.Controls.Add(this.bZoomIn);
            this.Controls.Add(this.bFullExtent);
            this.Name = "FormXGIS";
            this.Text = "FormXGIS2019";
            this.Load += new System.EventHandler(this.FormXGIS_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormXGIS_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button deleteField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_del;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addField;
        private System.Windows.Forms.TextBox textBox_add;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_oldField;
        private System.Windows.Forms.TextBox textBox_newField;
        private System.Windows.Forms.Button changeFieldName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_movefield;
        private System.Windows.Forms.Button moveforward;
        private System.Windows.Forms.Button moveback;
    }
}

