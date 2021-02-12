namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Sos;
    using Linn.Stores.Domain.LinnApps.StockLocators;
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
            builder.RegisterType<CarrierRepository>().As<IRepository<Carrier, string>>();
            builder.RegisterType<PartDataSheetValuesRepository>().As<IQueryRepository<PartDataSheetValues>>();
            builder.RegisterType<StockLocatorRepository>().As<IRepository<StockLocator, int>>();
            builder.RegisterType<StoragePlaceRepository>().As<IQueryRepository<StoragePlace>>();
            builder.RegisterType<StoresBugetRepository>().As<IQueryRepository<StoresBudget>>();
            builder.RegisterType<AuditLocationRepository>().As<IQueryRepository<AuditLocation>>();
            builder.RegisterType<ManufacturerRepository>().As<IRepository<Manufacturer, string>>();
            builder.RegisterType<TqmsCategoriesRepository>().As<IRepository<TqmsCategory, string>>();
            builder.RegisterType<PtlMasterRepository>().As<ISingleRecordRepository<PtlMaster>>();
            builder.RegisterType<TopUpListJobRefRepository>().As<IRepository<TopUpListJobRef, string>>();
            builder.RegisterType<StoresPalletRepository>().As<IStoresPalletRepository>();
            builder.RegisterType<DespatchPickingSummaryRepository>().As<IQueryRepository<DespatchPickingSummary>>();
            builder.RegisterType<DespatchPalletQueueDetailsRepository>().As<IQueryRepository<DespatchPalletQueueDetail>>();
            builder.RegisterType<StorageLocationRepository>().As<IRepository<StorageLocation, int>>();
            builder.RegisterType<InspectedStatesRepository>().As<IRepository<InspectedState, string>>();
            builder.RegisterType<WandConsignmentsRepository>().As<IQueryRepository<WandConsignment>>();
        }
    }
}
