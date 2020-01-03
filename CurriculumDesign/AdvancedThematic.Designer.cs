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
            this.btchangethematic = new System.Windows.Forms.Button();
            this.tblevelnumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbattribute = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbclass = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cbattributeEsymbol = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btchangeEsymbol = new System.Windows.Forms.Button();
            this.btsymbolfillcolor = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.tbsymbolsize = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbattributedot = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btchangedotdensity = new System.Windows.Forms.Button();
            this.btdotfillcolor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbdotsize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbdotdensity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.preview = new System.Windows.Forms.Button();
            this.previewes = new System.Windows.Forms.Button();
            this.previewds = new System.Windows.Forms.Button();
            this.gisPanel1advanced = new MYGIS.GISPanel();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btmaxvcolor = new System.Windows.Forms.Button();
            this.btminvcolor = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbleveltype
            // 
            this.cbleveltype.FormattingEnabled = true;
            this.cbleveltype.Items.AddRange(new object[] {
            "分位数分级法(原默认方法)",
            "等范围分级法(Equal Interval)",
            "自定义间隔法(Define Interval)"});
            this.cbleveltype.Location = new System.Drawing.Point(168, 22);
            this.cbleveltype.Name = "cbleveltype";
            this.cbleveltype.Size = new System.Drawing.Size(148, 20);
            this.cbleveltype.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
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
            this.tabControl1.Size = new System.Drawing.Size(428, 355);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btminvcolor);
            this.tabPage1.Controls.Add(this.btmaxvcolor);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.preview);
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
            this.tabPage1.Size = new System.Drawing.Size(420, 329);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "分级设色地图(更多设置)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btchangethematic
            // 
            this.btchangethematic.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btchangethematic.Location = new System.Drawing.Point(168, 203);
            this.btchangethematic.Name = "btchangethematic";
            this.btchangethematic.Size = new System.Drawing.Size(170, 31);
            this.btchangethematic.TabIndex = 25;
            this.btchangethematic.Text = "修改分级设色地图";
            this.btchangethematic.UseVisualStyleBackColor = true;
            this.btchangethematic.Click += new System.EventHandler(this.Btchangethematic_Click);
            // 
            // tblevelnumber
            // 
            this.tblevelnumber.Location = new System.Drawing.Point(168, 91);
            this.tblevelnumber.Name = "tblevelnumber";
            this.tblevelnumber.Size = new System.Drawing.Size(170, 21);
            this.tblevelnumber.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "分级数量/分级间隔";
            // 
            // cbattribute
            // 
            this.cbattribute.FormattingEnabled = true;
            this.cbattribute.Location = new System.Drawing.Point(168, 55);
            this.cbattribute.Name = "cbattribute";
            this.cbattribute.Size = new System.Drawing.Size(170, 20);
            this.cbattribute.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "专题属性";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.previewes);
            this.tabPage2.Controls.Add(this.tbclass);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.cbattributeEsymbol);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.btchangeEsymbol);
            this.tabPage2.Controls.Add(this.btsymbolfillcolor);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.tbsymbolsize);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(420, 329);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "等比符号地图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbclass
            // 
            this.tbclass.Location = new System.Drawing.Point(130, 125);
            this.tbclass.Name = "tbclass";
            this.tbclass.Size = new System.Drawing.Size(124, 21);
            this.tbclass.TabIndex = 55;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(30, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 54;
            this.label17.Text = "符号分级数";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(54, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(0, 12);
            this.label18.TabIndex = 53;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(302, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 52;
            this.label12.Text = "注";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(304, 80);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 103);
            this.textBox2.TabIndex = 51;
            this.textBox2.Text = "此处size是最小的符号的大小,分级数是指符号将被分成多少个级别";
            // 
            // cbattributeEsymbol
            // 
            this.cbattributeEsymbol.FormattingEnabled = true;
            this.cbattributeEsymbol.Location = new System.Drawing.Point(130, 30);
            this.cbattributeEsymbol.Name = "cbattributeEsymbol";
            this.cbattributeEsymbol.Size = new System.Drawing.Size(170, 20);
            this.cbattributeEsymbol.TabIndex = 50;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(42, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 49;
            this.label11.Text = "专题属性";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(281, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 12);
            this.label13.TabIndex = 46;
            // 
            // btchangeEsymbol
            // 
            this.btchangeEsymbol.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btchangeEsymbol.Location = new System.Drawing.Point(130, 232);
            this.btchangeEsymbol.Name = "btchangeEsymbol";
            this.btchangeEsymbol.Size = new System.Drawing.Size(124, 31);
            this.btchangeEsymbol.TabIndex = 45;
            this.btchangeEsymbol.Text = "修改等比符号图";
            this.btchangeEsymbol.UseVisualStyleBackColor = true;
            this.btchangeEsymbol.Click += new System.EventHandler(this.BtchangeEsymbol_Click);
            // 
            // btsymbolfillcolor
            // 
            this.btsymbolfillcolor.Location = new System.Drawing.Point(130, 175);
            this.btsymbolfillcolor.Name = "btsymbolfillcolor";
            this.btsymbolfillcolor.Size = new System.Drawing.Size(124, 35);
            this.btsymbolfillcolor.TabIndex = 44;
            this.btsymbolfillcolor.UseVisualStyleBackColor = true;
            this.btsymbolfillcolor.Click += new System.EventHandler(this.SettingColor_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 43;
            this.label14.Text = "选择点颜色";
            // 
            // tbsymbolsize
            // 
            this.tbsymbolsize.Location = new System.Drawing.Point(130, 77);
            this.tbsymbolsize.Name = "tbsymbolsize";
            this.tbsymbolsize.Size = new System.Drawing.Size(124, 21);
            this.tbsymbolsize.TabIndex = 42;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(66, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 41;
            this.label15.Text = "size";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(54, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 12);
            this.label16.TabIndex = 40;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.previewds);
            this.tabPage3.Controls.Add(this.cbattributedot);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.btchangedotdensity);
            this.tabPage3.Controls.Add(this.btdotfillcolor);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.tbdotsize);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.tbdotdensity);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(420, 329);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "点密度图";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbattributedot
            // 
            this.cbattributedot.FormattingEnabled = true;
            this.cbattributedot.Location = new System.Drawing.Point(138, 46);
            this.cbattributedot.Name = "cbattributedot";
            this.cbattributedot.Size = new System.Drawing.Size(170, 20);
            this.cbattributedot.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "专题属性";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(308, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "注";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(310, 104);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 103);
            this.textBox1.TabIndex = 34;
            this.textBox1.Text = "点密度值请输入一个整数，通过内设的权重得到显示的点。";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(289, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 12);
            this.label8.TabIndex = 33;
            this.label8.Click += new System.EventHandler(this.Label8_Click);
            // 
            // btchangedotdensity
            // 
            this.btchangedotdensity.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btchangedotdensity.Location = new System.Drawing.Point(138, 233);
            this.btchangedotdensity.Name = "btchangedotdensity";
            this.btchangedotdensity.Size = new System.Drawing.Size(124, 31);
            this.btchangedotdensity.TabIndex = 32;
            this.btchangedotdensity.Text = "修改点密度图";
            this.btchangedotdensity.UseVisualStyleBackColor = true;
            this.btchangedotdensity.Click += new System.EventHandler(this.btchangedotdensity_Click);
            // 
            // btdotfillcolor
            // 
            this.btdotfillcolor.Location = new System.Drawing.Point(138, 177);
            this.btdotfillcolor.Name = "btdotfillcolor";
            this.btdotfillcolor.Size = new System.Drawing.Size(124, 33);
            this.btdotfillcolor.TabIndex = 31;
            this.btdotfillcolor.UseVisualStyleBackColor = true;
            this.btdotfillcolor.Click += new System.EventHandler(this.SettingColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "选择点颜色";
            // 
            // tbdotsize
            // 
            this.tbdotsize.Location = new System.Drawing.Point(138, 140);
            this.tbdotsize.Name = "tbdotsize";
            this.tbdotsize.Size = new System.Drawing.Size(124, 21);
            this.tbdotsize.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 27;
            // 
            // tbdotdensity
            // 
            this.tbdotdensity.Location = new System.Drawing.Point(138, 96);
            this.tbdotdensity.Name = "tbdotdensity";
            this.tbdotdensity.Size = new System.Drawing.Size(124, 21);
            this.tbdotdensity.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "点密度";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(464, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 37;
            this.label19.Text = "预览窗口";
            // 
            // preview
            // 
            this.preview.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.preview.Location = new System.Drawing.Point(35, 203);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(105, 31);
            this.preview.TabIndex = 26;
            this.preview.Text = "预览";
            this.preview.UseVisualStyleBackColor = true;
            this.preview.Click += new System.EventHandler(this.Btchangethematic_Click);
            // 
            // previewes
            // 
            this.previewes.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.previewes.Location = new System.Drawing.Point(6, 232);
            this.previewes.Name = "previewes";
            this.previewes.Size = new System.Drawing.Size(105, 31);
            this.previewes.TabIndex = 56;
            this.previewes.Text = "预览";
            this.previewes.UseVisualStyleBackColor = true;
            this.previewes.Click += new System.EventHandler(this.BtchangeEsymbol_Click);
            // 
            // previewds
            // 
            this.previewds.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.previewds.Location = new System.Drawing.Point(16, 233);
            this.previewds.Name = "previewds";
            this.previewds.Size = new System.Drawing.Size(105, 31);
            this.previewds.TabIndex = 57;
            this.previewds.Text = "预览";
            this.previewds.UseVisualStyleBackColor = true;
            this.previewds.Click += new System.EventHandler(this.btchangedotdensity_Click);
            // 
            // gisPanel1advanced
            // 
            this.gisPanel1advanced.BackColor = System.Drawing.Color.LightGray;
            this.gisPanel1advanced.Location = new System.Drawing.Point(466, 34);
            this.gisPanel1advanced.Name = "gisPanel1advanced";
            this.gisPanel1advanced.Size = new System.Drawing.Size(324, 329);
            this.gisPanel1advanced.TabIndex = 23;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(63, 137);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 12);
            this.label20.TabIndex = 27;
            this.label20.Text = "设定大值颜色";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(63, 170);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 12);
            this.label21.TabIndex = 28;
            this.label21.Text = "设定低值颜色";
            this.label21.Click += new System.EventHandler(this.Label21_Click);
            // 
            // btmaxvcolor
            // 
            this.btmaxvcolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btmaxvcolor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btmaxvcolor.Location = new System.Drawing.Point(168, 131);
            this.btmaxvcolor.Name = "btmaxvcolor";
            this.btmaxvcolor.Size = new System.Drawing.Size(104, 24);
            this.btmaxvcolor.TabIndex = 45;
            this.btmaxvcolor.UseVisualStyleBackColor = false;
            this.btmaxvcolor.Click += new System.EventHandler(this.SettingColor_Click);
            // 
            // btminvcolor
            // 
            this.btminvcolor.BackColor = System.Drawing.Color.White;
            this.btminvcolor.Location = new System.Drawing.Point(168, 164);
            this.btminvcolor.Name = "btminvcolor";
            this.btminvcolor.Size = new System.Drawing.Size(104, 24);
            this.btminvcolor.TabIndex = 46;
            this.btminvcolor.UseVisualStyleBackColor = false;
            this.btminvcolor.Click += new System.EventHandler(this.SettingColor_Click);
            // 
            // AdvancedThematic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 380);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.gisPanel1advanced);
            this.Controls.Add(this.tabControl1);
            this.Name = "AdvancedThematic";
            this.Text = "AdvancedThematic";
            this.Load += new System.EventHandler(this.AdvancedThematic_Load);
            this.Shown += new System.EventHandler(this.AdvancedThematic_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbdotsize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbdotdensity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btdotfillcolor;
        private System.Windows.Forms.Button btchangedotdensity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbattributedot;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbattributeEsymbol;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btchangeEsymbol;
        private System.Windows.Forms.Button btsymbolfillcolor;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbsymbolsize;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tbclass;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private GISPanel gisPanel1advanced;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button preview;
        private System.Windows.Forms.Button previewes;
        private System.Windows.Forms.Button previewds;
        private System.Windows.Forms.Button btminvcolor;
        private System.Windows.Forms.Button btmaxvcolor;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
    }
}