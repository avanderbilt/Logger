using System.Configuration;
using Aav.Configuration;

namespace Logger
{
    public class LoggerSection : ConfigurationSection
    {
        [ConfigurationProperty("logFileName", DefaultValue = "log.txt")]
        [PathValidator]
        public string LogFileName
        {
            get { return (string) this["logFileName"]; }
            set { this["logFileName"] = value; }
        }

        [ConfigurationProperty("windowOpacity", DefaultValue = 0.90)]
        [FloatingPointValidator]
        public double WindowOpacity
        {
            get { return (double) this["windowOpacity"]; }
            set { this["windowOpacity"] = value; }
        }

        [ConfigurationProperty("defaultInterval", DefaultValue = 100)]
        [IntegerValidator(MinValue = 0)]
        public int DefaultInterval
        {
            get { return (int)this["defaultInterval"]; }
            set { this["defaultInterval"] = value; }
        }
    }
}