namespace Linn.Stores.IoC
{
    using Autofac;
    using Linn.Common.Facade;
    using Linn.Production.Facade.Services;
    using Linn.Stores.Domain.LinnApps;
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
            builder.RegisterType<AccountingCompanyService>().As<IAccountingCompanyService>();
            builder.RegisterType<RootProductsService>().As<IRootProductService>();
            builder.RegisterType<DepartmentService>().As<IDepartmentsService>();
            builder.RegisterType<AllocationFacadeService>().As<IAllocationFacadeService>();
            builder.RegisterType<SernosSequencesService>().As<ISernosSequencesService>();
            builder.RegisterType<UnitsOfMeasureService>().As<IUnitsOfMeasureService>();
            builder.RegisterType<PartCategoryService>().As<IPartCategoryService>();
            builder.RegisterType<SuppliersService>().As<IFacadeWithSearchReturnTen<Supplier, int, SupplierResource, SupplierResource>>();
            builder.RegisterType<CarriersService>().As<IFacadeWithSearchReturnTen<Carrier, int, CarrierResource, CarrierResource>>();

            // proxy
            builder.RegisterType<SosPack>().As<ISosPack>();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
        }
    }
}