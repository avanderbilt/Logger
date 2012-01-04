using System;

namespace LoggerLibrary
{
    public interface IProvideDateTime
    {
        DateTime Now { get; }

        string GetDateTimeStamp(DateTime dateTime);
    }
}