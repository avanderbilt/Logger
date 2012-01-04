using LoggerLibrary;
using Ninject.Modules;

namespace Logger
{
    public class LoggingFormModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IReadConfigurationFile>().
                To<ReadConfigurationFile>();

            Bind<IManageFiles>().
                To<ManageFiles>();

            Bind<IProvideDateTime>().
                To<ProvideDateTime>();

            Bind<IManageConfiguration>().
                To<ManageConfiguration>();

            Bind<IManageLogFiles>().
                To<ManageLogFiles>();

            Bind<LoggingForm>().
                ToSelf();
        }
    }
}