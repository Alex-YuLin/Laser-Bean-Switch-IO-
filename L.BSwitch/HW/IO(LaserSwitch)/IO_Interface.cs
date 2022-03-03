#define Log
using System;
using System.IO;
using System.Windows.Forms;
using MainFrm.Hardware.IO.ErrCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainFrm.Hardware.IO
{
    class IO_Interface
    {
    }

    abstract class IIO
    {
        public abstract RetErr Open();
        public abstract RetErr Close();
        public abstract RetErr CheckOpen(out Boolean isOpenOrNot);
        public abstract RetErr SetOutPut(uint index, Boolean state);
        public abstract RetErr GetOutPut(int index, out Boolean state);
        public abstract RetErr GetInPut(int index, out Boolean state);
        public abstract RetErr SetAnalogOutput(int index, double value);
        public abstract RetErr GetAnalogInput(int index, out double value);
    }
}


namespace MainFrm.Hardware.IO.ErrCode
{
    class Num   // -401 ~ -600
    {
        public static int  _Open = -401;
        public static int _Close = -402;
        public static int _chkConnect = -403;
        public static int _setDOutput = -404;
        public static int _GetOutPut = -405;
        public static int _GetInPut = -406;
        public static int _SetAnalogOutput = -407;
        public static int _SetAnalogInput = -408;
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
            string pat = Application.StartupPath + "\\Log\\IO_Log.log";
            StreamWriter strr = new StreamWriter(pat, true, Encoding.Default);
            strr.WriteLine(NGString, false);
            strr.Close();

            MessageBox.Show(what);

        }

       
    }
}
