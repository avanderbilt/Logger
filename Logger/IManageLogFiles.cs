using System;

namespace Logger
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