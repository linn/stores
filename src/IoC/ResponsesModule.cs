namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<AllocationStartResourceBuilder>().As<IResourceBuilder<AllocationStart>>();
        }
    }
}