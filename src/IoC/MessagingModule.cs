namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Messaging.RabbitMQ;
    using Linn.Common.Messaging.RabbitMQ.Autofac;
    using Linn.Common.Messaging.RabbitMQ.Configuration;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Messaging.Dispatchers;

    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterConnectionBuilder();
            builder.RegisterInfiniteRetryStrategy();
            builder.RegisterConnector();
            builder.RegisterMessageDispatcher();
            builder.RegisterSender("orawin.x", "Stores Message Dispatcher");

            builder.RegisterType<PrintConsignmentNoteDispatcher>().As<IPrintConsignmentNoteDispatcher>();
            builder.RegisterType<PrintInvoiceDispatcher>().As<IPrintInvoiceDispatcher>();
            builder.RegisterType<PrintRsnDispatcher>().As<IPrintRsnService>();

            builder.RegisterType<RabbitConfiguration>().As<IRabbitConfiguration>();
            builder.RegisterType<RabbitTerminator>().As<IRabbitTerminator>();
        }
    }
}
