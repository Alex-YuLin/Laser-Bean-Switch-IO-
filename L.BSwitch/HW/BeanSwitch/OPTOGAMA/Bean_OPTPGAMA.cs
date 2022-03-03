#define Log
#define debug
using MainFrm.Hardware.BeanSwitch.ErrCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainFrm.Hardware.BeanSwitch.define;
using Newtonsoft.Json;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

// interface
using MainFrm.Hardware.BeanSwitch;

namespace BeanSwitch.Optogama
{
    class Bean_Optogama : IBeanSw
    {
        ///////////////////////////
        ///         Para        ///
        ///////////////////////////

        #region Para

        SerialPort gPort;
        string FilePath = Application.StartupPath + "\\SetFile\\S_Optogama.ini";
        info gInfo;
        string gRecData = "";
        Stopwatch Timer = new Stopwatch();
        bool isRecDone = false;
        bool gisBusy = false;
        Label gLabel;

        List<define.points> gSPoints;

        #endregion


        ///////////////////////////
        ///         Sub         ///
        ///////////////////////////

        #region Sub

        void ReciveData(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Byte[] buffer = new Byte[gPort.BytesToRead];
            gPort.Read(buffer, 0, gPort.BytesToRead);

            gRecData = gRecData + Encoding.ASCII.GetString(buffer);
            isRecDone = true;
        }

        byte[] ToBytes(int value)
        {
            int intValue = value;
            byte[] intBytes = BitConverter.GetBytes(intValue);

            return intBytes;
        }
        byte[] ToBytes(string value)
        {
            string author = value;
            // Convert a C# string to a byte array  
            byte[] bytes = Encoding.ASCII.GetBytes(author);
            return bytes;
        }
        byte ToByte(char value)
        {
            byte tt = Convert.ToByte(value);
            return tt;
        }

        byte[] AddBytes(byte[] bMain, byte[] bAdd)
        {
            byte[] newBytes = new byte[bMain.Length + bAdd.Length];
            bMain.CopyTo(newBytes, 0);
            bAdd.CopyTo(newBytes, bMain.Length);

            return newBytes;
        }
        byte[] AddBytes(byte bMain, byte[] bAdd)
        {
            byte[] newBytes = new byte[bAdd.Length + 1];
            newBytes[0] = bMain;
            bAdd.CopyTo(newBytes, 1);

            return newBytes;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd">不包含 chk sum </param>
        /// <param name="Rec"></param>
        /// <returns></returns>
        bool Cmd(byte[] cmd, out List<byte> Rec)
        {
            Rec = new List<byte>();
            try
            {

                gPort.Write(cmd, 0, cmd.Length);
                System.Threading.Thread.Sleep(500);

                
                int count = gPort.BytesToRead;
                int intReturnASCII = 0;
                while (count > 0)
                {
                    intReturnASCII = gPort.ReadByte();
                    Rec.Add(Convert.ToByte(intReturnASCII));
                    count--;
                }


            }
            catch (Exception ee)
            {
                return false;
            }
            return true;
        }

        bool Cmd(string cmd, out string rec)
        {
            rec = "";
            try
            {
                if (cmd == "" || cmd == null)
                    return false;

                //
                cmd = "MEX>" + cmd + "\r\n";
                gPort.Write(cmd);

                Console.WriteLine("cmd: " + cmd);
                System.Threading.Thread.Sleep(500);


                int count = gPort.BytesToRead;
                int intReturnASCII = 0;
                List<byte> tamp = new List<byte>();
                while (count > 0)
                {
                    tamp.Add( Convert.ToByte(  gPort.ReadByte()));
                    count--;
                }

                rec = Encoding.ASCII.GetString(tamp.ToArray());

            }
            catch (Exception ee)
            {
                return false;
            }
            return true;
        }

        #endregion


        ///////////////////////////
        ///         Method      ///
        ///////////////////////////

        public override RetErr Open()
        {
            RetErr ret = new RetErr();
            try
            {
                #region ReadPara

                if (!File.Exists(FilePath))
                {
                    gInfo = new info();

                    string str = JsonConvert.SerializeObject(gInfo, Newtonsoft.Json.Formatting.Indented);
                    StreamWriter strw = new StreamWriter(FilePath, false, Encoding.Unicode);
                    strw.Write(str);
                    strw.Close();

                }
                else
                {
                    StreamReader strr = new StreamReader(FilePath, Encoding.ASCII);
                    string str =  strr.ReadToEnd();
                    gInfo = JsonConvert.DeserializeObject<info>(str);
                    strr.Close();
                }


                #endregion

                #region Set SerialPort

                gPort = new SerialPort();
                gPort.PortName = "COM" + gInfo.Port.ToString();
                gPort.BaudRate = (int)gInfo.Baud;
                gPort.DataBits = 8;
                gPort.StopBits = StopBits.One;
                gPort.ReadTimeout = 400;

                #endregion

                #region Open Serial

                gPort.Open();

                #endregion
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._Open,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._Open;
                ret.Meg = "Altechna Bean Open";
                return ret;
            }
            return ret;
        }
        public override RetErr Close()
        {
            RetErr ret = new RetErr();
            try
            {
                if(gPort.IsOpen)
                    gPort.Close();
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._Close,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._Close;
                ret.Meg = "Altechna Bean close";
                return ret;
            }
            return ret;
        }

        /// <summary>
        /// 必須在讀取檔案之後才能進
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override RetErr GetMBE(out double value)
        {
            RetErr ret = new RetErr();
            value = 0;
            define.points index = new define.points();
            try
            {
                var t = false;
                double Get_Value = 0;
                string rec = "";

                // MAG
                t = Cmd("MAG?", out rec) ? true : throw new Exception("Get MAG:" + "fail");
                Get_Value = Convert.ToDouble(rec.Replace("MEX>MAG_", ""));
                index.MAG = Get_Value;

                // MOF
                t = Cmd("MOF?" , out rec) ? true : throw new Exception("Get MOF:"  + "fail");
                Get_Value = Convert.ToDouble(rec.Replace("MEX>MOF_", ""));
                index.MOF = Get_Value;

                // DOF
                t = Cmd("DOF?", out rec) ? true : throw new Exception("Get DOF:" + "fail");
                Get_Value = Convert.ToDouble(rec.Replace("MEX>DOF_", ""));
                index.DOF = Get_Value;

                // get table index
                foreach(var om in gSPoints)
                {
                    if (index.MAG == om.MAG &&
                        index.MOF == om.MOF &&
                        index.DOF == om.DOF)
                    {
                        value = om.index;
                        gLabel.Text = "index: " + om.index + ", MAG: " + om.MAG.ToString() + ", DOF: " + om.DOF.ToString();
                        break;
                    }
                    else
                        value = 0;
                }

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._GetMBE,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._GetMBE;
                ret.Meg = "Altechna Bean _GetMBE";
                return ret;
            }
            return ret;
        }

        public override RetErr Home()
        {
            RetErr ret = new RetErr();
            try
            {
                

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._Home,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._Home;
                ret.Meg = "Altechna Bean _Home";
                return ret;
            }
            return ret;
        }

        public override RetErr init()
        {
            RetErr ret = new RetErr();
            try
            {

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._init,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._init;
                ret.Meg = "Altechna Bean _init";
                return ret;
            }
            return ret;
        }

        public override bool isBusy()
        {
            bool ret = false;
            try
            {
                // status : bit0 > Optical elements in motion
                var t = Cmd("STATUS?", out string rec) ? true : throw new Exception("STATUS Fault");
                int value = Convert.ToInt32(rec.Replace("DIS_COF_DIRECT_ERR_", ""));
                ret = (value & 0x01) == 1 ? true : false;

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._SetMBE,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                return false;
            }
            return ret;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"> 0 ~ n </param>
        /// <returns></returns>
        public override RetErr SetMBE(int index)
        {
            RetErr ret = new RetErr();
            try
            {
                var t = false;
                double Get_Value = 0;
                string rec = "";

                // MAG
                t = Cmd("MAG!_" + gSPoints[index].MAG.ToString(), out rec) ? true : throw new Exception("Set MAG:" + gSPoints[index].MAG.ToString() + "fail");
                Get_Value = Convert.ToDouble(rec.Replace("MEX>MAG_", ""));
                if (Get_Value != gSPoints[index].MAG)
                    throw new Exception("Set MAG:" + gSPoints[index].MAG.ToString() + ", Get: " + Get_Value.ToString() + ", fail");

                // MOF
                t = Cmd("MOF!_" + gSPoints[index].MOF.ToString(), out rec) ? true : throw new Exception("Set MOF:" + gSPoints[index].MOF.ToString() + "fail");
                Get_Value = Convert.ToDouble(rec.Replace("MEX>MOF_", ""));
                if (Get_Value != gSPoints[index].MOF)
                    throw new Exception("Set MOF:" + gSPoints[index].MOF.ToString() + ", Get: " + Get_Value.ToString() + ", fail");

                // DOF
                t = Cmd("DOF!_" + gSPoints[index].DOF.ToString(), out rec) ? true : throw new Exception("Set MOF:" + gSPoints[index].DOF.ToString() + "fail");
                Get_Value = Convert.ToDouble(rec.Replace("MEX>DOF_", ""));
                if (Get_Value != gSPoints[index].DOF)
                    throw new Exception("Set MOF:" + gSPoints[index].DOF.ToString() + ", Get: " + Get_Value.ToString() + ", fail");
                
                var om = gSPoints[index];

                gLabel.Text = gLabel.Text + ", MAG: " + om.MAG.ToString() + ", DOF: " + om.DOF.ToString();

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._SetMBE,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._SetMBE;
                ret.Meg = "Altechna Bean _SetMBE";
                return ret;
            }
            return ret;
        }

        public override bool isHomed()
        {
            bool ret = false;
            try
            {
                
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._SetMBE,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                return false;
            }
            return ret;
        }

        /// <summary>
        /// Load .xml File
        /// </summary>
        /// <returns></returns>
        public override RetErr LoadFile()
        {
            RetErr ret = new RetErr();
            try
            {
                string path = Application.StartupPath + "\\SetFile\\SinglePoints_opto.ini";
                if (!File.Exists(path))
                {
                    List<define.points> tt = new List<define.points>();
                    tt.Add(new define.points());
                    tt.Add(new define.points());

                    string str = JsonConvert.SerializeObject(tt, Newtonsoft.Json.Formatting.Indented);

                    StreamWriter strw = new StreamWriter(path);
                    strw.Write(str);
                    strw.Close();
                    MessageBox.Show("請重新確認各point值");
                    return ret;
                }

                StreamReader strr = new StreamReader(path);
                string ori = strr.ReadToEnd();
                strr.Close();
                gSPoints = JsonConvert.DeserializeObject<List<define.points>>(ori);


#if debug
                foreach (var om in gSPoints)
                {
                    string ar = "index: " + om.index + " ,MAG: " + om.MAG + " ,DOF: " + om.DOF + " ,MOF: " + om.MOF;
                    define.LOG.Pushlist("FileRead", ar);
                }
#endif 


            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._LoadFile,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._LoadFile;
                ret.Meg = "Altechna Bean _LoadFile";
                return ret;
            }
            return ret;
        }

        public override int GetPointsCunt()
        {
            return gSPoints.Count;
        }

        public override void SetStatus(Label label)
        {
            gLabel = label;
        }

        public override void SetStatusStr(string str)
        {
            gLabel.Text = str;
        }
    }
}

namespace BeanSwitch.Optogama.define
{
    struct points
    {
        public uint index;
        public double MAG;
        public double DOF;
        public double MOF;
    }
    enum XmlType
    {
        idel,CustomPreset, PointNumber, Expansion, Divergence,
    }

    class LOG
    {
        public static void Pushlist(string Who, string what)
        {
            //初始化字串
            string NGString = "";
            string NGtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


            // 重組字串
            NGString = "#" + NGtime + "\t" + Who + ":\t" + what;

            //寫入文檔
            string pat = Application.StartupPath + "\\Log\\SinglePoints_optogama.log";
            StreamWriter strr = new StreamWriter(pat, true, Encoding.Default);
            strr.WriteLine(NGString, false);
            strr.Close();

        }
    }
}