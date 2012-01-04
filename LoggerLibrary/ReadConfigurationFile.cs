using System.Configuration;

namespace LoggerLibrary
{
    public class ReadConfigurationFile : IReadConfigurationFile
    {
        public string ReadAppSetting(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            return value;
        }
    }
}
