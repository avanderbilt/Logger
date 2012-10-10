using System;

namespace LoggerLibrary
{
    public class ProvideDateTime : IProvideDateTime
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        public string GetDateTimeStamp(DateTime dateTime)
        {
            var dateTimeStamp = string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}", dateTime.Year, dateTime.Month,
                                              dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

            return dateTimeStamp;
        }

        public string GetDateTimeHeading(DateTime dateTime)
        {
            var dateTimeHeading = string.Format("{0} {1}", dateTime.ToShortTimeString(), dateTime.ToShortDateString());

            return dateTimeHeading;
        }
    }
}