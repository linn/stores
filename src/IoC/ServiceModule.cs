﻿namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<AllocationService>().As<IAllocationService>();

            // facade services
            builder.RegisterType<PartFacadeService>()
                .As<IFacadeService<Part, int, PartResource, PartResource>>();
            builder.RegisterType<AllocationFacadeService>().As<IAllocationFacadeService>();

            // proxy
            builder.RegisterType<SosPack>().As<ISosPack>();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
        }
    }
}