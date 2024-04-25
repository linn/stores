namespace Linn.Stores.Messaging.Host
{
    using System.Threading;
    using System.Threading.Tasks;

    using Linn.Common.Logging;
    using Linn.Common.Messaging.RabbitMQ.Configuration;

    using Microsoft.Extensions.Hosting;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class Listener : BackgroundService
    {
        private readonly IModel channel;

        private readonly string queueName;

        private readonly EventingBasicConsumer consumer;

        private readonly ILog logger;

        private readonly ChannelConfiguration channelConfiguration;

        public Listener(
            EventingBasicConsumer consumer,
            ChannelConfiguration channelConfiguration,
            ILog logger)
        {
            this.queueName = "sales";
            this.consumer = consumer;
            this.logger = logger;
            this.channelConfiguration = channelConfiguration;
            this.channel = this.channelConfiguration.ConsumerChannel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.Info("Waiting for messages. To exit press CTRL+C");
            this.consumer.Received += (_, ea) =>
            {
                // switch on message RoutingKey to decide which handler to use
                // handlers process the message and return true if successful
                // or log errors and return false if unsuccessful
                bool success = ea.RoutingKey switch
                {
                    _ => false
                };

                if (success)
                {
                    // acknowledge successfully handled messages
                    this.channelConfiguration.ConsumerChannel.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    // reject problem messages
                    this.channelConfiguration.ConsumerChannel.BasicReject(ea.DeliveryTag, false);
                }
            };
            this.channel.BasicConsume(queue: $"{this.queueName}.q", autoAck: false, consumer: this.consumer);
            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            this.channelConfiguration.Connection.Dispose();
            this.logger.Info("Closing connection...");
            return Task.CompletedTask;
        }
    }
}
