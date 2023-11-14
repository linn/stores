namespace Linn.Stores.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Domain.LinnApps.Wcs;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
    using Linn.Stores.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<AllocationResultResourceBuilder>().As<IResourceBuilder<AllocationResult>>();
            builder.RegisterType<PartResourceBuilder>().As<IResourceBuilder<Part>>();
            builder.RegisterType<PartsResourceBuilder>().As<IResourceBuilder<IEnumerable<Part>>>();
            builder.RegisterType<AccountingCompanyResourceBuilder>()
                .As<IResourceBuilder<AccountingCompany>>();
            builder.RegisterType<AccountingCompaniesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<AccountingCompany>>>();
            builder.RegisterType<RootProductResourceBuilder>()
                .As<IResourceBuilder<RootProduct>>();
            builder.RegisterType<RootProductsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<RootProduct>>>();
            builder.RegisterType<DepartmentResourceBuilder>()
                .As<IResourceBuilder<Department>>();
            builder.RegisterType<DepartmentsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Department>>>();
            builder.RegisterType<SernosSequenceResourceBuilder>()
                .As<IResourceBuilder<SernosSequence>>();
            builder.RegisterType<SernosSequencesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<SernosSequence>>>();
            builder.RegisterType<UnitOfMeasureResourceBuilder>()
                .As<IResourceBuilder<UnitOfMeasure>>();
            builder.RegisterType<UnitsOfMeasureResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<UnitOfMeasure>>>();
            builder.RegisterType<SupplierResourceBuilder>()
                .As<IResourceBuilder<Supplier>>();
            builder.RegisterType<SuppliersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Supplier>>>();
            builder.RegisterType<ProductAnalysisCodeResourceBuilder>()
                .As<IResourceBuilder<ProductAnalysisCode>>();
            builder.RegisterType<ProductAnalysisCodesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ProductAnalysisCode>>>();
            builder.RegisterType<NominalResourceBuilder>().As<IResourceBuilder<Nominal>>();
            builder.RegisterType<DespatchLocationResourceBuilder>().As<IResourceBuilder<DespatchLocation>>();
            builder.RegisterType<DespatchLocationsResourceBuilder>().As<IResourceBuilder<IEnumerable<DespatchLocation>>>();
            builder.RegisterType<StockPoolResourceBuilder>().As<IResourceBuilder<StockPool>>();
            builder.RegisterType<StockPoolsResourceBuilder>().As<IResourceBuilder<IEnumerable<StockPool>>>();
            builder.RegisterType<AssemblyTechnologyResourceBuilder>()
                .As<IResourceBuilder<AssemblyTechnology>>();
            builder.RegisterType<AssemblyTechnologiesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<AssemblyTechnology>>>();
            builder.RegisterType<DecrementRuleResourceBuilder>()
                .As<IResourceBuilder<DecrementRule>>();
            builder.RegisterType<DecrementRulesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<DecrementRule>>>();
            builder.RegisterType<ResultsModelResourceBuilder>().As<IResourceBuilder<ResultsModel>>();
            builder.RegisterType<ResultsModelsResourceBuilder>().As<IResourceBuilder<IEnumerable<ResultsModel>>>();
            builder.RegisterType<CountryResourceBuilder>().As<IResourceBuilder<Country>>();
            builder.RegisterType<CountriesResourceBuilder>().As<IResourceBuilder<IEnumerable<Country>>>();
            builder.RegisterType<PartTemplateResourceBuilder>().As<IResourceBuilder<ResponseModel<PartTemplate>>>();
            builder.RegisterType<PartTemplatesResourceBuilder>().As<IResourceBuilder<ResponseModel< IEnumerable<PartTemplate>>>>();
            builder.RegisterType<PartLiveTestResourceBuilder>().As<IResourceBuilder<PartLiveTest>>();
            builder.RegisterType<SosAllocHeadResourceBuilder>().As<IResourceBuilder<SosAllocHead>>();
            builder.RegisterType<SosAllocHeadsResourceBuilder>().As<IResourceBuilder<IEnumerable<SosAllocHead>>>();
            builder.RegisterType<SosAllocDetailResourceBuilder>().As<IResourceBuilder<SosAllocDetail>>();
            builder.RegisterType<SosAllocDetailsResourceBuilder>().As<IResourceBuilder<IEnumerable<SosAllocDetail>>>();
            builder.RegisterType<MechPartSourceResourceBuilder>().As<IResourceBuilder<MechPartSource>>();
            builder.RegisterType<PartDataSheetResourceBuilder>().As<IResourceBuilder<PartDataSheet>>();
            builder.RegisterType<ParcelResourceBuilder>()
                .As<IResourceBuilder<Parcel>>();
            builder.RegisterType<ParcelsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Parcel>>>();
            builder.RegisterType<AuditLocationResourceBuilder>().As<IResourceBuilder<AuditLocation>>();
            builder.RegisterType<AuditLocationsResourceBuilder>().As<IResourceBuilder<IEnumerable<AuditLocation>>>();
            builder.RegisterType<StoragePlaceResourceBuilder>().As<IResourceBuilder<StoragePlace>>();
            builder.RegisterType<StoragePlacesResourceBuilder>().As<IResourceBuilder<IEnumerable<StoragePlace>>>();
            builder.RegisterType<ManufacturerResourceBuilder>().As<IResourceBuilder<Manufacturer>>();
            builder.RegisterType<ManufacturersResourceBuilder>().As<IResourceBuilder<IEnumerable<Manufacturer>>>();
            builder.RegisterType<MechPartManufacturerAltResourceBuilder>()
                .As<IResourceBuilder<MechPartManufacturerAlt>>();
            builder.RegisterType<MechPartAltResourceBuilder>()
                .As<IResourceBuilder<MechPartAlt>>();
            builder.RegisterType<EmployeeResourceBuilder>()
                .As<IResourceBuilder<Employee>>();
            builder.RegisterType<EmployeesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Employee>>>();
            builder.RegisterType<PartDataSheetValuesResourceBuilder>().As<IResourceBuilder<PartDataSheetValues>>();
            builder.RegisterType<PartDataSheetValuesListResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PartDataSheetValues>>>();
            builder.RegisterType<ErrorResourceBuilder>().As<IResourceBuilder<Error>>();
            builder.RegisterType<ImportBookResourceBuilder>().As<IResourceBuilder<ImportBook>>();
            builder.RegisterType<ImportBooksResourceBuilder>().As<IResourceBuilder<IEnumerable<ImportBook>>>();
            builder.RegisterType<ImportBookDeliveryTermResourceBuilder>().As<IResourceBuilder<ImportBookDeliveryTerm>>();
            builder.RegisterType<ImportBookDeliveryTermsResourceBuilder>().As<IResourceBuilder<IEnumerable<ImportBookDeliveryTerm>>>();
            builder.RegisterType<ImportBookExchangeRateResourceBuilder>().As<IResourceBuilder<ImportBookExchangeRate>>();
            builder.RegisterType<ImportBookExchangeRatesResourceBuilder>().As<IResourceBuilder<IEnumerable<ImportBookExchangeRate>>>();
            builder.RegisterType<ImportBookInvoiceDetailResourceBuilder>()
                .As<IResourceBuilder<ImportBookInvoiceDetail>>();
            builder.RegisterType<ImportBookTransportCodeResourceBuilder>()
                .As<IResourceBuilder<ImportBookTransportCode>>();
            builder.RegisterType<ImportBookTransportCodesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ImportBookTransportCode>>>();
            builder.RegisterType<ImportBookTransactionCodeResourceBuilder>()
                .As<IResourceBuilder<ImportBookTransactionCode>>();
            builder.RegisterType<ImportBookTransactionCodesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ImportBookTransactionCode>>>();
            builder.RegisterType<ImportBookOrderDetailResourceBuilder>().As<IResourceBuilder<ImportBookOrderDetail>>();
            builder.RegisterType<ImportBookPostEntryResourceBuilder>().As<IResourceBuilder<ImportBookPostEntry>>();
            builder.RegisterType<ImportBookCpcNumberResourceBuilder>().As<IResourceBuilder<ImportBookCpcNumber>>();
            builder.RegisterType<ImportBookCpcNumbersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ImportBookCpcNumber>>>();
            builder.RegisterType<PartTqmsOverrideResourceBuilder>()
                .As<IResourceBuilder<PartTqmsOverride>>();
            builder.RegisterType<PartTqmsOverridesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PartTqmsOverride>>>();
            builder.RegisterType<WorkstationTopUpStatusResourceBuilder>()
                .As<IResourceBuilder<ResponseModel<WorkstationTopUpStatus>>>();
            builder.RegisterType<StockLocatorResourceBuilder>().As<IResourceBuilder<StockLocator>>();
            builder.RegisterType<StockLocatorsResourceBuilder>().As<IResourceBuilder<IEnumerable<StockLocator>>>();
            builder.RegisterType<StockLocatorWithStoragePlaceInfoResourceBuilder>().As<IResourceBuilder<StockLocatorWithStoragePlaceInfo>>();
            builder.RegisterType<StockLocatorsWithStoragePlaceInfoResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<StockLocatorWithStoragePlaceInfo>>>();
            builder.RegisterType<DespatchPalletQueueResultResourceBuilder>()
                .As<IResourceBuilder<DespatchPalletQueueResult>>();
            builder.RegisterType<StorageLocationResourceBuilder>().As<IResourceBuilder<StorageLocation>>();
            builder.RegisterType<StorageLocationsResourceBuilder>().As<IResourceBuilder<IEnumerable<StorageLocation>>>();
            builder.RegisterType<InspectedStateResourceBuilder>().As<IResourceBuilder<InspectedState>>();
            builder.RegisterType<InspectedStatesResourceBuilder>().As<IResourceBuilder<IEnumerable<InspectedState>>>();
            builder.RegisterType<MessageResourceBuilder>().As<IResourceBuilder<MessageResult>>();
            builder.RegisterType<NominalAccountResourceBuilder>().As<IResourceBuilder<NominalAccount>>();
            builder.RegisterType<NominalAccountsResourceBuilder>().As<IResourceBuilder<IEnumerable<NominalAccount>>>();
            builder.RegisterType<WandConsignmentsResourceBuilder>().As<IResourceBuilder<IEnumerable<WandConsignment>>>();
            builder.RegisterType<WandItemsResourceBuilder>().As<IResourceBuilder<IEnumerable<WandItem>>>();
            builder.RegisterType<SalesOutletResourceBuilder>().As<IResourceBuilder<SalesOutlet>>();
            builder.RegisterType<SalesOutletsResourceBuilder>().As<IResourceBuilder<IEnumerable<SalesOutlet>>>();
            builder.RegisterType<SalesAccountResourceBuilder>().As<IResourceBuilder<SalesAccount>>();
            builder.RegisterType<SalesAccountsResourceBuilder>().As<IResourceBuilder<IEnumerable<SalesAccount>>>();
            builder.RegisterType<ExportRsnResourceBuilder>().As<IResourceBuilder<ExportRsn>>();
            builder.RegisterType<ExportRsnsResourceBuilder>().As<IResourceBuilder<IEnumerable<ExportRsn>>>();
            builder.RegisterType<StockQuantitiesResourceBuilder>().As<IResourceBuilder<StockQuantities>>();
            builder.RegisterType<StockQuantitiesListResourceBuilder>().As<IResourceBuilder<IEnumerable<StockQuantities>>>();
            builder.RegisterType<WandItemResultResourceBuilder>().As<IResourceBuilder<WandResult>>();
            builder.RegisterType<RequisitionActionResourceBuilder>().As<IResourceBuilder<RequisitionActionResult>>();
            builder.RegisterType<TransferableStockResourceBuilder>().As<IResourceBuilder<TransferableStock>>();
            builder.RegisterType<TransferableStockListResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<TransferableStock>>>();
            builder.RegisterType<TpkResultResourceBuilder>().As<IResourceBuilder<TpkResult>>();
            builder.RegisterType<ExportReturnResourceBuilder>().As<IResourceBuilder<ExportReturn>>();
            builder.RegisterType<ExportReturnDetailResourceBuilder>().As<IResourceBuilder<ExportReturnDetail>>();
            builder.RegisterType<ExportReturnDetailsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ExportReturnDetail>>>();
            builder.RegisterType<StockLocatorPricesResourceBuilder>().As<IResourceBuilder<StockLocatorPrices>>();
            builder.RegisterType<StockLocatorPricesListResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<StockLocatorPrices>>>();
            builder.RegisterType<AvailableStockResourceBuilder>().As<IResourceBuilder<IEnumerable<AvailableStock>>>();
            builder.RegisterType<RequisitionProcessResultResourceBuilder>().As<IResourceBuilder<RequisitionProcessResult>>();
            builder.RegisterType<RequisitionResourceBuilder>().As<IResourceBuilder<RequisitionHeader>>();
            builder.RegisterType<RequisitionMovesResourceBuilder>().As<IRequisitionMovesResourceBuilder>();
            builder.RegisterType<PartStorageTypesResourceBuilder>().As<IResourceBuilder<IEnumerable<PartStorageType>>>();
            builder.RegisterType<InterCompanyInvoiceResourceBuilder>()
                .As<IResourceBuilder<InterCompanyInvoice>>();
            builder.RegisterType<InterCompanyInvoicesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<InterCompanyInvoice>>>();
            builder.RegisterType<TqmsMasterResourceBuilder>().As<IResourceBuilder<TqmsMaster>>();
            builder.RegisterType<TqmsJobRefsResourceBuilder>().As<IResourceBuilder<IEnumerable<TqmsJobRef>>>();
            builder.RegisterType<TqmsCategoriesResourceBuilder>().As<IResourceBuilder<IEnumerable<TqmsCategory>>>();
            builder.RegisterType<ConsignmentShipfileResourceBuilder>().As<IResourceBuilder<ConsignmentShipfile>>();
            builder.RegisterType<ConsignmentShipfilesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<ConsignmentShipfile>>>();
            builder.RegisterType<ConsignmentResourceBuilder>().As<IResourceBuilder<Consignment>>();
            builder.RegisterType<ConsignmentsResourceBuilder>().As<IResourceBuilder<IEnumerable<Consignment>>>();
            builder.RegisterType<CurrencyResourceBuilder>().As<IResourceBuilder<Currency>>();
            builder.RegisterType<CurrenciesResourceBuilder>().As<IResourceBuilder<IEnumerable<Currency>>>();
            builder.RegisterType<HubResourceBuilder>().As<IResourceBuilder<Hub>>();
            builder.RegisterType<HubsResourceBuilder>().As<IResourceBuilder<IEnumerable<Hub>>>();
            builder.RegisterType<CarrierResourceBuilder>().As<IResourceBuilder<Carrier>>();
            builder.RegisterType<CarriersResourceBuilder>().As<IResourceBuilder<IEnumerable<Carrier>>>();
            builder.RegisterType<ShippingTermResourceBuilder>().As<IResourceBuilder<ShippingTerm>>();
            builder.RegisterType<ShippingTermsResourceBuilder>().As<IResourceBuilder<IEnumerable<ShippingTerm>>>();
            builder.RegisterType<LoanDetailResourceBuilder>().As<IResourceBuilder<LoanDetail>>();
            builder.RegisterType<LoanDetailsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<LoanDetail>>>();
            builder.RegisterType<SalesArticleResourceBuilder>().As<IResourceBuilder<SalesArticle>>();
            builder.RegisterType<SalesArticlesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<SalesArticle>>>();
            builder.RegisterType<ProcessResultResourceBuilder>().As<IResourceBuilder<ProcessResult>>();
            builder.RegisterType<ValidatePurchaseOrderResultResourceBuilder>()
                .As<IResourceBuilder<ValidatePurchaseOrderResult>>();
            builder.RegisterType<CartonTypeResourceBuilder>().As<IResourceBuilder<CartonType>>();
            builder.RegisterType<CartonTypesResourceBuilder>().As<IResourceBuilder<IEnumerable<CartonType>>>();
            builder.RegisterType<PortResourceBuilder>().As<IResourceBuilder<Port>>();
            builder.RegisterType<PortsResourceBuilder>().As<IResourceBuilder<IEnumerable<Port>>>();
            builder.RegisterType<BookInResultResourceBuilder>().As<IResourceBuilder<BookInResult>>();
            builder.RegisterType<StockMoveResourceBuilder>().As<IResourceBuilder<StockMove>>();
            builder.RegisterType<StockMovesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<StockMove>>>();
            builder.RegisterType<ValidateStorageTypeResultResourceBuilder>()
                .As<IResourceBuilder<ValidateStorageTypeResult>>();
            builder.RegisterType<PackingListResourceBuilder>().As<IResourceBuilder<PackingList>>();
            builder.RegisterType<RsnResourceBuilder>().As<IResourceBuilder<Rsn>>();
            builder.RegisterType<RsnsResourceBuilder>().As<IResourceBuilder<IEnumerable<Rsn>>>();
            builder.RegisterType<PurchaseOrderResourceBuilder>().As<IResourceBuilder<PurchaseOrder>>();
            builder.RegisterType<PurchaseOrdersResourceBuilder>().As<IResourceBuilder<IEnumerable<PurchaseOrder>>>();
            builder.RegisterType<LoanResourceBuilder>().As<IResourceBuilder<Loan>>();
            builder.RegisterType<LoansResourceBuilder>().As<IResourceBuilder<IEnumerable<Loan>>>();
            builder.RegisterType<RsnConditionResourceBuilder>().As<IResourceBuilder<RsnCondition>>();
            builder.RegisterType<RsnConditionsResourceBuilder>().As<IResourceBuilder<IEnumerable<RsnCondition>>>();
            builder.RegisterType<RsnAccessoryResourceBuilder>().As<IResourceBuilder<RsnAccessory>>();
            builder.RegisterType<RsnAccessoriesResourceBuilder>().As<IResourceBuilder<IEnumerable<RsnAccessory>>>();
            builder.RegisterType<ValidateRsnResultResourceBuilder>().As<IResourceBuilder<ValidateRsnResult>>();
            builder.RegisterType<WhatToWandConsignmentResourceBuilder>().As<IResourceBuilder<WhatToWandConsignment>>();
            builder.RegisterType<WarehouseLocationResourceBuilder>().As<IResourceBuilder<WarehouseLocation>>();
            builder.RegisterType<StockTriggerLevelResourceBuilder>().As<IResourceBuilder<StockTriggerLevel>>();
            builder.RegisterType<StockTriggerLevelsResourceBuilder>().As<IResourceBuilder<IEnumerable<StockTriggerLevel>>>();
            builder.RegisterType<PartLibraryResourceBuilder>().As<IResourceBuilder<PartLibrary>>();
            builder.RegisterType<PartLibrariesResourceBuilder>().As<IResourceBuilder<IEnumerable<PartLibrary>>>();
            builder.RegisterType<ScsPalletsResourceBuilder>().As<IResourceBuilder<IEnumerable<ScsPallet>>>();
        }
    }
}
