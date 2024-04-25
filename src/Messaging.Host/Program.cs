using Linn.Sales.IoC;
using Linn.Stores.IoC;
using Linn.Stores.Messaging.Host;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
        {
            services.AddLog();
            services.AddCredentialsExtensions();
            services.AddServices();
            services.AddPersistence();
            services.AddSqsExtensions();
            services.AddRabbitConfiguration();
            services.AddMessageHandlers();
            services.AddHostedService<Listener>();
        })
    .Build();

await host.RunAsync();
