namespace Linn.Stores.IoC
{
    using Amazon;
    using Amazon.SimpleEmail;

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
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
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
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Resources.ImportBooks;
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
            builder.RegisterType<ConsignmentShipfileService>().As<IConsignmentShipfileService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<PdfService>().As<IPdfService>().WithParameter(
                "htmlToPdfConverterServiceUrl",
                ConfigurationManager.Configuration["PDF_SERVICE_ROOT"]);
            builder.RegisterType<TemplateEngine>().As<ITemplateEngine>();
            builder.RegisterType<ImportBookService>().As<IImportBookService>();
            builder.RegisterType<PackingListService>().As<IPackingListService>();
            builder.RegisterType<GoodsInService>().As<IGoodsInService>();
            builder.RegisterType<ImportBookReportService>()
                .As<IImportBookReportService>();
            builder.RegisterType<ConsignmentService>().As<IConsignmentService>();
            builder.RegisterType<ZeroValuedInvoiceDetailsReportService>()
                .As<IZeroValuedInvoiceDetailsReportService>();
            builder.RegisterType<QcPartsReportService>().As<IQcPartsReportService>();
            builder.RegisterType<EuCreditInvoicesReportService>().As<IEuCreditInvoicesReportService>();
            builder.RegisterType<StockTriggerLevelsForAStoragePlaceReportService>()
                .As<IStockTriggerLevelsForAStoragePlaceReportService>();
            builder.RegisterType<StoresMoveLogReportService>().As<IStoresMoveLogReportService>();

            // facade services
            builder.RegisterType<PartFacadeService>()
                .As<IPartsFacadeService>();
            builder.RegisterType<AccountingCompanyService>().As<IAccountingCompanyService>();
            builder.RegisterType<RootProductsService>().As<IRootProductService>();
            builder.RegisterType<DepartmentService>().As<IDepartmentsService>();
            builder.RegisterType<AllocationFacadeService>().As<IAllocationFacadeService>();
            builder.RegisterType<SernosSequencesService>().As<ISernosSequencesService>();
            builder.RegisterType<UnitsOfMeasureService>().As<IUnitsOfMeasureService>();
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
            builder.RegisterType<ParcelFacadeService>()
                .As<IFacadeFilterService<Parcel, int, ParcelResource, ParcelResource, ParcelSearchRequestResource>>();
            builder.RegisterType<ManufacturerService>()
                .As<IFacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource>>();
            builder.RegisterType<EmployeesService>().As<IEmployeeService>();
            builder.RegisterType<ImportBookFacadeService>()
                .As<IImportBookFacadeService>();
            builder.RegisterType<ImportBookReportFacadeService>()
                .As<IImportBookReportFacadeService>();
            builder.RegisterType<ImportBookDeliveryTermFacadeService>().As<IFacadeService<ImportBookDeliveryTerm, string, ImportBookDeliveryTermResource, ImportBookDeliveryTermResource>>();
            builder.RegisterType<ImportBookExchangeRateService>().As<IImportBookExchangeRateService>();
            builder.RegisterType<ImportBookTransactionCodeFacadeService>().As<IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource>>();
            builder.RegisterType<ImportBookTransportCodeFacadeService>().As<IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource>>();
            builder.RegisterType<ImportBookCpcNumberFacadeService>().As<IFacadeService<ImportBookCpcNumber, int, ImportBookCpcNumberResource, ImportBookCpcNumberResource>>();
            builder.RegisterType<PartDataSheetValuesService>().As<IPartDataSheetValuesService>();
            builder.RegisterType<PartTqmsOverridesService>()
                .As<IFacadeService<PartTqmsOverride, string, PartTqmsOverrideResource, PartTqmsOverrideResource>>();
            builder.RegisterType<WorkstationFacadeService>().As<IWorkstationFacadeService>();
            builder.RegisterType<StockLocatorsFacadeService>()
                .As<IStockLocatorFacadeService>();
            builder.RegisterType<StorageLocationService>()
                .As<IFacadeFilterService<StorageLocation, int, StorageLocationResource, StorageLocationResource, StorageLocationResource>>();
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
            builder.RegisterType<TqmsCategoriesFacadeService>().As<IFacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource>>();
            builder.RegisterType<ConsignmentShipfileFacadeService>().As<IConsignmentShipfileFacadeService>();
            builder.RegisterType<ConsignmentFacadeService>().As<IConsignmentFacadeService>();
            builder.RegisterType<CurrencyFacadeService>()
                .As<IFacadeService<Currency, string, CurrencyResource, CurrencyResource>>();
            builder.RegisterType<HubFacadeService>().As<IFacadeService<Hub, int, HubResource, HubResource>>();
            builder.RegisterType<CarrierFacadeService>().As<IFacadeService<Carrier, string, CarrierResource, CarrierResource>>();
            builder.RegisterType<ShippingTermFacadeService>().As<IFacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource>>();
            builder.RegisterType<GoodsInFacadeService>().As<IGoodsInFacadeService>();
            builder.RegisterType<SalesArticleService>().As<ISalesArticleService>();
            builder.RegisterType<CartonTypeFacadeService>().As<IFacadeService<CartonType, string, CartonTypeResource, CartonTypeResource>>();
            builder.RegisterType<PortFacadeService>().As<IFacadeService<Port, string, PortResource, PortResource>>();
            builder.RegisterType<LogisticsProcessesFacadeService>().As<ILogisticsProcessesFacadeService>();
            builder.RegisterType<LogisticsReportsFacadeService>().As<ILogisticsReportsFacadeService>();
            builder.RegisterType<RsnService>().As<IRsnService>();
            builder.RegisterType<PurchaseOrderFacadeService>().As<IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>>();
            builder.RegisterType<LoanService>().As<ILoanService>();
            builder.RegisterType<RsnConditionsService>().As<IRsnConditionsService>();
            builder.RegisterType<RsnAccessoriesService>().As<IRsnAccessoriesService>();
            builder.RegisterType<ZeroValuedInvoiceDetailsReportFacadeService>()
                .As<IZeroValuedInvoiceDetailsReportFacadeService>();
            builder.RegisterType<QcPartsReportFacadeService>().As<IQcPartsReportFacadeService>();
            builder.RegisterType<EuCreditInvoicesReportFacadeService>().As<IEuCreditInvoicesReportFacadeService>();
            builder.RegisterType<StockTriggerLevelsForAStoragePlaceFacadeService>()
                .As<IStockTriggerLevelsForAStoragePlaceFacadeService>();
            builder.RegisterType<StoresMoveLogReportFacadeService>().As<IStoresMoveLogReportFacadeService>();
            builder.RegisterType<StockTriggerLevelsFacadeService>().As<IStockTriggerLevelsFacadeService>();
            builder.RegisterType<PartLibraryService>()
                .As<IFacadeService<PartLibrary, string, PartLibraryResource, PartLibraryResource>>();

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
            builder.RegisterType<ConsignmentShipfileDataProxy>()
                .As<IConsignmentShipfileDataService>();
            builder.RegisterType<BartenderLabelPack>().As<IBartenderLabelPack>();
            builder.RegisterType<GoodsInPack>().As<IGoodsInPack>();
            builder.RegisterType<PalletAnalysisPack>().As<IPalletAnalysisPack>();
            builder.RegisterType<PurchaseOrderPack>().As<IPurchaseOrderPack>();
            builder.RegisterType<ConsignmentProxyService>().As<IConsignmentProxyService>();
            builder.RegisterType<InvoicingPack>().As<IInvoicingPack>();
            builder.RegisterType<ExportBookPack>().As<IExportBookPack>();
            builder.RegisterType<LogisticsLabelService>().As<ILogisticsLabelService>();
            builder.RegisterType<DeptStockPartsService>().As<IDeptStockPartsService>();
            builder.RegisterType<PurchaseLedgerPack>().As<IPurchaseLedgerPack>();

            // rest client proxies
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<ProductionTriggerLevelsProxy>().As<IProductionTriggerLevelsService>().WithParameter(
                "rootUri",
                ConfigurationManager.Configuration["PROXY_ROOT"]);
            builder.RegisterType<ProductsService>().As<IProductsService>().WithParameter(
                "rootUri",
                ConfigurationManager.Configuration["PROXY_ROOT"]);

            // ses
            builder.Register<AmazonSimpleEmailServiceClient>(x =>
                new AmazonSimpleEmailServiceClient(RegionEndpoint.EUWest1)).As<IAmazonSimpleEmailService>();
        }
    }
}
