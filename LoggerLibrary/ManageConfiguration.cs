using System;

namespace LoggerLibrary
{
    public class ManageConfiguration : IManageConfiguration
    {
        private const string LogFileNameConfigurationKey = "LogFileName";
        private const string WindowOpacityConfigurationKey = "WindowOpacity";
        private const string DefaultIntervalConfigurationKey = "DefaultInterval";

        private readonly IReadConfigurationFile _configurationFileReader;

        public string FileName { get; set; }
        public double WindowOpacity { get; set; }
        public int DefaultInterval { get; set; }

        public ManageConfiguration(IReadConfigurationFile configurationFileReader)
        {
            _configurationFileReader = configurationFileReader;

            Validate();

            FileName = _configurationFileReader.ReadAppSetting(LogFileNameConfigurationKey);
            WindowOpacity = double.Parse(_configurationFileReader.ReadAppSetting(WindowOpacityConfigurationKey));
            DefaultInterval = int.Parse(_configurationFileReader.ReadAppSetting(DefaultIntervalConfigurationKey));
        }

        private void Validate()
        {
            var fileName = _configurationFileReader.ReadAppSetting(LogFileNameConfigurationKey);

            if (string.IsNullOrEmpty(fileName))
                throw new Exception("The LogFileName configuration parameter was null or empty.");

            var windowOpacityAsString = _configurationFileReader.ReadAppSetting(WindowOpacityConfigurationKey);

            if (string.IsNullOrEmpty(windowOpacityAsString))
                throw new Exception("The WindowOpacity configuration parameter was null or empty.");

            double windowOpacity;

            var windowOpacityIsADouble = double.TryParse(windowOpacityAsString, out windowOpacity);

            if (!windowOpacityIsADouble)
                throw new Exception("The value of the WindowOpacity configuration parameter is not a valid double.");

            var defaultIntervalAsString = _configurationFileReader.ReadAppSetting(DefaultIntervalConfigurationKey);

            if (string.IsNullOrEmpty(defaultIntervalAsString))
                throw new Exception("The DefaultInterval configuration parameter was null or empty.");

            int defaultInterval;

            var defaultIntervalIsAnInteger = int.TryParse(defaultIntervalAsString, out defaultInterval);

            if (!defaultIntervalIsAnInteger)
                throw new Exception("The value of the DefaultInterval configuration parameter is not a valid integer.");
        }
    }
}