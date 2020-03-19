namespace Linn.Stores.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<AllocationStartResourceBuilder>().As<IResourceBuilder<AllocationStart>>();
            builder.RegisterType<PartResourceBuilder>().As<IResourceBuilder<Part>>();
            builder.RegisterType<PartsResourceBuilder>().As<IResourceBuilder<IEnumerable<Part>>>();
        }
    }
}