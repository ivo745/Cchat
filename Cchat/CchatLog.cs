using System;
using System.IO;

namespace Cchat
{
    public static class CchatLog
    {
        private static string Path = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Cchat");

        private const string MSG_WELCOME = "Welcome!";

        public static void CreateLog()
        {
            Directory.CreateDirectory(Path);

            if (!File.Exists(Path + "\\log"))
            {
                File.Create(Path + "\\log").Close();
            }

            if (new FileInfo(Path + "\\log").Length == 0)
            {
                WriteToLog(MSG_WELCOME);
            }
        }

        public static void WriteToLog(string text)
        {
            using (StreamWriter w = File.AppendText(Path + "\\log"))
            {
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("{0}", text);
            }
        }

        // Store all lines of the log file
        public static string[] ReadFromLog()
        {
            string[] lines = File.ReadAllLines(Path + "\\log");
            return lines;
        }
    }
}