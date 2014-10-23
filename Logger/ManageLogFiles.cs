using System;
using System.IO;
using Aav.ExtensionMethods;

namespace Logger
{
    public class ManageLogFiles : IManageLogFiles
    {
        private bool _newFile;

        public ManageLogFiles()
        {
            _newFile = !File.Exists(Program.Configuration.LogFileName);
        }

        public void WriteLine(string logEntry)
        {
            using (var sw = new StreamWriter(Program.Configuration.LogFileName, true))
            {
                var now = DateTime.Now;

                if (_newFile)
                {
                    sw.WriteLine("# Log, Created {0}{1}", now.ToLiterateString(), Environment.NewLine);
                    _newFile = false;
                }

                sw.WriteLine("## {0}{2}{2}{1}{2}", now.ToSortableString(), logEntry, Environment.NewLine);
            }
        }

        public bool Exists()
        {
            return File.Exists(Program.Configuration.LogFileName);
        }

        public void Delete()
        {
            if (!Exists())
                return;

            File.Delete(Program.Configuration.LogFileName);
        }

        public void Archive()
        {
            if (!Exists())
                return;

            var newFileName = GetArchiveFileName(DateTime.Now);

            if (File.Exists(newFileName))
                throw new Exception("Archive file already exists.");

            File.Move(Program.Configuration.LogFileName, newFileName);
        }

        public string GetArchiveFileName(DateTime dateTime)
        {
            var dateString = string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}", dateTime.Year, dateTime.Month,
                                           dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

            var archiveFileName = string.Format("{0}.{1}", Program.Configuration.LogFileName, dateString);

            return archiveFileName;
        }
    }
}