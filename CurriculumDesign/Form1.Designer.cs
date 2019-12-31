namespace Lesson_22
{
    partial class Form1
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
            this.gisPanel1 = new MYGIS.GISPanel();
            this.statusStripLegend = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // gisPanel1
            // 
            this.gisPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gisPanel1.Location = new System.Drawing.Point(12, 12);
            this.gisPanel1.Name = "gisPanel1";
            this.gisPanel1.Size = new System.Drawing.Size(1330, 623);
            this.gisPanel1.TabIndex = 0;
            this.gisPanel1.Load += new System.EventHandler(this.GisPanel1_Load);
            // 
            // statusStripLegend
            // 
            this.statusStripLegend.Location = new System.Drawing.Point(0, 637);
            this.statusStripLegend.Name = "statusStripLegend";
            this.statusStripLegend.Size = new System.Drawing.Size(1354, 22);
            this.statusStripLegend.TabIndex = 1;
            this.statusStripLegend.Text = "statusStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 659);
            this.Controls.Add(this.statusStripLegend);
            this.Controls.Add(this.gisPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MYGIS.GISPanel gisPanel1;
        private System.Windows.Forms.StatusStrip statusStripLegend;
    }
}