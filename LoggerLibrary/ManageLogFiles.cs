using System;
using System.IO;

namespace LoggerLibrary
{
    public class ManageLogFiles : IManageLogFiles
    {
        private readonly IManageConfiguration _configurationManager;
        private readonly IManageFiles _fileManager;
        private readonly IProvideDateTime _dateTimeProvider;

        private bool _newFile;

        public ManageLogFiles(IManageConfiguration configurationManager, IManageFiles fileManager, IProvideDateTime dateTimeProvider)
        {
            _configurationManager = configurationManager;
            _fileManager = fileManager;
            _dateTimeProvider = dateTimeProvider;

            _newFile = !_fileManager.Exists(_configurationManager.FileName);
        }

        public void WriteLine(string logEntry)
        {
            var now = _dateTimeProvider.Now;

            using (var sw = new StreamWriter(_configurationManager.FileName, true))
            {
                if (_newFile)
                {
                    sw.WriteLine(string.Format(".LOG{0}", Environment.NewLine));
                    _newFile = false;
                }

                sw.WriteLine(string.Format("{0} {1}{2}{3}{4}{5}", now.ToShortTimeString(), now.ToShortDateString(),
                                           Environment.NewLine, Environment.NewLine, logEntry, Environment.NewLine));
                sw.Close();
            }
        }

        public bool Exists()
        {
            return _fileManager.Exists(_configurationManager.FileName);
        }

        public void Delete()
        {
            if (!Exists())
                return;

            _fileManager.Delete(_configurationManager.FileName);
        }

        public void Archive()
        {
            if (!Exists())
                return;

            var newFileName = GetArchiveFileName(_dateTimeProvider.Now);

            if (_fileManager.Exists(newFileName))
                throw new Exception("Archive file already exists.");

            _fileManager.Move(_configurationManager.FileName, newFileName);
        }

        public string GetArchiveFileName(DateTime dateTime)
        {
            var dateString = _dateTimeProvider.GetDateTimeStamp(dateTime);

            var archiveFileName = string.Format("{0}.{1}", _configurationManager.FileName, dateString);

            return archiveFileName;
        }
    }
}