using System;

namespace LoggerLibrary
{
    public interface IManageLogFiles
    {
        void WriteLine(string logEntry);
        bool Exists();
        void Delete();
        void Archive();
        string GetArchiveFileName(DateTime dateTime);
    }
}