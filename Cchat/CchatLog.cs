using System;
using System.IO;

namespace Cchat
{
    public static class CchatLog
    {
        private const string MSG_WELCOME = "Welcome!";

        public static void CreateLog()
        {
            Directory.CreateDirectory(Form1.Path);

            if (!File.Exists(Form1.Path + "\\log"))
            {
                File.Create(Form1.Path + "\\log").Close();
            }

            if (new FileInfo(Form1.Path + "\\log").Length == 0)
            {
                WriteToLog(MSG_WELCOME);
            }
        }

        public static void WriteToLog(string text)
        {
            using (StreamWriter w = File.AppendText(Form1.Path + "\\log"))
            {
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("{0}", text);
            }
        }

        // Store all lines of the log file
        public static string[] ReadFromLog()
        {
            string[] lines = File.ReadAllLines(Form1.Path + "\\log");
            return lines;
        }
    }
}