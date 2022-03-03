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

namespace BeanSwitch.Altechna
{
    class Bean_Altechna : IBeanSw
    {
        ///////////////////////////
        ///         Para        ///
        ///////////////////////////

        #region Para

        SerialPort gPort;
        string FilePath = Application.StartupPath + "\\SetFile\\S_Altechna.ini";
        info gInfo;
        string gRecData = "";
        Stopwatch Timer = new Stopwatch();
        bool isRecDone = false;
        bool gisBusy = false;

        List<define.SinglePoint> gSPoints;

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

        private static short calcCrc(byte[] data)
        {
            unchecked
            {
                short crc = 0;
                for (int a = 0; a < data.Length; a++)
                {
                    crc ^= (short)(data[a] << 8);
                    for (int i = 0; i < 8; i++)
                    {
                        if ((crc & 0x8000) != 0)
                            crc = (short)((crc << 1) ^ 0x1021);
                        else
                            crc = (short)(crc << 1);
                    }
                }
                return crc;
            }
        }

        private byte[] CombineCmd(byte start, byte[] length, byte[] ASCll, byte[] Data, byte[] CRC)
        {
            byte[] _length = new byte[] { length[0], length[1] };
            byte[] _CRC = new byte[] { CRC[0], CRC[1] };
            

            byte[] newBytes = AddBytes(start, _length);
            newBytes = AddBytes(newBytes, ASCll);
            if(Data != null)
                newBytes = AddBytes(newBytes, Data);
            newBytes = AddBytes(newBytes, _CRC);

            return newBytes;
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

        public override RetErr GetMBE(out double value)
        {
            RetErr ret = new RetErr();
            value = 0;
            try
            {
               
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
                byte start = ToByte('@');
                byte[] ASCll = ToBytes("hob");
                byte[] length = ToBytes(ASCll.Length);

                short sCRC = calcCrc(ASCll);
                byte[] CRC = ToBytes(sCRC);

                byte[] cmd = CombineCmd(start, length, ASCll, null, CRC);

                Cmd(cmd, out List<byte> rec);
                Console.WriteLine("rec = 0x" + Convert.ToString(Convert.ToInt32(rec[0]), 16));

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


                byte start = ToByte('@');
                byte[] ASCll = ToBytes("osb");
                byte[] _ASCll = ASCll.Length == 2 ? new byte[] { ASCll[0], ASCll[1], ASCll[1] } : ASCll;
                byte[] length = ToBytes(3);
                short sCRC = calcCrc(_ASCll);
                byte[] CRC = ToBytes(sCRC);

                byte[] cmd = CombineCmd(start, length, _ASCll, null, CRC);

                Cmd(cmd, out List<byte> rec);

                // 處理資料
                byte[] tamp = new byte[rec.Count - 6];
                rec.CopyTo(4, tamp, 0, rec.Count - 6);

                //Console.WriteLine("Cunt: " + tamp.Length);
                //foreach (var on in tamp)
                //{
                //    //Console.WriteLine("rec = " + Convert.ToChar(on));
                //    Console.WriteLine("rec = 0x" + Convert.ToString(on, 16));
                //}

                // 分割個別狀態
                byte[] ExpStatus = new byte[4];
                byte[] DivStatus = new byte[4];

                List<byte> bM = tamp.ToList();
                bM.CopyTo(0, ExpStatus, 0, 4);
                bM.CopyTo(8, DivStatus, 0, 4);

                // 判別是否在 home(bit 20)
                byte ExtS = ExpStatus[0];
                //Console.WriteLine("0x" + Convert.ToString(ExtS, 2));
                bool isExpH = (ExtS & 0x01 << 0) >> 0 == 1 ? true : false;


                // 判別是否在 home(bit 20)
                byte DivS = DivStatus[0];
                //Console.WriteLine(Convert.ToString(DivS, 2));
                bool isDivH = (ExtS & 0x01 << 0) >> 0 == 1 ? true : false;

                ret = isExpH && isDivH;
                Console.WriteLine(ret);
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
                // expansion
                byte start = ToByte('@');
                byte[] ASCll = ToBytes("rad");
                byte[] Data = ToBytes(gSPoints[index].Exp);
                byte[] length = ToBytes(ASCll.Length + Data.Length);
                ASCll = AddBytes(ASCll, Data);
                short sCRC = calcCrc(ASCll);
                byte[] CRC = ToBytes(sCRC);

                byte[] cmd = CombineCmd(start, length, ASCll, Data, CRC);

                Cmd(cmd, out List<byte> rec);
                Console.WriteLine("rec = 0x" + Convert.ToString(Convert.ToInt32(rec[0]), 16));

                // divergence
                start = ToByte('@');
                ASCll = ToBytes("ra2");
                Data = ToBytes(gSPoints[index].Div);
                length = ToBytes(ASCll.Length + Data.Length);
                ASCll = AddBytes(ASCll, Data);
                sCRC = calcCrc(ASCll);
                CRC = ToBytes(sCRC);

                cmd = CombineCmd(start, length, ASCll, Data, CRC);

                Cmd(cmd, out rec);
                Console.WriteLine("rec = 0x" + Convert.ToString(Convert.ToInt32(rec[0]), 16));
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
                

                byte start = ToByte('@');
                byte[] ASCll = ToBytes("osb");
                byte[] _ASCll = ASCll.Length == 2 ? new byte[] { ASCll[0], ASCll[1], ASCll[1] } : ASCll;
                byte[] length = ToBytes(3);
                short sCRC = calcCrc(_ASCll);
                byte[] CRC = ToBytes(sCRC);

                byte[] cmd = CombineCmd(start, length, _ASCll, null, CRC);

                Cmd(cmd, out List<byte> rec);
               
                // 處理資料
                byte[] tamp = new byte[rec.Count - 6];
                rec.CopyTo(4, tamp, 0, rec.Count - 6);

                Console.WriteLine("Cunt: " + tamp.Length);
                foreach (var on in tamp)
                {
                    //Console.WriteLine("rec = " + Convert.ToChar(on));
                    Console.WriteLine("rec = 0x" + Convert.ToString(on, 16));
                }

                // 分割個別狀態
                byte[] ExpStatus = new byte[4];
                byte[] DivStatus = new byte[4];

                List<byte> bM = tamp.ToList();
                bM.CopyTo(0, ExpStatus, 0, 4);
                bM.CopyTo(8, DivStatus, 0, 4);

                // 判別是否在 home(bit 20)
                byte ExtS = ExpStatus[2];
                Console.WriteLine("0x"+ Convert.ToString(ExtS, 2));
                bool isExpH = (ExtS & 0x01 << 5) >> 5 == 1 ? true : false;


                // 判別是否在 home(bit 20)
                byte DivS = DivStatus[3];
                Console.WriteLine(Convert.ToString(DivS, 2));
                bool isDivH = (ExtS & 0x01 << 5) >> 5 == 1 ? true : false;

                ret = isExpH && isDivH;
                Console.WriteLine(ret);
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
                // Xml reader
                XmlTextReader reader = null;
                string path = Application.StartupPath + "\\SetFile\\SinglePoints.xml";
                
                // Single Point
                define.SinglePoint SPoint = new define.SinglePoint();
                define.XmlType Xmltype = define.XmlType.idel;
                List<define.SinglePoint> SPoints = new List<define.SinglePoint>();
                
                // Load the reader with the data file and ignore all white space nodes.
                reader = new XmlTextReader(path);
                reader.WhitespaceHandling = WhitespaceHandling.None;

                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                                
                            if (reader.Name == "CustomPreset")
                                SPoint = new define.SinglePoint();
                            else if (reader.Name == "PointNumber")
                                Xmltype = define.XmlType.PointNumber;
                            else if (reader.Name == "Expansion")
                                Xmltype = define.XmlType.Expansion;
                            else if (reader.Name == "Divergence")
                                Xmltype = define.XmlType.Divergence;

                            break;
                        case XmlNodeType.Text:

                            if (Xmltype == define.XmlType.PointNumber)
                                SPoint.index = Convert.ToInt32(reader.Value);
                            else if (Xmltype == define.XmlType.Expansion)
                                SPoint.Exp = Convert.ToInt32(reader.Value);
                            else if (Xmltype == define.XmlType.Divergence)
                                SPoint.Div = Convert.ToInt32(reader.Value);
                            break;

                        case XmlNodeType.EndElement:

                            if (reader.Name == "CustomPreset")
                                SPoints.Add(SPoint);

                            break;
                    }
                }

                gSPoints = SPoints;

#if debug
                string p = Application.StartupPath + "\\Log\\SinglePoints.log";
                StreamWriter strr = new StreamWriter(p);
                foreach (var om in SPoints)
                {
                    strr.WriteLine("index: " + om.index + " ,Exp: " + om.Exp + " ,Div: " + om.Div);
                }
                strr.Close();
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
            throw new NotImplementedException();
        }

        public override void SetStatusStr(string str)
        {
            throw new NotImplementedException();
        }
    }
}

namespace BeanSwitch.Altechna.define
{
    struct SinglePoint
    {
        public int index;
        public int Exp;
        public int Div;
    }
    enum XmlType
    {
        idel,CustomPreset, PointNumber, Expansion, Divergence,
    }
}