using System;
using System.IO;

namespace LoggerLibrary
{
    public class ManageFiles : IManageFiles
    {
        public bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public void Delete(string fileName)
        {
            File.Delete(fileName);
        }

        public void Move(string fileName, string destination)
        {
            File.Move(fileName, destination);
        }

        public string ProgramDirectory
        {
            get
            {
                return Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            }
        }
    }
}