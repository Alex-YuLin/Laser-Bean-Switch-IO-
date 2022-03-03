using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using GalvoScan.Hardware.Key.Define;
using GalvoScan.Hardware.Key.ErrCode;

namespace KeyInfoSetting
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        string IgnorPath = Application.StartupPath + "\\SetFile\\IgnoreKeyInfo.ini";

        private void ShowWindow(GalvoScan.Hardware.Key.Define.HWType type)
        {

            #region Laser config

            switch (type._Laser)
            {
                case Laser.Dummy:
                    LaserConfig.SelectedIndex = 0;
                    break;
                case Laser.Coherent:
                    LaserConfig.SelectedIndex = 1;
                    break;
                case Laser.Optowave:
                    LaserConfig.SelectedIndex = 2;
                    break;
                case Laser.Mulitiwave:
                    LaserConfig.SelectedIndex = 3;
                    break;
                case Laser.SPI:
                    LaserConfig.SelectedIndex = 4;
                    break;
                case Laser.SPUV:
                    LaserConfig.SelectedIndex = 5;
                    break;
                case Laser.YVO4:
                    LaserConfig.SelectedIndex = 6;
                    break;
                case Laser.PicoUV:
                    LaserConfig.SelectedIndex = 7;
                    break;
                case Laser.na:
                    LaserConfig.SelectedIndex = 8;
                    break;
            }

            #endregion

            #region CCD config
            switch (type._CCD)
            {
                case CCD.Dummy:
                    CCDConfig.SelectedIndex = 0;
                    break;
                case CCD.Picolo:
                    CCDConfig.SelectedIndex = 1;
                    break;
                case CCD.AISYS:
                    CCDConfig.SelectedIndex = 2;
                    break;
                case CCD.na:
                    CCDConfig.SelectedIndex = 3;
                    break;
            }
            #endregion

            #region IO

            switch (type._IO)
            {
                case IO.Dummy:
                    DigitalIO.SelectedIndex = 0;
                    break;
                case IO.U500:
                    DigitalIO.SelectedIndex = 01;
                    break;
                case IO.Advantech:
                    DigitalIO.SelectedIndex = 02;
                    break;
                case IO.L112:
                    DigitalIO.SelectedIndex = 03;
                    break;
                case IO.L122:
                    DigitalIO.SelectedIndex = 04;
                    break;
                case IO.na:
                    DigitalIO.SelectedIndex = 05;
                    break;
            }
            #endregion

            #region Axis

            switch (type._Axis)
            {
                case Axis.Dummy:
                    AxesConfig.SelectedIndex = 0;
                    break;
                case Axis.U500:
                    AxesConfig.SelectedIndex = 01;
                    break;
                case Axis.A3200:
                    AxesConfig.SelectedIndex = 02;
                    break;
                case Axis.Servetronic:
                    AxesConfig.SelectedIndex = 03;
                    break;
                case Axis.UTC400:
                    AxesConfig.SelectedIndex = 04;
                    break;
                case Axis.PMC6:
                    AxesConfig.SelectedIndex = 05;
                    break;
                case Axis.ACS:
                    AxesConfig.SelectedIndex = 06;
                    break;
                case Axis.na:
                    AxesConfig.SelectedIndex = 07;
                    break;
            }
            #endregion

            #region Analog output

            switch (type._AO)
            {
                case AO.Dummy:
                    AnalogOutput.SelectedIndex = 0;
                    break;
                case AO.U500:
                    AnalogOutput.SelectedIndex = 01;
                    break;
                case AO.A3200:
                    AnalogOutput.SelectedIndex = 02;
                    break;
                case AO.L112:
                    AnalogOutput.SelectedIndex = 03;
                    break;
                case AO.L122:
                    AnalogOutput.SelectedIndex = 04;
                    break;
                case AO.na:
                    AnalogOutput.SelectedIndex = 05;
                    break;
            }
            #endregion

            #region Galvo configee

            switch(type._Galvo)
            {
                case Galvo.Dummy:
                    GalvoConfig.SelectedIndex = 0;
                    break;
                case Galvo.LightningII:
                    GalvoConfig.SelectedIndex = 01;
                    break;
                case Galvo.RTC3:
                    GalvoConfig.SelectedIndex = 02;
                    break;
                case Galvo.RTC5:
                    GalvoConfig.SelectedIndex = 03;
                    break;
                case Galvo.na:
                    GalvoConfig.SelectedIndex = 04;
                    break;
            }
            #endregion

            #region AI

            switch (type._AI)
            {
                case AI.Dummy:
                    AnalogInput.SelectedIndex = 0;
                    break;
                case AI.A3200:
                    AnalogInput.SelectedIndex = 01;
                    break;
                case AI.L112:
                    AnalogInput.SelectedIndex = 02;
                    break;
                case AI.L122:
                    AnalogInput.SelectedIndex = 03;
                    break;
                case AI.na:
                    AnalogInput.SelectedIndex = 04;
                    break;
            }
            #endregion

            XFlip.Checked = type.XFlip;
            YFlip.Checked = type.YFlip;
            XYChange.Checked = type.XYChange;
            GalvoXFlip.Checked = type.Galvo_XFlip;
            GalvoYFlip.Checked = type.Galvo_YFlip;
            GalvoXYChange.Checked = type.Galvo_XYChange;
            WobbleFlag.Checked = type.Wobble;

            VisualMarkCounts.SelectedIndex = (type.VisionCunt - 2) <0? 0 : type.VisionCunt - 2;
            ExtensionOption.Checked = type.ExtensionA;
        }

        private void LoadWindow(out HWType Otype)
        {
            HWType type = new HWType();



            #region Laser config

            switch (LaserConfig.SelectedIndex)
            {
                case 0:
                    type._Laser = Laser.Dummy;
                    break;
                case 1:
                    type._Laser = Laser.Coherent;
                    break;
                case 2:
                    type._Laser = Laser.Optowave;
                    break;
                case 3:
                    type._Laser = Laser.Mulitiwave;
                    break;
                case 4:
                    type._Laser = Laser.SPI;
                    break;
                case 5:
                    type._Laser = Laser.SPUV;
                    break;
                case 6:
                    type._Laser = Laser.YVO4;
                    break;
                case 7:
                    type._Laser = Laser.PicoUV;
                    break;
            }

            #endregion

            #region CCD config
            switch (CCDConfig.SelectedIndex)
            {
                case 0:
                    type._CCD = CCD.Dummy;
                    break;
                case 1:
                    type._CCD = CCD.Picolo;
                    break;
                case 2:
                    type._CCD = CCD.AISYS;
                    break;
                case 3:
                    type._CCD = CCD.na;
                    break;
            }
            #endregion

            #region IO

            switch (DigitalIO.SelectedIndex)
            {
                case 0:
                    type._IO = IO.Dummy;
                    break;
                case 01:
                    type._IO = IO.U500;
                    break;
                case 02:
                    type._IO = IO.Advantech;
                    break;
                case 03:
                    type._IO = IO.L112;
                    break;
                case 04:
                    type._IO = IO.L122;
                    break;
                case 05:
                    type._IO = IO.na;
                    break;
            }
            #endregion

            #region Axis

            switch (AxesConfig.SelectedIndex)
            {
                case 0:
                    type._Axis = Axis.Dummy;
                    break;
                case 01:
                    type._Axis = Axis.U500;
                    break;
                case 02:
                    type._Axis = Axis.A3200;
                    break;
                case 03:
                    type._Axis = Axis.Servetronic;
                    break;
                case 04:
                    type._Axis = Axis.UTC400;
                    break;
                case 05:
                    type._Axis = Axis.PMC6;
                    break;
                case 06:
                    type._Axis = Axis.ACS;
                    break;
                case 07:
                    type._Axis = Axis.na;
                    break;
            }
            #endregion

            #region Analog output

            switch (AnalogOutput.SelectedIndex)
            {
                case 0:
                    type._AO = AO.Dummy;
                    break;
                case 01:
                    type._AO = AO.U500;
                    break;
                case 02:
                    type._AO = AO.A3200;
                    break;
                case 03:
                    type._AO = AO.L112;
                    break;
                case 04:
                    type._AO = AO.L122;
                    break;
                case 05:
                    type._AO = AO.na;
                    break;
            }
            #endregion

            #region Galvo configee

            switch (GalvoConfig.SelectedIndex)
            {
                case 0:
                    type._Galvo = Galvo.Dummy;
                    break;
                case 01:
                    type._Galvo = Galvo.LightningII;
                    break;
                case 02:
                    type._Galvo = Galvo.RTC3;
                    break;
                case 03:
                    type._Galvo = Galvo.RTC5;
                    break;
                case 04:
                    type._Galvo = Galvo.na;
                    break;
            }
            #endregion

            #region AI

            switch (AnalogInput.SelectedIndex)
            {
                case 0:
                    type._AI = AI.Dummy;
                    break;
                case 01:
                    type._AI = AI.A3200;
                    break;
                case 02:
                    type._AI = AI.L112;
                    break;
                case 03:
                    type._AI = AI.L122;
                    break;
                case 04:
                    type._AI = AI.na;
                    break;
            }
            #endregion

            type.XFlip = XFlip.Checked;
            type.YFlip = YFlip.Checked;
            type.XYChange = XYChange.Checked;
            type.Galvo_XFlip = GalvoXFlip.Checked;
            type.Galvo_YFlip = GalvoYFlip.Checked;
            type.Galvo_XYChange = GalvoXYChange.Checked;
            type.Wobble = WobbleFlag.Checked;

            type.VisionCunt = VisualMarkCounts.SelectedIndex + 2;
            type.ExtensionA = ExtensionOption.Checked;

            Otype = type;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GalvoScan.Hardware.Key.Sentinel.Key_Sentinel _Key = new GalvoScan.Hardware.Key.Sentinel.Key_Sentinel();
            RetErr tt = _Key.Open() ;
            if (!tt.flag)
            {
                MessageBox.Show("Load Key info fail", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tt = _Key.GetHWType(out GalvoScan.Hardware.Key.Define.HWType type);
            if (!tt.flag)
            {
                MessageBox.Show("Get Key info fail", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowWindow(type);

            string limiday = _Key.GetKeyDay();
            label8.Text = "Time:  " + limiday;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }

}
