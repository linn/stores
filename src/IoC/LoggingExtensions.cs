namespace Linn.Stores.IoC
{
    using Linn.Common.Logging;

    using Microsoft.Extensions.DependencyInjection;

    public static class LoggingExtensions
    {
        public static IServiceCollection AddLog(this IServiceCollection services)
        {
#if DEBUG
            return services.AddSingleton<ILog, Linn.Common.Logging.ConsoleLog>();
#else
            return services.AddSingleton<ILog>(
                l =>
                    {
                        var sqs = l.GetRequiredService<IAmazonSQS>();
                        return new AmazonSqsLog(
                            sqs,
                            LoggingConfiguration.Environment,
                            LoggingConfiguration.MaxInnerExceptionDepth,
                            LoggingConfiguration.AmazonSqsQueueUri);
                    });
#endif
        }
    }
}
