namespace PCI_L122DSF_D122
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
            this.components = new System.ComponentModel.Container();
            this.gBox_DI = new System.Windows.Forms.GroupBox();
            this.lbl_Baudrate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_Initial = new System.Windows.Forms.Button();
            this.btn_CheckSlave = new System.Windows.Forms.Button();
            this.cBox_baudrate = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.cBox_cardno = new System.Windows.Forms.ComboBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.gBox_DO = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBox_DI
            // 
            this.gBox_DI.Enabled = false;
            this.gBox_DI.Location = new System.Drawing.Point(141, 10);
            this.gBox_DI.Margin = new System.Windows.Forms.Padding(2);
            this.gBox_DI.Name = "gBox_DI";
            this.gBox_DI.Padding = new System.Windows.Forms.Padding(2);
            this.gBox_DI.Size = new System.Drawing.Size(400, 164);
            this.gBox_DI.TabIndex = 91;
            this.gBox_DI.TabStop = false;
            this.gBox_DI.Text = "DI";
            // 
            // lbl_Baudrate
            // 
            this.lbl_Baudrate.AutoSize = true;
            this.lbl_Baudrate.Location = new System.Drawing.Point(10, 92);
            this.lbl_Baudrate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Baudrate.Name = "lbl_Baudrate";
            this.lbl_Baudrate.Size = new System.Drawing.Size(98, 12);
            this.lbl_Baudrate.TabIndex = 88;
            this.lbl_Baudrate.Text = "Baudrate of Ring0 :";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_Initial
            // 
            this.btn_Initial.Location = new System.Drawing.Point(12, 58);
            this.btn_Initial.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Initial.Name = "btn_Initial";
            this.btn_Initial.Size = new System.Drawing.Size(91, 25);
            this.btn_Initial.TabIndex = 78;
            this.btn_Initial.Text = "Initial";
            this.btn_Initial.UseVisualStyleBackColor = true;
            this.btn_Initial.Click += new System.EventHandler(this.btn_Initial_Click);
            // 
            // btn_CheckSlave
            // 
            this.btn_CheckSlave.Enabled = false;
            this.btn_CheckSlave.Location = new System.Drawing.Point(12, 130);
            this.btn_CheckSlave.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CheckSlave.Name = "btn_CheckSlave";
            this.btn_CheckSlave.Size = new System.Drawing.Size(91, 25);
            this.btn_CheckSlave.TabIndex = 89;
            this.btn_CheckSlave.Text = "Check Slave Device";
            this.btn_CheckSlave.UseVisualStyleBackColor = true;
            this.btn_CheckSlave.Click += new System.EventHandler(this.btn_CheckSlave_Click);
            // 
            // cBox_baudrate
            // 
            this.cBox_baudrate.FormattingEnabled = true;
            this.cBox_baudrate.Items.AddRange(new object[] {
            "2.5MHZ [11]",
            "5MHZ [10]",
            "10MHZ [01]",
            "20MHz [00]"});
            this.cBox_baudrate.Location = new System.Drawing.Point(12, 106);
            this.cBox_baudrate.Margin = new System.Windows.Forms.Padding(2);
            this.cBox_baudrate.Name = "cBox_baudrate";
            this.cBox_baudrate.Size = new System.Drawing.Size(92, 20);
            this.cBox_baudrate.TabIndex = 87;
            this.cBox_baudrate.SelectedValueChanged += new System.EventHandler(this.cBox_baudrate_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_Initial);
            this.groupBox3.Controls.Add(this.lbl_Baudrate);
            this.groupBox3.Controls.Add(this.label45);
            this.groupBox3.Controls.Add(this.btn_CheckSlave);
            this.groupBox3.Controls.Add(this.cBox_baudrate);
            this.groupBox3.Controls.Add(this.cBox_cardno);
            this.groupBox3.Location = new System.Drawing.Point(9, 9);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(121, 165);
            this.groupBox3.TabIndex = 90;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Master";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(11, 21);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(51, 12);
            this.label45.TabIndex = 80;
            this.label45.Text = "Card No :";
            // 
            // cBox_cardno
            // 
            this.cBox_cardno.FormattingEnabled = true;
            this.cBox_cardno.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cBox_cardno.Location = new System.Drawing.Point(12, 35);
            this.cBox_cardno.Margin = new System.Windows.Forms.Padding(2);
            this.cBox_cardno.Name = "cBox_cardno";
            this.cBox_cardno.Size = new System.Drawing.Size(92, 20);
            this.cBox_cardno.TabIndex = 79;
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(430, 388);
            this.btn_exit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(111, 35);
            this.btn_exit.TabIndex = 92;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // gBox_DO
            // 
            this.gBox_DO.Enabled = false;
            this.gBox_DO.Location = new System.Drawing.Point(141, 197);
            this.gBox_DO.Margin = new System.Windows.Forms.Padding(2);
            this.gBox_DO.Name = "gBox_DO";
            this.gBox_DO.Padding = new System.Windows.Forms.Padding(2);
            this.gBox_DO.Size = new System.Drawing.Size(400, 164);
            this.gBox_DO.TabIndex = 93;
            this.gBox_DO.TabStop = false;
            this.gBox_DO.Text = "DO";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 436);
            this.Controls.Add(this.gBox_DO);
            this.Controls.Add(this.gBox_DI);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_exit);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBox_DI;
        private System.Windows.Forms.Label lbl_Baudrate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_Initial;
        private System.Windows.Forms.Button btn_CheckSlave;
        private System.Windows.Forms.ComboBox cBox_baudrate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cBox_cardno;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.GroupBox gBox_DO;
    }
}

