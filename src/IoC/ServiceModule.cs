namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Stores.Facade.Services;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AllocationFacadeService>().As<IAllocationFacadeService>();
        }
    }
}