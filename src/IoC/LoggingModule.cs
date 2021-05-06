namespace Linn.Stores.IoC
{
    using Amazon.SQS;
    using Autofac;

    using Linn.Common.Logging;
    using Linn.Common.Logging.AmazonSqs;

    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Linn.Common.Logging.ConsoleLog>().As<ILog>().SingleInstance();
        }
    }
}
