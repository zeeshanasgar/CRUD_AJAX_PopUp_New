using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BL
{
    public class WriteLogFile
    {
        public static void WriteLog()
        {
            try
            {
                var uploadDirFolder = "LogFile";
                string filePath = AppDomain.CurrentDomain.BaseDirectory + uploadDirFolder;
                if(!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string sFileName = filePath + "\\DummyFile.txt";
                File.AppendAllText(sFileName, "Log Written Date" + DateTime.Now.ToString() + "\r\n");
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
