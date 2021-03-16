namespace Linn.Stores.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
    using Linn.Stores.Facade;
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
            builder.RegisterType<PartCategoryResourceBuilder>()
                .As<IResourceBuilder<PartCategory>>();
            builder.RegisterType<PartCategoriesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<PartCategory>>>();
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
            builder.RegisterType<CountryResourceBuilder>().As<IResourceBuilder<Country>>();
            builder.RegisterType<CountriesResourceBuilder>().As<IResourceBuilder<IEnumerable<Country>>>();
            builder.RegisterType<PartTemplateResourceBuilder>().As<IResourceBuilder<PartTemplate>>();
            builder.RegisterType<PartTemplatesResourceBuilder>().As<IResourceBuilder<IEnumerable<PartTemplate>>>();
            builder.RegisterType<PartLiveTestResourceBuilder>().As<IResourceBuilder<PartLiveTest>>();
            builder.RegisterType<SosAllocHeadResourceBuilder>().As<IResourceBuilder<SosAllocHead>>();
            builder.RegisterType<SosAllocHeadsResourceBuilder>().As<IResourceBuilder<IEnumerable<SosAllocHead>>>();
            builder.RegisterType<SosAllocDetailResourceBuilder>().As<IResourceBuilder<SosAllocDetail>>();
            builder.RegisterType<SosAllocDetailsResourceBuilder>().As<IResourceBuilder<IEnumerable<SosAllocDetail>>>();
            builder.RegisterType<MechPartSourceResourceBuilder>().As<IResourceBuilder<MechPartSource>>();
            builder.RegisterType<PartDataSheetResourceBuilder>().As<IResourceBuilder<PartDataSheet>>();
            builder.RegisterType<CarrierResourceBuilder>()
                .As<IResourceBuilder<Carrier>>();
            builder.RegisterType<CarriersResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Carrier>>>();
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
            builder.RegisterType<ImportBookInvoiceDetailResourceBuilder>()
                .As<IResourceBuilder<ImpBookInvoiceDetail>>();
            builder.RegisterType<ImportBookOrderDetailResourceBuilder>().As<IResourceBuilder<ImpBookOrderDetail>>();
            builder.RegisterType<ImportBookPostEntryResourceBuilder>().As<IResourceBuilder<ImpBookPostEntry>>();
            builder.RegisterType<TqmsCategoryResourceBuilder>()
                .As<IResourceBuilder<TqmsCategory>>();
            builder.RegisterType<TqmsCategoriesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<TqmsCategory>>>();
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
            builder.RegisterType<AvailableStockResourceBuilder>().As<IResourceBuilder<IEnumerable<AvailableStock>>>();
        }
    }
}
