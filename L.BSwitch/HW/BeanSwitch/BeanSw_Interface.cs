#define Log
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainFrm.Hardware.BeanSwitch.ErrCode;
using BeanSwitch.Optogama;

namespace MainFrm.Hardware.BeanSwitch
{
    class BeanSw_Interface
    {
    }
    abstract class IBeanSw
    {
        public abstract RetErr Open();
        
        public abstract RetErr Close();

        public abstract RetErr init();

        public abstract RetErr Home();

        public abstract RetErr SetMBE(int value);

        public abstract RetErr GetMBE(out double value);

        public abstract void SetStatus(Label label);
        public abstract void SetStatusStr(string str);

        public abstract RetErr LoadFile();

        public abstract int GetPointsCunt();

        public abstract bool isBusy();

        public abstract bool isHomed();

        
        
    }
}

namespace MainFrm.Hardware.BeanSwitch.define
{
    class info
    {
        public uint Port = 1;
        public uint Baud = 115200;
        public uint MaxRange = 2;
        public uint MinRange = 1;
    }
}

namespace MainFrm.Hardware.BeanSwitch.ErrCode
{
    class Num   // -401 ~ -600
    {
        public static int _Open = -401;
        public static int _Close = -402;
        public static int _init = -403;
        public static int _Home = -404;
        public static int _SetMBE = -405;
        public static int _GetMBE = -406;
        public static int _isBusy = -407;
        public static int _LoadFile = -408;
    }
    class RetErr
    {
        public Boolean flag = true;
        public int Num = 0;
        public string Meg = "";
    }
    class Log
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NGCode"> 編碼 </param>
        /// <param name="Who"> Method </param>
        /// <param name="what"> message</param>
        public static void Pushlist(int NGCode, string Who, string what)
        {
            //初始化字串
            string NGString = "";
            string NGtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


            // 重組字串
            NGString = "#" + NGtime + "\t" + NGCode.ToString() + "\t" + Who + ":\t" + what;

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
}
