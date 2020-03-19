namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // facade services
            builder.RegisterType<PartFacadeService>()
                .As<IFacadeService<Part, int, PartResource, PartResource>>();
            builder.RegisterType<AllocationFacadeService>().As<IAllocationFacadeService>();
        }
    }
}