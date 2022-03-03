using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainFrm
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            #region Show Laser Type

            string pat = ReadFile(Application.StartupPath + "\\SetFile\\LaserTypePath.txt");
            if (pat == "")
            {
                MessageBox.Show(Application.StartupPath + "\\SetFile\\LaserTypePath.txt" + "\r\n" +
                                "PL_Lasertype.ini fail");
                return;
            }    
            StreamReader strr = new StreamReader(pat);
            string LaserType = "";
            while (!strr.EndOfStream)
            {
                string ff = strr.ReadLine();
                if (ff.Contains("Type="))
                {
                    LaserType = ff.Split('=')[1];
                    break;
                }    
            }
            strr.Close();

            DialogResult result = DialogResult.None;
            if (LaserType == "1")
                result = MessageBox.Show("Laser : Matrix \r\n" + "是否進行設定?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            else if (LaserType == "4")
                result = MessageBox.Show("Laser : SPI \r\n" + "是否進行設定?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            if(result == DialogResult.Yes)
            {
                foreach (var pm in Process.GetProcessesByName("MainPrg"))
                {
                    pm.Kill();
                    Thread.Sleep(1000);
                    break;
                }
            }

            #endregion

            #region 判別主程式是否開啟

            Process[] process = Process.GetProcesses();
            int i = 0;
            foreach(var om in process)
            {
                if (om.ProcessName.Equals("MainPrg", StringComparison.OrdinalIgnoreCase))
                    i++;
                if (om.ProcessName.Contains("MainPrg"))
                    Console.WriteLine("ff");
            }
            if (i >= 1)
            {
                MessageBox.Show("主程式已開啟 請關閉主程式");
                return;
            }

            #endregion


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }
        static string ReadFile(string path)
        {
            if (!File.Exists(path))
                return "";

            StreamReader strr = new StreamReader(path, Encoding.Default);
            string str = strr.ReadToEnd();
            strr.Close();

            return str;
        }
    }
}
