using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Tool
{
   public class LogHelp
    {
       public static LogHelp _logMsg;


       public static LogHelp get()
        {
            if (_logMsg == null)
            {
                _logMsg = new LogHelp();
            }
            return _logMsg;
        }


        public static void WriteLogFile(string input)
        {
            string saveFolder = "Log";//日志文件保存路径  
            string fileName = DateTime.Now.ToString("yyyy-MM-dd");
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveFolder);
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            string filePath = dir + "\\" + fileName + ".txt";
            System.IO.FileInfo file = new FileInfo(filePath);

            if (!file.Exists)
            {
                using (StreamWriter sw = file.CreateText())
                {
                    //写入当前系统时间并换行
                    sw.Write("@詹宝华 {0} \r\n", DateTime.Now);
                    //写入日志内容并换行
                    sw.Write(input + "\r\n");
                    //写入------------------------------------“并换行
                    sw.Write("------------------------------------\r\n");
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw=file.AppendText())
                {
                     //写入当前系统时间并换行
                    sw.Write("@詹宝华 {0} \r\n", DateTime.Now);
                    //写入日志内容并换行
                    sw.Write(input + "\r\n");
                    //写入------------------------------------“并换行
                    sw.Write("------------------------------------\r\n");
                    sw.Flush();
                    sw.Close();
                }
            }

        }

    }
}
