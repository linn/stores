namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Authorisation;
    using Linn.Common.Configuration;
    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Facade;
    using Linn.Common.Proxy;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.StockMove;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Workstation;
    using Linn.Stores.Facade;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Requisitions;
    using Linn.Stores.Resources.StockLocators;
    using Linn.Stores.Resources.Tqms;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();
            builder.RegisterType<ReportingHelper>().As<IReportingHelper>();
            builder.RegisterType<AllocationService>().As<IAllocationService>();
            builder.RegisterType<AllocationReportsService>().As<IAllocationReportsService>();
            builder.RegisterType<PartService>().As<IPartService>();
            builder.RegisterType<WhatWillDecrementReportService>().As<IWhatWillDecrementReportService>();
            builder.RegisterType<StoragePlaceAuditReportService>().As<IStoragePlaceAuditReportService>();
            builder.RegisterType<MechPartSourceService>().As<IMechPartSourceService>();
            builder.RegisterType<WorkstationService>().As<IWorkstationService>();
            builder.RegisterType<StockLocatorService>().As<IStockLocatorService>();
            builder.RegisterType<WarehouseService>().As<IWarehouseService>();
            builder.RegisterType<WandService>().As<IWandService>();
            builder.RegisterType<RequisitionService>().As<IRequisitionService>();
            builder.RegisterType<TpkService>().As<ITpkService>();
            builder.RegisterType<MoveStockService>().As<IMoveStockService>();
            builder.RegisterType<TqmsReportsService>().As<ITqmsReportsService>();

            // facade services
            builder.RegisterType<PartFacadeService>()
                .As<IPartsFacadeService>();
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
            builder.RegisterType<NominalAccountsService>().As<INominalAccountsService>();
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
            builder.RegisterType<ParcelService>()
                .As<IParcelService>();
            builder.RegisterType<ManufacturerService>()
                .As<IFacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource>>();
            builder.RegisterType<EmployeesService>().As<IEmployeeService>();
            builder.RegisterType<ImportBookFacadeService>()
                .As<IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource>>();
            builder.RegisterType<PartDataSheetValuesService>().As<IPartDataSheetValuesService>();
            builder.RegisterType<TqmsCategoriesService>()
                .As<IFacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource>>();
            builder.RegisterType<WorkstationFacadeService>().As<IWorkstationFacadeService>();
            builder.RegisterType<StockLocatorsFacadeService>()
                .As<IStockLocatorFacadeService>();
            builder.RegisterType<StorageLocationService>()
                .As<IFacadeService<StorageLocation, int, StorageLocationResource, StorageLocationResource>>();
            builder.RegisterType<InspectedStateService>()
                .As<IFacadeService<InspectedState, string, InspectedStateResource, InspectedStateResource>>();
            builder.RegisterType<WarehouseFacadeService>().As<IWarehouseFacadeService>();
            builder.RegisterType<WandFacadeService>().As<IWandFacadeService>();
            builder.RegisterType<SalesOutletService>().As<ISalesOutletService>();
            builder.RegisterType<SalesAccountService>().As<ISalesAccountService>();
            builder.RegisterType<ExportReturnService>().As<IExportReturnService>();
            builder.RegisterType<ExportRsnService>().As<IExportRsnService>();
            builder.RegisterType<StockQuantitiesService>().As<IStockQuantitiesService>();
            builder.RegisterType<RequisitionActionsFacadeService>().As<IRequisitionActionsFacadeService>();
            builder.RegisterType<TpkFacadeService>().As<ITpkFacadeService>();
            builder.RegisterType<ExportReturnDetailFacadeService>()
                .As<IFacadeService<ExportReturnDetail, ExportReturnDetailKey, ExportReturnDetailResource, ExportReturnDetailResource>>();
            builder.RegisterType<AvailableStockFacadeService>().As<IAvailableStockFacadeService>();
            builder.RegisterType<MoveStockFacadeService>().As<IMoveStockFacadeService>();
            builder.RegisterType<RequisitionFacadeService>()
                .As<IFacadeService<RequisitionHeader, int, RequisitionResource, RequisitionResource>>();
            builder.RegisterType<StockLocatorPricesService>().As<IStockLocatorPricesService>();
            builder.RegisterType<PartStorageTypeFacadeService>()
                .As<IFacadeService<PartStorageType, int, PartStorageTypeResource, PartStorageTypeResource>>();
            builder.RegisterType<InterCompanyInvoiceService>().As<IInterCompanyInvoiceService>();
            builder.RegisterType<TqmsReportsFacadeService>().As<ITqmsReportsFacadeService>();
            builder.RegisterType<TqmsMasterFacadeService>().As<ISingleRecordFacadeService<TqmsMaster, TqmsMasterResource>>();
            builder.RegisterType<TqmsJobrefsFacadeService>().As<IFacadeService<TqmsJobRef, string, TqmsJobRefResource, TqmsJobRefResource>>();

            // oracle proxies
            builder.RegisterType<SosPack>().As<ISosPack>();
            builder.RegisterType<PartPack>().As<IPartPack>();
            builder.RegisterType<DatabaseService>().As<IDatabaseService>();
            builder.RegisterType<WwdPack>().As<IWwdPack>();
            builder.RegisterType<StoragePlaceAuditPack>().As<IStoragePlaceAuditPack>();
            builder.RegisterType<AllocPack>().As<IAllocPack>();
            builder.RegisterType<WorkstationPackProxy>().As<IWorkstationPack>();
            builder.RegisterType<WcsPack>().As<IWcsPack>();
            builder.RegisterType<WandPack>().As<IWandPack>();
            builder.RegisterType<StoresPack>().As<IStoresPack>();
            builder.RegisterType<TpkPack>().As<ITpkPack>();
            builder.RegisterType<BundleLabelPack>().As<IBundleLabelPack>();
            builder.RegisterType<WhatToWandDataProxy>().As<IWhatToWandService>();
            builder.RegisterType<ExportReturnsPack>().As<IExportReturnsPack>();
            builder.RegisterType<StockLocatorLocationsViewService>().As<IStockLocatorLocationsViewService>();
            builder.RegisterType<KardexPack>().As<IKardexPack>();
            builder.RegisterType<BartenderLabelPack>().As<IBartenderLabelPack>();

            // rest client proxies
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<ProductionTriggerLevelsProxy>().As<IProductionTriggerLevelsService>().WithParameter(
                "rootUri",
                ConfigurationManager.Configuration["PROXY_ROOT"]);
        }
    }
}
