using System;
using System.Windows.Forms;
using Ninject;

namespace Logger
{
    static class Program
    {
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