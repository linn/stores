namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Sos;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Domain.LinnApps.Workstation;
    using Linn.Stores.Persistence.LinnApps;
    using Linn.Stores.Persistence.LinnApps.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceDbContext>().AsSelf().As<DbContext>().InstancePerRequest();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();

            builder.RegisterType<PartRepository>().As<IRepository<Part, int>>();
            builder.RegisterType<ParetoClassRepository>().As<IRepository<ParetoClass, string>>();
            builder.RegisterType<DepartmentRepository>().As<IQueryRepository<Department>>();
            builder.RegisterType<ProductAnalysisCodeRepository>().As<IQueryRepository<ProductAnalysisCode>>();
            builder.RegisterType<AccountingCompanyRepository>().As<IQueryRepository<AccountingCompany>>();
            builder.RegisterType<EmployeeRepository>().As<IRepository<Employee, int>>();
            builder.RegisterType<RootProductRepository>().As<IQueryRepository<RootProduct>>();
            builder.RegisterType<SosOptionRepository>().As<IRepository<SosOption, int>>();
            builder.RegisterType<SernosSequenceRepository>().As<IQueryRepository<SernosSequence>>();
            builder.RegisterType<UnitsOfMeasureRepository>().As<IQueryRepository<UnitOfMeasure>>();
            builder.RegisterType<PartCategoryRepository>().As<IQueryRepository<PartCategory>>();
            builder.RegisterType<SupplierRepository>().As<IQueryRepository<Supplier>>();
            builder.RegisterType<NominalRepository>().As<IQueryRepository<Nominal>>();
            builder.RegisterType<NominalAccountRepository>().As<IQueryRepository<NominalAccount>>();
            builder.RegisterType<DespatchLocationRepository>().As<IRepository<DespatchLocation, int>>();
            builder.RegisterType<StockPoolRepository>().As<IRepository<StockPool, int>>();
            builder.RegisterType<AssemblyTechnologyRepository>().As<IRepository<AssemblyTechnology, string>>();
            builder.RegisterType<DecrementRuleRepository>().As<IRepository<DecrementRule, string>>();
            builder.RegisterType<ChangeRequestRepository>().As<IQueryRepository<ChangeRequest>>();
            builder.RegisterType<WwdWorkRepository>().As<IQueryRepository<WwdWork>>();
            builder.RegisterType<WwdWorkDetailsRepository>().As<IQueryRepository<WwdWorkDetail>>();
            builder.RegisterType<CountryRepository>().As<IRepository<Country, string>>();
            builder.RegisterType<QcControlRepository>().As<IRepository<QcControl, int>>();
            builder.RegisterType<PartTemplateRepository>().As<IRepository<PartTemplate, string>>();
            builder.RegisterType<SosAllocHeadRepository>().As<IQueryRepository<SosAllocHead>>();
            builder.RegisterType<SosAllocDetailRepository>().As<IRepository<SosAllocDetail, int>>();
            builder.RegisterType<PartDataSheetRepository>().As<IRepository<PartDataSheet, PartDataSheetKey>>();
            builder.RegisterType<MechPartSourceRepository>().As<IRepository<MechPartSource, int>>();
            builder.RegisterType<ParcelRepository>().As<IRepository<Parcel, int>>();
            builder.RegisterType<PartDataSheetValuesRepository>().As<IQueryRepository<PartDataSheetValues>>();
            builder.RegisterType<StockLocatorRepository>().As<IRepository<StockLocator, int>>();
            builder.RegisterType<StoragePlaceRepository>().As<IQueryRepository<StoragePlace>>();
            builder.RegisterType<StoresBudgetRepository>().As<IQueryRepository<StoresBudget>>();
            builder.RegisterType<AuditLocationRepository>().As<IQueryRepository<AuditLocation>>();
            builder.RegisterType<ManufacturerRepository>().As<IRepository<Manufacturer, string>>();
            builder.RegisterType<ImportBookCpcNumberRepository>().As<IRepository<ImportBookCpcNumber, int>>();
            builder.RegisterType<ImportBookDeliveryTermsRepository>().As<IRepository<ImportBookDeliveryTerm, string>>();
            builder.RegisterType<ImportBookExhangeRateRepository>()
                .As<IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey>>();
            builder.RegisterType<ImportBookInvoiceDetailsRepository>()
                .As<IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey>>();
            builder.RegisterType<ImportBookOrderDetailsRepository>()
                .As<IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey>>();
            builder.RegisterType<ImportBookPostEntryRepository>()
                .As<IRepository<ImportBookPostEntry, ImportBookPostEntryKey>>();
            builder.RegisterType<ImportBookRepository>().As<IRepository<ImportBook, int>>();
            builder.RegisterType<LedgerPeriodRepository>().As<IRepository<LedgerPeriod, int>>();
            builder.RegisterType<ImportBookTransactionCodeRepository>()
                .As<IRepository<ImportBookTransactionCode, int>>();
            builder.RegisterType<ImportBookTransportCodeRepository>().As<IRepository<ImportBookTransportCode, int>>();
            builder.RegisterType<PortRepository>().As<IRepository<Port, string>>();
            builder.RegisterType<TqmsCategoriesRepository>().As<IRepository<TqmsCategory, string>>();
            builder.RegisterType<PtlMasterRepository>().As<ISingleRecordRepository<PtlMaster>>();
            builder.RegisterType<TopUpListJobRefRepository>().As<IRepository<TopUpListJobRef, string>>();
            builder.RegisterType<StoresPalletRepository>().As<IStoresPalletRepository>();
            builder.RegisterType<DespatchPickingSummaryRepository>().As<IQueryRepository<DespatchPickingSummary>>();
            builder.RegisterType<DespatchPalletQueueDetailsRepository>().As<IQueryRepository<DespatchPalletQueueDetail>>();
            builder.RegisterType<StorageLocationRepository>().As<IRepository<StorageLocation, int>>();
            builder.RegisterType<InspectedStatesRepository>().As<IRepository<InspectedState, string>>();
            builder.RegisterType<StockLocatorLocationsRepository>()
                .As<IQueryRepository<StockLocatorLocation>>();
            builder.RegisterType<StockLocatorBatchesRepository>()
                .As<IQueryRepository<StockLocatorBatch>>();
            builder.RegisterType<WandConsignmentsRepository>().As<IQueryRepository<WandConsignment>>();
            builder.RegisterType<WandItemsRepository>().As<IQueryRepository<WandItem>>();
            builder.RegisterType<ExportRsnRepository>().As<IQueryRepository<ExportRsn>>();
            builder.RegisterType<SalesAccountRepository>().As<IQueryRepository<SalesAccount>>();
            builder.RegisterType<SalesOutletRepository>().As<IQueryRepository<SalesOutlet>>();
            builder.RegisterType<StockQuantitiesRepository>().As<IQueryRepository<StockQuantities>>();
            builder.RegisterType<RequisitionHeaderRepository>().As<IRepository<RequisitionHeader, int>>();
            builder.RegisterType<TransferableStockRepository>().As<IQueryRepository<TransferableStock>>();
            builder.RegisterType<ConsignmentRepository>().As<IRepository<Consignment, int>>();
            builder.RegisterType<WandLogRepository>().As<IRepository<WandLog, int>>();
            builder.RegisterType<StockAvailableRepository>().As<IQueryRepository<AvailableStock>>();
            builder.RegisterType<StockLocatorPricesRepository>().As<IQueryRepository<StockLocatorPrices>>();
            builder.RegisterType<SalesOrderRepository>().As<IQueryRepository<SalesOrder>>();
            builder.RegisterType<SalesOrderDetailsRepository>().As<IQueryRepository<SalesOrderDetail>>();
            builder.RegisterType<ExportReturnRepository>().As<IRepository<ExportReturn, int>>();
            builder.RegisterType<ExportReturnDetailRepository>()
                .As<IRepository<ExportReturnDetail, ExportReturnDetailKey>>();
            builder.RegisterType<WandLogRepository>().As<IRepository<WandLog, int>>();
            builder.RegisterType<StockAvailableRepository>().As<IQueryRepository<AvailableStock>>();
            builder.RegisterType<StockLocatorPricesRepository>().As<IQueryRepository<StockLocatorPrices>>();
            builder.RegisterType<PartStorageTypeRepository>().As<IRepository<PartStorageType, int>>();
            builder.RegisterType<InterCompanyInvoiceRepository>().As<IQueryRepository<InterCompanyInvoice>>();
            builder.RegisterType<ReqMovesRepository>().As<IRepository<ReqMove, ReqMoveKey>>();
            builder.RegisterType<TqmsSummaryByCategoryRepository>().As<IQueryRepository<TqmsSummaryByCategory>>();
            builder.RegisterType<TqmsOutstandingLoansByCategoryRepository>().As<IQueryRepository<TqmsOutstandingLoansByCategory>>();
            builder.RegisterType<TqmsMasterRepository>().As<ISingleRecordRepository<TqmsMaster>>();
            builder.RegisterType<TqmsJobRefRepository>().As<IRepository<TqmsJobRef, string>>();
            builder.RegisterType<ConsignmentShipfileRepository>().As<IRepository<ConsignmentShipfile, int>>();
            builder.RegisterType<PrinterMappingRepository>().As<IRepository<PrinterMapping, int>>();
            builder.RegisterType<CurrencyRepository>().As<IRepository<Currency, string>>();
            builder.RegisterType<HubRepository>().As<IRepository<Hub, int>>();
            builder.RegisterType<CarrierRepository>().As<IRepository<Carrier, string>>();
            builder.RegisterType<ShippingTermRepository>().As<IRepository<ShippingTerm, int>>();
            builder.RegisterType<LoanDetailsRepository>().As<IQueryRepository<LoanDetail>>();
            builder.RegisterType<SalesArticleRepository>().As<IQueryRepository<SalesArticle>>();
            builder.RegisterType<CartonTypeRepository>().As<IRepository<CartonType, string>>();
            builder.RegisterType<GoodsInLogRepository>().As<IRepository<GoodsInLogEntry, int>>();
        }
    }
}
