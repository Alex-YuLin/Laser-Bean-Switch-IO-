namespace MainFrm
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
            this.Status = new System.Windows.Forms.StatusStrip();
            this.ToolStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RadB_SPI = new System.Windows.Forms.RadioButton();
            this.RadB_Matrix = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Label_BeanValue = new System.Windows.Forms.Label();
            this.Label_Middle = new System.Windows.Forms.Label();
            this.Label_Max = new System.Windows.Forms.Label();
            this.Label_Min = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStatus});
            this.Status.Location = new System.Drawing.Point(0, 240);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(372, 22);
            this.Status.TabIndex = 4;
            this.Status.Text = "statusStrip1";
            // 
            // ToolStatus
            // 
            this.ToolStatus.Name = "ToolStatus";
            this.ToolStatus.Size = new System.Drawing.Size(42, 17);
            this.ToolStatus.Text = "Status";
            this.ToolStatus.TextChanged += new System.EventHandler(this.ToolStatus_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::L.BSwitch.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(42, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(285, 51);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadB_SPI);
            this.groupBox1.Controls.Add(this.RadB_Matrix);
            this.groupBox1.Location = new System.Drawing.Point(12, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 110);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LaserType";
            // 
            // RadB_SPI
            // 
            this.RadB_SPI.Appearance = System.Windows.Forms.Appearance.Button;
            this.RadB_SPI.Location = new System.Drawing.Point(6, 71);
            this.RadB_SPI.Name = "RadB_SPI";
            this.RadB_SPI.Size = new System.Drawing.Size(137, 33);
            this.RadB_SPI.TabIndex = 23;
            this.RadB_SPI.Text = "SPI";
            this.RadB_SPI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadB_SPI.UseVisualStyleBackColor = true;
            this.RadB_SPI.CheckedChanged += new System.EventHandler(this.RadB_SPI_CheckedChanged);
            // 
            // RadB_Matrix
            // 
            this.RadB_Matrix.Appearance = System.Windows.Forms.Appearance.Button;
            this.RadB_Matrix.Location = new System.Drawing.Point(6, 26);
            this.RadB_Matrix.Name = "RadB_Matrix";
            this.RadB_Matrix.Size = new System.Drawing.Size(137, 33);
            this.RadB_Matrix.TabIndex = 2;
            this.RadB_Matrix.Text = "Matrix";
            this.RadB_Matrix.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadB_Matrix.UseVisualStyleBackColor = true;
            this.RadB_Matrix.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Label_BeanValue);
            this.groupBox2.Controls.Add(this.Label_Middle);
            this.groupBox2.Controls.Add(this.Label_Max);
            this.groupBox2.Controls.Add(this.Label_Min);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Location = new System.Drawing.Point(176, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 109);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LaserBean";
            // 
            // Label_BeanValue
            // 
            this.Label_BeanValue.AutoSize = true;
            this.Label_BeanValue.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label_BeanValue.Location = new System.Drawing.Point(6, 25);
            this.Label_BeanValue.Name = "Label_BeanValue";
            this.Label_BeanValue.Size = new System.Drawing.Size(21, 17);
            this.Label_BeanValue.TabIndex = 4;
            this.Label_BeanValue.Text = "M";
            this.Label_BeanValue.Click += new System.EventHandler(this.Label_BeanValue_Click);
            // 
            // Label_Middle
            // 
            this.Label_Middle.AutoSize = true;
            this.Label_Middle.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label_Middle.Location = new System.Drawing.Point(83, 54);
            this.Label_Middle.Name = "Label_Middle";
            this.Label_Middle.Size = new System.Drawing.Size(21, 17);
            this.Label_Middle.TabIndex = 3;
            this.Label_Middle.Text = "M";
            this.Label_Middle.Visible = false;
            // 
            // Label_Max
            // 
            this.Label_Max.AutoSize = true;
            this.Label_Max.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label_Max.Location = new System.Drawing.Point(160, 54);
            this.Label_Max.Name = "Label_Max";
            this.Label_Max.Size = new System.Drawing.Size(21, 17);
            this.Label_Max.TabIndex = 2;
            this.Label_Max.Text = "M";
            // 
            // Label_Min
            // 
            this.Label_Min.AutoSize = true;
            this.Label_Min.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label_Min.Location = new System.Drawing.Point(6, 54);
            this.Label_Min.Name = "Label_Min";
            this.Label_Min.Size = new System.Drawing.Size(21, 17);
            this.Label_Min.TabIndex = 1;
            this.Label_Min.Text = "M";
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(6, 74);
            this.trackBar1.Maximum = 5;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(179, 30);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(383, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(383, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 33);
            this.button2.TabIndex = 10;
            this.button2.Text = "Home";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(383, 184);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 33);
            this.button3.TabIndex = 11;
            this.button3.Text = "SetVal";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(526, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(137, 33);
            this.button4.TabIndex = 12;
            this.button4.Text = "isHomed?";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(383, 99);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(137, 33);
            this.button5.TabIndex = 13;
            this.button5.Text = "LoadFile";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(383, 51);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(137, 33);
            this.button6.TabIndex = 14;
            this.button6.Text = "disConnect";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(526, 51);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(77, 33);
            this.button7.TabIndex = 15;
            this.button7.Text = "IOOpen";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(526, 148);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(77, 29);
            this.textBox1.TabIndex = 16;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(526, 184);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(77, 33);
            this.button8.TabIndex = 17;
            this.button8.Text = "Set_H";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(669, 151);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(116, 33);
            this.button9.TabIndex = 18;
            this.button9.Text = "Get_InputIO";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(526, 99);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(77, 33);
            this.button10.TabIndex = 19;
            this.button10.Text = "chkOpen";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(609, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "label1";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(669, 190);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(129, 33);
            this.button11.TabIndex = 21;
            this.button11.Text = "Get_OutputIO";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(526, 223);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(77, 33);
            this.button12.TabIndex = 22;
            this.button12.Text = "Set_L";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(372, 262);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Status);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LaserBeanSwitch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainFrm_Shown);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel ToolStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Label_BeanValue;
        private System.Windows.Forms.Label Label_Middle;
        private System.Windows.Forms.Label Label_Max;
        private System.Windows.Forms.Label Label_Min;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.RadioButton RadB_SPI;
        private System.Windows.Forms.RadioButton RadB_Matrix;
    }
}

