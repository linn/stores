namespace Linn.Stores.IoC
{
    using Linn.Common.Logging;
    using Linn.Common.Messaging.RabbitMQ.Configuration;
    using Linn.Common.Messaging.RabbitMQ.Dispatchers;
    using Linn.Stores.Messaging.Messages;
    using Linn.Stores.Resources.MessageDispatch;

    using Microsoft.Extensions.DependencyInjection;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public static class MessagingExtensions
    {
        public static IServiceCollection AddRabbitConfiguration(this IServiceCollection services)
        {
            var routingKeys = new[]
                                  {
                                      PrintConsignmentNoteMessage.RoutingKey
                                  };
            return services
                .AddSingleton<ChannelConfiguration>(
                    d => new ChannelConfiguration(
                        "orawin", 
                        routingKeys, 
                        "orawin", 
                        true, 
                        "orawin",
                        "orawin",
                        ExchangeType.Topic,
                        ExchangeType.Topic))
                .AddSingleton(d => new EventingBasicConsumer(d.GetService<ChannelConfiguration>()?.ConsumerChannel));
        }

        public static IServiceCollection AddMessageHandlers(this IServiceCollection services)
        {
            // register handlers for different message types
            return services;
        }

        public static IServiceCollection AddMessageDispatchers(this IServiceCollection services)
        {
            // register dispatchers for different message types:
            return services
                .AddTransient<IMessageDispatcher<PrintConsignmentNoteMessageResource>>(
                    provider => new RabbitMessageDispatcher<PrintConsignmentNoteMessageResource>(
                        provider.GetService<ChannelConfiguration>(),
                        provider.GetService<ILog>(),
                        PrintConsignmentNoteMessage.RoutingKey))
                .AddTransient<IMessageDispatcher<PrintInvoiceMessageResource>>(
                    provider => new RabbitMessageDispatcher<PrintInvoiceMessageResource>(
                        provider.GetService<ChannelConfiguration>(),
                        provider.GetService<ILog>(),
                        PrintInvoiceMessage.RoutingKey))
                .AddTransient<IMessageDispatcher<PrintRsnMessageResource>>(
                    provider => new RabbitMessageDispatcher<PrintRsnMessageResource>(
                        provider.GetService<ChannelConfiguration>(),
                        provider.GetService<ILog>(),
                        PrintRsnMessage.RoutingKey));
        }
    }
}
