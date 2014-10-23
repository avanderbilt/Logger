using System;
using System.Configuration;
using System.Windows.Forms;
using Ninject;

namespace Logger
{
    static class Program
    {
        public static LoggerSection Configuration;

        static Program()
        {
            var namespaceName = typeof(Program).Namespace;

            if (namespaceName == null)
                return;

            Configuration = (LoggerSection)ConfigurationManager.GetSection(namespaceName);
        }

        [STAThread]
        static void Main()
        {
            var kernel = new StandardKernel(new LoggingFormModule());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(kernel.Get<LoggingForm>());
        }
    }
}