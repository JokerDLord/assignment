﻿namespace XGIS
{
    partial class FormAttribute
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
            this.dgvAttribute = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttribute)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAttribute
            // 
            this.dgvAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttribute.Location = new System.Drawing.Point(0, 0);
            this.dgvAttribute.Name = "dgvAttribute";
            this.dgvAttribute.RowTemplate.Height = 23;
            this.dgvAttribute.Size = new System.Drawing.Size(785, 450);
            this.dgvAttribute.TabIndex = 0;
            // 
            // FormAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 450);
            this.Controls.Add(this.dgvAttribute);
            this.Name = "FormAttribute";
            this.Text = "FormAttribute";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttribute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAttribute;
    }
}