#define ErrClose
#define LaserON
#define Bean
//#define KeySwitch
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MainFrm
{
    public partial class MainFrm : Form
    {
        ///////////////////////////////////
        ///             Para            ///
        ///////////////////////////////////
        ///
        #region Para

        Hardware.BeanSwitch.IBeanSw gIBeanSW;

        Hardware.IO.IIO gIIO;
        define.IOInfo gIOInfo;

        // 
        Hardware.IO.ErrCode.RetErr ret_IO;

        define.PTSetting gPTSetting;
        define.PTSetting gBasicSetting;
        define.PTSetting gAlimentSetting;

        int StartCunt = 0;

        Thread Monitor;

        delegate void delCloseFrm();

        #endregion


        ///////////////////////////////////
        ///             Sub             ///
        ///////////////////////////////////
        ///
        #region Sub

        private void MainPrg_Monitor()
        {
            while (true)
            {
                Process[] process = Process.GetProcesses();
                int i = 0;
                foreach (var om in process)
                {
                    if (om.ProcessName.Equals("MainPrg", StringComparison.OrdinalIgnoreCase))
                        i++;
                }
                if (i >= 1)
                {
                    //MessageBox.Show("主程式已開啟 請關閉主程式");

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new delCloseFrm(this.Close), new object[] { });
                    }
                    else
                        this.Close();

                    return;
                }
            }
        }

        private void RefStatus(string str, int mmSec)
        {
            Stopwatch Timer = new Stopwatch();
            ToolStatus.Text = str;

            Timer.Restart();
            while (Timer.ElapsedMilliseconds < mmSec)
                Application.DoEvents();
        }

        private string ReadFile(string path)
        {
            if (!File.Exists(path))
                return "";

            StreamReader strr = new StreamReader(path, Encoding.Default);
            string str = strr.ReadToEnd();
            strr.Close();

            return str;
        }

        private bool WriteFile(string path, string str)
        {
            try
            {
                if (!File.Exists(path))
                    throw new Exception("write file: " + path + ", fail");

                StreamWriter strw = new StreamWriter(path, false);
                strw.Write(str);
                strw.Close();
            }
            catch(Exception ee)
            {
                
                return false;
            }
            return true;
        }

        private bool WriteFile(string path, string ChangeName, int ChangeValue)
        {
            try
            {
                if (!File.Exists(path))
                    throw new Exception("write file: " + path + ", fail");
                StreamReader strr = new StreamReader(path, Encoding.ASCII);
                string str = "a";
                string outStr = "";
                while (!strr.EndOfStream)
                {
                    str = strr.ReadLine();
                    if (str.Split('=')[0] == ChangeName)
                        outStr = outStr + ChangeName + "=" + ChangeValue + "\r\n";
                    else
                        outStr = outStr + str+"\r\n";

                }
                strr.Close();


                StreamWriter strw = new StreamWriter(path, false);
                strw.Write(outStr);
                strw.Close();


            }
            catch (Exception ee)
            {

                return false;
            }
            return true;
        }

        private void FrmClose()
        {
            #region Bean

            if (gIBeanSW != null)
                gIBeanSW.Close();
            if (gIIO != null)
                gIIO.Close();
            Monitor.Abort();
            Application.Exit();
            #endregion
        }

        private void Waitmm(int mmSec)
        {
            Stopwatch _timer = new Stopwatch();
            _timer.Restart();
            while (_timer.ElapsedMilliseconds < mmSec)
                Application.DoEvents();
        }

        #endregion


        ///////////////////////////////////
        ///             Method          ///
        ///////////////////////////////////
        ///

        public MainFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Power table set
            string path = Application.StartupPath + "\\SetFile\\PowerTable\\Setting.ini";
            if (!File.Exists(path))
            {
                gPTSetting = new define.PTSetting();
                string sstr = JsonConvert.SerializeObject(gPTSetting, Formatting.Indented);
                StreamWriter strr = new StreamWriter(path);
                strr.Write(sstr);
                strr.Close();


                MessageBox.Show("File not Find\r\n" + path);
                this.Dispose();
                return;
            }

            string str = ReadFile(path);
            gPTSetting = JsonConvert.DeserializeObject<define.PTSetting>(str);

            // BasicMotion
            path = Application.StartupPath + "\\SetFile\\BasicMotion\\Setting.ini";
            if (!File.Exists(path))
            {
                MessageBox.Show("File not Find\r\n" + path);
                this.Dispose();
                return;
            }

            str = ReadFile(path);
            gBasicSetting = JsonConvert.DeserializeObject<define.PTSetting>(str);


            // Aliment
            path = Application.StartupPath + "\\SetFile\\Aliment\\Setting.ini";
            if (!File.Exists(path))
            {
                MessageBox.Show("File not Find\r\n" + path);
                this.Dispose();
                return;
            }

            str = ReadFile(path);
            gAlimentSetting = JsonConvert.DeserializeObject<define.PTSetting>(str);


            // start Monitor
            Monitor = new Thread(new ThreadStart(MainPrg_Monitor));
            Monitor.Start();

            // Read Bean Status


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            Label_BeanValue.Text = "index : " + ((decimal)trackBar1.Value).ToString();
#if Bean
            ToolStatus.Text = "Moving...";
            Application.DoEvents();
            Waitmm(10);
            gIBeanSW.SetMBE(trackBar1.Value - 1);
#endif

            // Copy BasicMotion
            int BranIndex = trackBar1.Value;
            string path = Application.StartupPath + "\\SetFile\\BasicMotion\\Matrix\\" + BranIndex.ToString() + "\\BasicMotionPara.ini";
            try
            {
                // chk
                if (!File.Exists(path)) throw new Exception("File: " + path + ", not exist");

                File.Copy(path, gBasicSetting.Target, true);

            }
            catch (Exception ee)
            {
                MessageBox.Show("Copy fail\r\n" + path + "\r\n" + ee.Message);
            }

            // Copy Correct File
            BranIndex = trackBar1.Value;
            string CorDir = Application.StartupPath + "\\SetFile\\CorrectFile\\Matrix\\" + BranIndex.ToString();
            string[] files = Directory.GetFiles(CorDir);

            foreach (var om in files)
            {
                string filename = om.Split('\\')[om.Split('\\').Length - 1];
                string destname = @"C:\TempExt\" + filename;
                try
                {
                    filename = om.Split('\\')[om.Split('\\').Length - 1];
                    destname = @"C:\TempExt\" + filename;
                    File.Copy(om, destname, true);
                }
                catch (Exception ee)
                {
                    _Log.Pushlist("Copy " + filename + ", to, " + destname + ", fail");
                    return;
                }
            }

            ToolStatus.Text = "";
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmClose();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            gIBeanSW.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            gIBeanSW.Home();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gIBeanSW.SetMBE(-123456);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gIBeanSW.isHomed();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gIBeanSW.LoadFile();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            gIBeanSW.Close();
        }

        private void MainFrm_Shown(object sender, EventArgs e)
        {
            #region Laser

            #region Build gIoInfo DB

            string IOpath = Application.StartupPath + "\\SetFile\\S_IO.ini";
            if (!File.Exists(IOpath))
            {
                gIOInfo = new define.IOInfo();
                string _str = JsonConvert.SerializeObject(gIOInfo, Formatting.Indented); 
                StreamWriter strw = new StreamWriter(IOpath);
                strw.Write(_str);
                strw.Close();
            }
            else{
                string _str = ReadFile(IOpath);
                gIOInfo = JsonConvert.DeserializeObject<define.IOInfo>(_str);
            }
            

            #endregion

            gIIO = new Hardware.IO.L122.IO_L122();
            ret_IO = gIIO.Open();
            if (!ret_IO.flag)
            {
                MessageBox.Show(ret_IO.Meg,"", MessageBoxButtons.OK, MessageBoxIcon.Error);
#if ErrClose
                FrmClose();
                this.Dispose();
#endif
            }
            
            gIIO.CheckOpen(out bool isOPen);
            if (!isOPen)
            {
                MessageBox.Show("Open IO Fail , Please open program Again", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
#if ErrClose
                FrmClose();
                this.Dispose();
#endif
                //return;
            }

#if LaserON
            // Set Laser Status
            gIIO.GetInPut(gIOInfo.MatrixPort_senser , out bool isMatrixOn);
            RadB_Matrix.Checked = isMatrixOn;

            gIIO.GetInPut(gIOInfo.SPIPort_senser , out bool isSPIOn);
            RadB_SPI.Checked = isSPIOn;
            groupBox2.Visible = !isSPIOn;

            if (!RadB_Matrix.Checked && !RadB_SPI.Checked) // 假如都沒有感應到端點, 設定Matrix
            {
                gIIO.SetOutPut((uint)gIOInfo.SPIPort, gIOInfo.SPIPort_inv == 0 ? false : true);  
                gIIO.SetOutPut((uint)gIOInfo.MatrixPort, gIOInfo.MatrixPort_inv == 0 ? true : false);
                gIIO.GetInPut(gIOInfo.MatrixPort_senser - 1, out isMatrixOn);
                while (!isMatrixOn) // 等待到位
                {
                    Application.DoEvents();
                    gIIO.GetInPut(gIOInfo.MatrixPort_senser - 1, out isMatrixOn);
                }
                RadB_Matrix.Checked = isMatrixOn;
            }
#endif


            #endregion

            #region Bean

#if Bean
            // BeanSwitch
            //gIBeanSW = new BeanSwitch.Altechna.Bean_Altechna();
            gIBeanSW = new BeanSwitch.Optogama.Bean_Optogama();
            gIBeanSW.Open();
            gIBeanSW.LoadFile();
            gIBeanSW.Home();

            while (gIBeanSW.isBusy())
            {
                Application.DoEvents();
                ToolStatus.Text = "homeing...";
            }

            // Set
            int cunt = gIBeanSW.GetPointsCunt();
            Label_Min.Text = "1";
            Label_Middle.Text = cunt % 2 == 0 ? (cunt / 2).ToString() : (cunt / 2 + 1).ToString();
            Label_Max.Text = cunt.ToString();
            trackBar1.Maximum = cunt;
            trackBar1.Minimum = 1;
            trackBar1.Value = 1;
            trackBar1.SmallChange = 1;

            gIBeanSW.SetStatus(Label_BeanValue);
            

            // get
            gIBeanSW.GetMBE(out double value);
            trackBar1.Value = (int)(value + 1);
            //gIBeanSW.SetMBE(trackBar1.Value - 1);
#endif

        #endregion

        }

        private void ToolStatus_TextChanged(object sender, EventArgs e)
        {
            //Stopwatch timer = new Stopwatch();
            //timer.Restart();
            //while (timer.ElapsedMilliseconds < 3000)
            //    Application.DoEvents();
            //ToolStatus.Text = "";
        }

        private void RadB_Matrix_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(gIIO.GetType() == null)
                gIIO = new Hardware.IO.L122.IO_L122();
            gIIO.Open();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (gIIO.GetType() == null) return;
            gIIO.CheckOpen(out bool isOpen);

            button10.BackColor = isOpen ? Color.Red : button10.BackColor;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (gIIO.GetType() == null) return;

            gIIO.SetOutPut(Convert.ToUInt32(textBox1.Text), true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (gIIO.GetType() == null) return;
            gIIO.GetInPut(Convert.ToInt32(textBox1.Text), out bool state);
            label1.Text = Convert.ToString(state);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (gIIO.GetType() == null) return;
            gIIO.GetOutPut(Convert.ToInt32(textBox1.Text), out bool state);
            label1.Text = Convert.ToString(state);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (gIIO.GetType() == null) return;

            gIIO.SetOutPut(Convert.ToUInt32(textBox1.Text), false);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StartCunt++;
            if (StartCunt < 2) return;
            if (gIIO == null) return;
#if LaserON
            string path_M = Application.StartupPath + "\\FFFFFCA9_M.upw";
#if KeySwitch
            Process tamp = RadB_Matrix.Checked ? Process.Start(path_M) : null;
#endif
            

            bool isOn = false;
            define.Laser CurLs = RadB_Matrix.Checked ? define.Laser.Matrix : define.Laser.non;
            if (CurLs == define.Laser.non) return;
            string pat = "";
            switch (CurLs)
            {
                case define.Laser.Matrix:
                    #region Copy Aliment 作備份
                    string source = "";
                    string dest = "";
                    try
                    {
                        source = gAlimentSetting.Target;
                        dest = Application.StartupPath + @"\SetFile\Aliment\Alignment_SPI.ini";
                        File.Copy(source, dest, true);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Copy fail\r\n" + source + "\r\n to \r\n" + dest + "\r\n" + ee.Message);
                        return;
                    }
                    #endregion
                    gIIO.SetOutPut((uint)gIOInfo.SPIPort, gIOInfo.SPIPort_inv == 0 ? false : true);
                    gIIO.SetOutPut((uint)gIOInfo.MatrixPort, gIOInfo.MatrixPort_inv == 0 ? true : false);
                    gIIO.SetOutPut((uint)gIOInfo.MatrixPort_Enable, true );
                    gIIO.SetOutPut((uint)gIOInfo.SPIPort_Enable, false);
                    gIIO.GetInPut(gIOInfo.MatrixPort_senser , out  isOn);
                    while (!isOn) // 等待到位
                    {
                        Application.DoEvents();
                        gIIO.GetInPut(gIOInfo.MatrixPort_senser , out isOn);
                        ToolStatus.Text = "Moving...";
                    }
                    ToolStatus.Text = "";
                    RadB_Matrix.Checked = isOn;

                    pat = ReadFile(Application.StartupPath + "\\SetFile\\LaserTypePath.txt");
                    //WriteFile(pat, "[SYSTEM]\r\nType=1");
                    WriteFile(pat, "Type", 1);
                    groupBox2.Visible = true;
                    break;
            }

            // read Current Bean Position
             Hardware.BeanSwitch.ErrCode.RetErr ret = gIBeanSW.GetMBE(out double value);
            if (!ret.flag)
                MessageBox.Show("Read MBE fail", "Err");
            trackBar1.Value = (int)value + 1;



            // Copy PowerTable
            string path = Application.StartupPath + "\\SetFile\\PowerTable\\MatrixPT.txt" ;
            try
            {
                File.Copy(path, gPTSetting.Target, true);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Copy fail\r\n" + path);
                return;
            }


            // Copy BasicMotion
            int BranIndex = trackBar1.Value;
            path = Application.StartupPath + "\\SetFile\\BasicMotion\\Matrix\\"+ BranIndex.ToString()+ "\\BasicMotionPara.ini";
            try
            {
                // chk
                if (!File.Exists(path)) throw new Exception("File: " + path + ", not exist");

                File.Copy(path, gBasicSetting.Target,true);

            }
            catch (Exception ee)
            {
                MessageBox.Show("Copy fail\r\n" + path + "\r\n" + ee.Message);
            }

            // Copy Correct File
            BranIndex = trackBar1.Value;
            string CorDir = Application.StartupPath + "\\SetFile\\CorrectFile\\Matrix\\" + BranIndex.ToString();
            string[] files = Directory.GetFiles(CorDir);

            foreach (var om in files)
            {
                string filename = om.Split('\\')[om.Split('\\').Length - 1];
                string destname = @"C:\TempExt\" + filename;
                try
                {
                    filename = om.Split('\\')[om.Split('\\').Length - 1];
                    destname = @"C:\TempExt\" + filename;
                    File.Copy(om, destname, true);
                }
                catch (Exception ee)
                {
                    _Log.Pushlist("Copy " + filename + ", to, " + destname + ", fail");
                    return;
                }
            }


            // Copy Aliment
            string source_aliment = "";
            string dest_aliment = "";
            try
            {
                source_aliment = Application.StartupPath + @"\SetFile\Aliment\Alignment_Matrix.ini";
                dest_aliment = gAlimentSetting.Target ;
                File.Copy(source_aliment, dest_aliment, true);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Copy fail\r\n" + source_aliment + "\r\n to \r\n" + dest_aliment + "\r\n" + ee.Message);
                return;
            }



            MessageBox.Show("Sucessful !");
#endif
        }

        private void RadB_SPI_CheckedChanged(object sender, EventArgs e)
        {
            StartCunt++;
            if (StartCunt < 2) return;

#if LaserON
            string path_SPI = Application.StartupPath + "\\FFFFFCA9_SPI.upw";
#if KeySwitch
            Process tamp2 = RadB_SPI.Checked ? Process.Start(path_SPI) : null;
#endif


           



            bool isOn = false;
            define.Laser CurLs = RadB_SPI.Checked ? define.Laser.SPI : define.Laser.non;
            if (CurLs == define.Laser.non) return;
            string pat = "";
            switch (CurLs)
            {
                case define.Laser.SPI:
                    #region Copy Aliment 作備份
                    string source = "";
                    string dest = "";
                    try
                    {
                        source = gAlimentSetting.Target;
                        dest = Application.StartupPath + @"\SetFile\Aliment\Alignment_Matrix.ini";
                        File.Copy(source, dest, true);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Copy fail\r\n" + source + "\r\n to \r\n" + dest + "\r\n" + ee.Message);
                        return;
                    }
                    #endregion

                    gIIO.SetOutPut((uint)gIOInfo.MatrixPort, gIOInfo.MatrixPort_inv == 0 ? false : true);
                    gIIO.SetOutPut((uint)gIOInfo.SPIPort, gIOInfo.SPIPort_inv == 0 ? true : false);
                    gIIO.GetInPut(gIOInfo.SPIPort_senser , out  isOn);
                    gIIO.SetOutPut((uint)gIOInfo.SPIPort_Enable, true);
                    gIIO.SetOutPut((uint)gIOInfo.MatrixPort_Enable, false);
                    while (!isOn) // 等待到位
                    {
                        Application.DoEvents();
                        gIIO.GetInPut(gIOInfo.SPIPort_senser , out isOn);
                        ToolStatus.Text = "Moving...";
                    }
                    RadB_SPI.Checked = isOn;

                    pat = ReadFile(Application.StartupPath + "\\SetFile\\LaserTypePath.txt");
                    //WriteFile(pat, "[SYSTEM]\r\nType=4");
                    WriteFile(pat, "Type", 4);
                    groupBox2.Visible = false;
                    break;
            }
            ToolStatus.Text = "";


            // Copy PowerTable
            string path = Application.StartupPath + "\\SetFile\\PowerTable\\SPIPT.txt";
            try
            {
                File.Copy(path, gPTSetting.Target, true);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Copy fail\r\n"+path);
                return;
            }

            // Copy BasicMotion
            path = Application.StartupPath + "\\SetFile\\BasicMotion\\BasicMotionPara_SPI.ini";
            try
            {
                File.Copy(path, gBasicSetting.Target, true);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Copy fail\r\n" + path + "\r\n"+ee.Message);
                return;
            }

            // Copy Correct File
            string CorDir = Application.StartupPath + "\\SetFile\\CorrectFile\\SPI";
            string[] files = Directory.GetFiles(CorDir);
            
            foreach (var om in files)
            {
                string filename = om.Split('\\')[om.Split('\\').Length - 1];
                string destname = @"C:\TempExt\" + filename;
                try
                {
                    filename = om.Split('\\')[om.Split('\\').Length - 1];
                    destname = @"C:\TempExt\" + filename;
                    File.Copy(om, destname, true);
                }
                catch (Exception ee)
                {
                    _Log.Pushlist("Copy " + filename + ", to, " + destname + ", fail");
                    return;
                }
            }

            // Copy Aliment
            string source_aliment = "";
            string dest_aliment = "";
            try
            {
                source_aliment = Application.StartupPath + @"\SetFile\Aliment\Alignment_SPI.ini";
                dest_aliment = gAlimentSetting.Target ;
                File.Copy(source_aliment, dest_aliment, true);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Copy fail\r\n" + source_aliment + "\r\n to \r\n" + dest_aliment + "\r\n" + ee.Message);
                return;
            }




            MessageBox.Show("Sucessful !");
#endif
        }

        private void Label_BeanValue_Click(object sender, EventArgs e)
        {

        }
    }
}

namespace MainFrm.define
{
    class IOInfo
    {
        public int MatrixPort = 5;
        public int SPIPort = 10;
        public int MatrixPort_Enable = 9;
        public int SPIPort_Enable = 8;
        public int MatrixPort_senser = 2;
        public int SPIPort_senser = 1;
        /// <summary>
        /// 1: Matrix, 2: SPI
        /// </summary>
        public int CurrLaser = 1;
        public int MatrixPort_inv = 0;
        public int SPIPort_inv = 0;
    }
    enum Laser
    {
        Matrix,SPI,non,
    }

    /// <summary>
    /// power table set
    /// </summary>
    class PTSetting
    {
        public string Target = Application.StartupPath + "\\SetFile\\LaserPowerTable.ini";
    }
}
class _Log
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="NGCode"> 編碼 </param>
    /// <param name="Who"> Method </param>
    /// <param name="what"> message</param>
    public static void Pushlist(string what,
                                [CallerMemberName] string who="",
                                [CallerLineNumber] int line=0,
                                [CallerFilePath] string path = "")
    {
        //初始化字串
        string NGString = "";
        string NGtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


        // 重組字串
        NGString = "#" + NGtime + "\t" + who.ToString() + ",\t line:" + line + ",\t" + what;

        //寫入文檔
        string pat = Application.StartupPath + "\\Log\\Log.log";
        StreamWriter strr = new StreamWriter(pat, true, Encoding.Default);
        strr.WriteLine(NGString, false);
        strr.Close();

    }


    internal static void Pushlist(string v, string cmd)
    {
        throw new NotImplementedException();
    }
}
