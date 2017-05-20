using System;
using System.Web;
using System.IO;
using System.Text;

namespace ActiveLearning.Business.Common
{
    public class LogUtil
    {
        public async static void WriteLog(string log)
        {
            string logFilePath = HttpRuntime.AppDomainAppPath + "/logs/log.txt";

            string writeLog = "===================================================================" + Environment.NewLine;
            writeLog += DateTime.Now.ToString() + Environment.NewLine;
            writeLog += log + Environment.NewLine;
            writeLog += "===================================================================" + Environment.NewLine;


            FileStream fs;
            if (!new FileInfo(logFilePath).Exists)
            {
                fs = new FileStream(HttpRuntime.AppDomainAppPath + "/logs/log.txt", FileMode.Create);
            }
            else
            {
                fs = new FileStream(HttpRuntime.AppDomainAppPath + "/logs/log.txt", FileMode.Append);
            }


            byte[] info = new UTF8Encoding(true).GetBytes(writeLog);
            await fs.WriteAsync(info, 0, info.Length);
            fs.Close();
            fs.Dispose();
        }
    }
}