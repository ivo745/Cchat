using System;

namespace CchatLog
{
    public class CchatLog
    {
        private void createLog(string path)
        {
            path = (System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) + "Cchat";
            Directory.CreateDirectory(path);
        }
    }
}
