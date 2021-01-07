namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Authorisation;
    using Linn.Common.Configuration;
    using Linn.Common.Facade;
    using Linn.Common.Proxy;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Domain.LinnApps.Workstation;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();
            builder.RegisterType<ReportingHelper>().As<IReportingHelper>();
            builder.RegisterType<AllocationService>().As<IAllocationService>();
            builder.RegisterType<PartService>().As<IPartService>();
            builder.RegisterType<WhatWillDecrementReportService>().As<IWhatWillDecrementReportService>();
            builder.RegisterType<StoragePlaceAuditReportService>().As<IStoragePlaceAuditReportService>();
            builder.RegisterType<MechPartSourceService>().As<IMechPartSourceService>();
            builder.RegisterType<WorkstationService>().As<IWorkstationService>();
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();

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
            builder.RegisterType<SuppliersService>().As<ISuppliersService>();
            builder.RegisterType<ProductAnalysisCodeService>()
                .As<IProductAnalysisCodeService>();
            builder.RegisterType<NominalService>().As<INominalService>();
            builder.RegisterType<DespatchLocationFacadeService>()
                .As<IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource>>();
            builder.RegisterType<StockPoolFacadeService>()
                .As<IFacadeService<StockPool, int, StockPoolResource, StockPoolResource>>();
            builder.RegisterType<AssemblyTechnologyService>()
                .As<IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource>>();
            builder.RegisterType<DecrementRuleService>()
                .As<IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource>>();
            builder.RegisterType<WhatWillDecrementReportFacadeService>().As<IWhatWillDecrementReportFacadeService>();
            builder.RegisterType<CountryFacadeService>()
                .As<IFacadeService<Country, string, CountryResource, CountryResource>>();
            builder.RegisterType<PartTemplateService>()
                .As<IFacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource>>();
            builder.RegisterType<PartLiveService>().As<IPartLiveService>();
            builder.RegisterType<StoragePlaceAuditReportFacadeService>().As<IStoragePlaceAuditReportFacadeService>();
            builder.RegisterType<AuditLocationService>().As<IAuditLocationService>();
            builder.RegisterType<StoragePlaceService>().As<IStoragePlaceService>();
            builder.RegisterType<SosAllocHeadFacadeService>().As<ISosAllocHeadFacadeService>();
            builder.RegisterType<SosAllocDetailFacadeService>()
                .As<IFacadeFilterService<SosAllocDetail, int, SosAllocDetailResource, SosAllocDetailResource, JobIdRequestResource>>();
            builder.RegisterType<MechPartSourceFacadeService>()
                .As<IFacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>>();
            builder.RegisterType<CarriersService>().As<ICarriersService>();
            builder.RegisterType<ParcelFacadeService>()
                .As<IFacadeService<Parcel, int, ParcelResource, ParcelResource>>();
            builder.RegisterType<ManufacturerService>()
                .As<IFacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource>>();
            builder.RegisterType<EmployeesService>().As<IEmployeeService>();
            builder.RegisterType<PartDataSheetValuesService>().As<IPartDataSheetValuesService>();
            builder.RegisterType<TqmsCategoriesService>()
                .As<IFacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource>>();
            builder.RegisterType<WorkstationFacadeService>().As<IWorkstationFacadeService>();

            // oracle proxies
            builder.RegisterType<SosPack>().As<ISosPack>();
            builder.RegisterType<PartPack>().As<IPartPack>();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
            builder.RegisterType<WwdPack>().As<IWwdPack>();
            builder.RegisterType<StoragePlaceAuditPack>().As<IStoragePlaceAuditPack>();
            builder.RegisterType<AllocPack>().As<IAllocPack>();

            // rest client proxies
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<ProductionTriggerLevelsProxy>().As<IProductionTriggerLevelsService>().WithParameter(
                "rootUri",
                ConfigurationManager.Configuration["PROXY_ROOT"]);
        }
    }
}
