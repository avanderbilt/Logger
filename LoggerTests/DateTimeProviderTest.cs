using System;
using LoggerLibrary;
using NUnit.Framework;

namespace LoggerTests
{
    [TestFixture]
    class DateTimeProviderTest
    {
        [Test]
        public void DateTimeStampIsCorrect()
        {
            var now = DateTime.Now;

            var dateTimeStamp = string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}", now.Year, now.Month, now.Day,
                                              now.Hour, now.Minute, now.Second);

            var dateTimeProvider = new ProvideDateTime();

            Assert.AreEqual(dateTimeStamp, dateTimeProvider.GetDateTimeStamp(now));
        }
    }
}
