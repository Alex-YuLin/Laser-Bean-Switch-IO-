namespace KeyInfoSetting
{
    partial class MainFrm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.LaserConfig = new System.Windows.Forms.ComboBox();
            this.CCDConfig = new System.Windows.Forms.ComboBox();
            this.DigitalIO = new System.Windows.Forms.ComboBox();
            this.AxesConfig = new System.Windows.Forms.ComboBox();
            this.AnalogOutput = new System.Windows.Forms.ComboBox();
            this.GalvoConfig = new System.Windows.Forms.ComboBox();
            this.AnalogInput = new System.Windows.Forms.ComboBox();
            this.XFlip = new System.Windows.Forms.CheckBox();
            this.YFlip = new System.Windows.Forms.CheckBox();
            this.XYChange = new System.Windows.Forms.CheckBox();
            this.GalvoXFlip = new System.Windows.Forms.CheckBox();
            this.GalvoYFlip = new System.Windows.Forms.CheckBox();
            this.GalvoXYChange = new System.Windows.Forms.CheckBox();
            this.WobbleFlag = new System.Windows.Forms.CheckBox();
            this.VisualMarkCounts = new System.Windows.Forms.ComboBox();
            this.ExtensionOption = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LaserConfig
            // 
            this.LaserConfig.Enabled = false;
            this.LaserConfig.FormattingEnabled = true;
            this.LaserConfig.Items.AddRange(new object[] {
            "Dummy ",
            "Coherent ",
            "Optowave ",
            "Multiwave ",
            "SPI ",
            "SPUV ",
            "YVO4 ",
            "Pico UV ",
            "Offline"});
            this.LaserConfig.Location = new System.Drawing.Point(65, 76);
            this.LaserConfig.Name = "LaserConfig";
            this.LaserConfig.Size = new System.Drawing.Size(128, 28);
            this.LaserConfig.TabIndex = 1;
            this.LaserConfig.Text = "Laser Config";
            // 
            // CCDConfig
            // 
            this.CCDConfig.Enabled = false;
            this.CCDConfig.FormattingEnabled = true;
            this.CCDConfig.Items.AddRange(new object[] {
            "Clipboard ",
            "Picolo ",
            "AISYS",
            "Offline"});
            this.CCDConfig.Location = new System.Drawing.Point(65, 110);
            this.CCDConfig.Name = "CCDConfig";
            this.CCDConfig.Size = new System.Drawing.Size(128, 28);
            this.CCDConfig.TabIndex = 2;
            this.CCDConfig.Text = "CCD Config";
            this.CCDConfig.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // DigitalIO
            // 
            this.DigitalIO.Enabled = false;
            this.DigitalIO.FormattingEnabled = true;
            this.DigitalIO.Items.AddRange(new object[] {
            "Dummy ",
            "U500 ",
            "Advantech ",
            "PCIL112 ",
            "PCIL122 ",
            "Offline"});
            this.DigitalIO.Location = new System.Drawing.Point(65, 144);
            this.DigitalIO.Name = "DigitalIO";
            this.DigitalIO.Size = new System.Drawing.Size(128, 28);
            this.DigitalIO.TabIndex = 3;
            this.DigitalIO.Text = "Digital IO\t";
            // 
            // AxesConfig
            // 
            this.AxesConfig.Enabled = false;
            this.AxesConfig.FormattingEnabled = true;
            this.AxesConfig.Items.AddRange(new object[] {
            "Dummy ",
            "U500 ",
            "A3200 ",
            "Servotronix ",
            "UTC400 ",
            "PMC6 ",
            "ACS",
            "Offline"});
            this.AxesConfig.Location = new System.Drawing.Point(65, 178);
            this.AxesConfig.Name = "AxesConfig";
            this.AxesConfig.Size = new System.Drawing.Size(128, 28);
            this.AxesConfig.TabIndex = 4;
            this.AxesConfig.Text = "Axes Config";
            // 
            // AnalogOutput
            // 
            this.AnalogOutput.Enabled = false;
            this.AnalogOutput.FormattingEnabled = true;
            this.AnalogOutput.Items.AddRange(new object[] {
            "Dummy ",
            "U500 ",
            "A3200 ",
            "PCIL112 ",
            "PCIL122 ",
            "Offline"});
            this.AnalogOutput.Location = new System.Drawing.Point(65, 212);
            this.AnalogOutput.Name = "AnalogOutput";
            this.AnalogOutput.Size = new System.Drawing.Size(139, 28);
            this.AnalogOutput.TabIndex = 4;
            this.AnalogOutput.Text = "Analog Output ";
            // 
            // GalvoConfig
            // 
            this.GalvoConfig.Enabled = false;
            this.GalvoConfig.FormattingEnabled = true;
            this.GalvoConfig.Items.AddRange(new object[] {
            "Dummy ",
            "LightningII ",
            "RTC3 ",
            "RTC5 ",
            "Offline"});
            this.GalvoConfig.Location = new System.Drawing.Point(65, 246);
            this.GalvoConfig.Name = "GalvoConfig";
            this.GalvoConfig.Size = new System.Drawing.Size(139, 28);
            this.GalvoConfig.TabIndex = 4;
            this.GalvoConfig.Text = "Galvo Config";
            // 
            // AnalogInput
            // 
            this.AnalogInput.Enabled = false;
            this.AnalogInput.FormattingEnabled = true;
            this.AnalogInput.Items.AddRange(new object[] {
            "Dummy ",
            "A3200 ",
            "PCIL112 ",
            "PCIL122 ",
            "Offline"});
            this.AnalogInput.Location = new System.Drawing.Point(63, 280);
            this.AnalogInput.Name = "AnalogInput";
            this.AnalogInput.Size = new System.Drawing.Size(139, 28);
            this.AnalogInput.TabIndex = 4;
            this.AnalogInput.Text = "Analog Input";
            // 
            // XFlip
            // 
            this.XFlip.AutoSize = true;
            this.XFlip.Location = new System.Drawing.Point(221, 71);
            this.XFlip.Name = "XFlip";
            this.XFlip.Size = new System.Drawing.Size(68, 24);
            this.XFlip.TabIndex = 5;
            this.XFlip.Text = "X Flip";
            this.XFlip.UseVisualStyleBackColor = true;
            // 
            // YFlip
            // 
            this.YFlip.AutoSize = true;
            this.YFlip.Location = new System.Drawing.Point(292, 71);
            this.YFlip.Name = "YFlip";
            this.YFlip.Size = new System.Drawing.Size(68, 24);
            this.YFlip.TabIndex = 6;
            this.YFlip.Text = "Y Flip";
            this.YFlip.UseVisualStyleBackColor = true;
            // 
            // XYChange
            // 
            this.XYChange.AutoSize = true;
            this.XYChange.Location = new System.Drawing.Point(221, 101);
            this.XYChange.Name = "XYChange";
            this.XYChange.Size = new System.Drawing.Size(111, 24);
            this.XYChange.TabIndex = 6;
            this.XYChange.Text = "XY Change";
            this.XYChange.UseVisualStyleBackColor = true;
            // 
            // GalvoXFlip
            // 
            this.GalvoXFlip.AutoSize = true;
            this.GalvoXFlip.Location = new System.Drawing.Point(221, 131);
            this.GalvoXFlip.Name = "GalvoXFlip";
            this.GalvoXFlip.Size = new System.Drawing.Size(115, 24);
            this.GalvoXFlip.TabIndex = 6;
            this.GalvoXFlip.Text = "Galvo X Flip";
            this.GalvoXFlip.UseVisualStyleBackColor = true;
            // 
            // GalvoYFlip
            // 
            this.GalvoYFlip.AutoSize = true;
            this.GalvoYFlip.Location = new System.Drawing.Point(221, 161);
            this.GalvoYFlip.Name = "GalvoYFlip";
            this.GalvoYFlip.Size = new System.Drawing.Size(115, 24);
            this.GalvoYFlip.TabIndex = 6;
            this.GalvoYFlip.Text = "Galvo Y Flip";
            this.GalvoYFlip.UseVisualStyleBackColor = true;
            // 
            // GalvoXYChange
            // 
            this.GalvoXYChange.AutoSize = true;
            this.GalvoXYChange.Location = new System.Drawing.Point(221, 191);
            this.GalvoXYChange.Name = "GalvoXYChange";
            this.GalvoXYChange.Size = new System.Drawing.Size(158, 24);
            this.GalvoXYChange.TabIndex = 6;
            this.GalvoXYChange.Text = "Galvo XY Change";
            this.GalvoXYChange.UseVisualStyleBackColor = true;
            // 
            // WobbleFlag
            // 
            this.WobbleFlag.AutoSize = true;
            this.WobbleFlag.Location = new System.Drawing.Point(221, 221);
            this.WobbleFlag.Name = "WobbleFlag";
            this.WobbleFlag.Size = new System.Drawing.Size(122, 24);
            this.WobbleFlag.TabIndex = 6;
            this.WobbleFlag.Text = "Wobble Flag";
            this.WobbleFlag.UseVisualStyleBackColor = true;
            // 
            // VisualMarkCounts
            // 
            this.VisualMarkCounts.Enabled = false;
            this.VisualMarkCounts.FormattingEnabled = true;
            this.VisualMarkCounts.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.VisualMarkCounts.Location = new System.Drawing.Point(221, 251);
            this.VisualMarkCounts.Name = "VisualMarkCounts";
            this.VisualMarkCounts.Size = new System.Drawing.Size(139, 28);
            this.VisualMarkCounts.TabIndex = 4;
            this.VisualMarkCounts.Text = "Visual Mark Cts";
            // 
            // ExtensionOption
            // 
            this.ExtensionOption.AutoSize = true;
            this.ExtensionOption.Location = new System.Drawing.Point(221, 285);
            this.ExtensionOption.Name = "ExtensionOption";
            this.ExtensionOption.Size = new System.Drawing.Size(158, 24);
            this.ExtensionOption.TabIndex = 6;
            this.ExtensionOption.Text = "Extension Option";
            this.ExtensionOption.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ExtensionOption);
            this.groupBox1.Controls.Add(this.LaserConfig);
            this.groupBox1.Controls.Add(this.WobbleFlag);
            this.groupBox1.Controls.Add(this.CCDConfig);
            this.groupBox1.Controls.Add(this.GalvoXYChange);
            this.groupBox1.Controls.Add(this.DigitalIO);
            this.groupBox1.Controls.Add(this.GalvoYFlip);
            this.groupBox1.Controls.Add(this.AxesConfig);
            this.groupBox1.Controls.Add(this.GalvoXFlip);
            this.groupBox1.Controls.Add(this.AnalogOutput);
            this.groupBox1.Controls.Add(this.XYChange);
            this.groupBox1.Controls.Add(this.GalvoConfig);
            this.groupBox1.Controls.Add(this.YFlip);
            this.groupBox1.Controls.Add(this.AnalogInput);
            this.groupBox1.Controls.Add(this.XFlip);
            this.groupBox1.Controls.Add(this.VisualMarkCounts);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 319);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "KeyInfo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "AI";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 249);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Galvo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "AO";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Axes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "IO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "CCD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Laser";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(422, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 33);
            this.button1.TabIndex = 8;
            this.button1.Text = "Read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Time: ";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 341);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainFrm";
            this.Text = "Program";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox LaserConfig;
        private System.Windows.Forms.ComboBox CCDConfig;
        private System.Windows.Forms.ComboBox DigitalIO;
        private System.Windows.Forms.ComboBox AxesConfig;
        private System.Windows.Forms.ComboBox AnalogOutput;
        private System.Windows.Forms.ComboBox GalvoConfig;
        private System.Windows.Forms.ComboBox AnalogInput;
        private System.Windows.Forms.CheckBox XFlip;
        private System.Windows.Forms.CheckBox YFlip;
        private System.Windows.Forms.CheckBox XYChange;
        private System.Windows.Forms.CheckBox GalvoXFlip;
        private System.Windows.Forms.CheckBox GalvoYFlip;
        private System.Windows.Forms.CheckBox GalvoXYChange;
        private System.Windows.Forms.CheckBox WobbleFlag;
        private System.Windows.Forms.ComboBox VisualMarkCounts;
        private System.Windows.Forms.CheckBox ExtensionOption;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
    }
}

