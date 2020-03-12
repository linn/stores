namespace Linn.Stores.IoC
{
    using Amazon.SQS;
    using Autofac;

    using Linn.Common.Logging;
    using Linn.Common.Logging.AmazonSqs;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
        }
    }
}