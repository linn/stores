namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Logging;

    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Linn.Common.Logging.ConsoleLog>().As<ILog>().SingleInstance();
        }
    }
}
