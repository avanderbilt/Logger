using System.Configuration;
using LoggerLibrary;
using NUnit.Framework;

namespace LoggerTests
{
    [TestFixture]
    class ConfigurationFileReaderTests
    {
        private const string LogFileNameConfigurationKey = "LogFileName";
        private const string WindowOpacityConfigurationKey = "WindowOpacity";

        [Test]
        public void CanReadConfigurationFile()
        {
            var reader = new ReadConfigurationFile();

            var logFileName = reader.ReadAppSetting(LogFileNameConfigurationKey);
            Assert.AreEqual(ConfigurationManager.AppSettings[LogFileNameConfigurationKey], logFileName);

            var windowOpacity = reader.ReadAppSetting(WindowOpacityConfigurationKey);
            Assert.AreEqual(ConfigurationManager.AppSettings[WindowOpacityConfigurationKey], windowOpacity);
        }
    }
}