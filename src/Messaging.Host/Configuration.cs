namespace Linn.Stores.Messaging.Host
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.Stores.IoC;

    public static class Configuration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AmazonCredentialsModule>();
            builder.RegisterModule<AmazonSqsModule>();
            builder.RegisterModule<LoggingModule>();
            //builder.RegisterModule<MessagingModule>();
            //builder.RegisterModule<PersistenceModule>();
            //builder.RegisterModule<ServiceModule>();
            builder.RegisterReceiver("stores.q", "stores.dlx");

            builder.RegisterType<Listener>().AsSelf();

            return builder.Build();
        }
    }
}
