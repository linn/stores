namespace Linn.Stores.Service.Host
{
    using System;

    using Autofac;

    using Linn.Common.Logging;
    using Linn.Stores.IoC;

    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Autofac;
    using Nancy.Configuration;
    using Nancy.Conventions;

    public class CustomBootstrapper : AutofacNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);

            environment.Tracing(enabled: false, displayErrorTraces: true);
        }

        protected override void ApplicationStartup(ILifetimeScope lifetimeScope, IPipelines pipelines)
        {
            base.ApplicationStartup(lifetimeScope, pipelines);

            pipelines.OnError += (ctx, ex) =>
            {
                Log(ex, lifetimeScope.Resolve<ILog>());
                return null;
            };
        }

        protected override void RequestStartup(ILifetimeScope lifetimeScope, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(lifetimeScope, pipelines, context);
            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Accept,Origin,Content-Type,Access-Control-Allow-Origin,Access-Control-Allow-Headers,Access-Control-Allow-Methods");
            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            pipelines.AfterRequest += ctx => ctx.Response.Headers.Add("Access-Control-Expose-Headers", "Accept,Origin,Content-type");
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("stores/assets", "client/assets"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("stores/build", "client/build"));
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            existingContainer.Update(
                builder =>
                {
                    builder.RegisterModule<AmazonCredentialsModule>();
                    builder.RegisterModule<AmazonSqsModule>();
                    builder.RegisterModule<LoggingModule>();
                    builder.RegisterModule<ServiceModule>();
                    builder.RegisterModule<PersistenceModule>();
                });

            base.ConfigureApplicationContainer(existingContainer);
        }

        protected override void ConfigureRequestContainer(ILifetimeScope lifetimeScope, NancyContext context)
        {
            lifetimeScope.Update(
                builder =>
                {
                    builder.RegisterModule<AmazonCredentialsModule>();
                    builder.RegisterModule<AmazonSqsModule>();
                    builder.RegisterModule<LoggingModule>();
                    builder.RegisterModule<ServiceModule>();
                    builder.RegisterModule<PersistenceModule>();
                });

            base.ConfigureRequestContainer(lifetimeScope, context);
        }

        private static void Log(Exception ex, ILog log)
        {
            if (ex is AggregateException exception)
            {
                foreach (var inner in exception.InnerExceptions)
                {
                    Log(inner, log);
                }
            }
            else
            {
                log.Error(ex.Message, ex);
            }
        }
    }
}