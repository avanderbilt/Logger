using Ninject.Modules;

namespace Logger
{
    public class LoggingFormModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IManageLogFiles>().
                To<ManageLogFiles>();

            Bind<LoggingForm>().
                ToSelf();
        }
    }
}