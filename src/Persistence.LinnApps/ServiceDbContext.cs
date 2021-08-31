namespace Linn.Stores.Persistence.LinnApps
{
    using Linn.Common.Configuration;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Domain.LinnApps.ExportBooks;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Sos;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Domain.LinnApps.Workstation;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ServiceDbContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory =
            new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });

        public DbSet<Part> Parts { get; set; }

        public DbSet<ParetoClass> ParetoClasses { get; set; }

        public DbSet<ProductAnalysisCode> ProductAnalysisCodes { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<RootProduct> RootProducts { get; set; }

        public DbSet<AccountingCompany> AccountingCompanies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<SosOption> SosOptions { get; set; }

        public DbSet<SernosSequence> SernosSequences { get; set; }

        public DbQuery<UnitOfMeasure> UnitsOfMeasure { get; set; }

        public DbQuery<PartCategory> PartCategories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Nominal> Nominals { get; set; }

        public DbSet<NominalAccount> NominalAccounts { get; set; }

        public DbSet<DespatchLocation> DespatchLocations { get; set; }

        public DbSet<StockPool> StockPools { get; set; }

        public DbSet<DecrementRule> DecrementRules { get; set; }

        public DbSet<AssemblyTechnology> AssemblyTechnologies { get; set; }

        public DbQuery<ChangeRequest> ChangeRequests { get; set; }

        public DbQuery<WwdWork> WwdWorks { get; set; }

        public DbQuery<WwdWorkDetail> WwdWorkDetails { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<QcControl> QcControl { get; set; }

        public DbSet<PartTemplate> PartTemplates { get; set; }

        public DbSet<PartDataSheet> PartDataSheets { get; set; }

        public DbSet<MechPartSource> MechPartSources { get; set; }

        public DbSet<StockLocator> StockLocators { get; set; }

        public DbQuery<StoragePlace> StoragePlaces { get; set; }

        public DbQuery<StoresBudget> StoresBudgets { get; set; }

        public DbQuery<AuditLocation> AuditLocations { get; set; }

        public DbSet<SosAllocHead> SosAllocHeads { get; set; }

        public DbSet<Parcel> Parcels { get; set; }

        public DbSet<SosAllocDetail> SosAllocDetails { get; set; }

        public DbSet<MechPartAlt> MechPartAlts { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<MechPartManufacturerAlt> MechPartManufacturerAlts { get; set; }

        public DbSet<ImportBook> ImportBooks { get; set; }

        public DbSet<ImportBookInvoiceDetail> ImportBookInvoiceDetails { get; set; }

        public DbSet<ImportBookOrderDetail> ImportBookOrderDetails { get; set; }

        public DbSet<ImportBookDeliveryTerm> ImportBookDeliveryTerms { get; set; }

        public DbSet<ImportBookPostEntry> ImportBookPostEntries { get; set; }

        public DbSet<ImportBookCpcNumber> ImportBookCpcNumbers { get; set; }

        public DbSet<ImportBookExchangeRate> ImportBookExchangeRates { get; set; }

        public DbSet<ImportBookTransactionCode> ImportBookTransactionCodes { get; set; }

        public DbSet<ImportBookTransportCode> ImportBookTransportCodes { get; set; }

        public DbSet<LedgerPeriod> LedgerPeriods { get; set; }

        public DbSet<Port> Ports { get; set; }

        public DbSet<PartParamData> PartParamDataSheets { get; set; }

        public DbSet<MechPartPurchasingQuote> MechPartPurchasingQuotes { get; set; }

        public DbQuery<PartDataSheetValues> PartDataSheetValues { get; set; }

        public DbSet<TqmsCategory> TqmsCategories { get; set; }

        public DbSet<PtlMaster> PtlMaster { get; set; }

        public DbSet<TopUpListJobRef> TopUpListJobRefs { get; set; }

        public DbSet<MechPartUsage> MechPartUsages { get; set; }

        public DbSet<StoresPallet> StoresPallets { get; set; }

        public DbQuery<DespatchPickingSummary> DespatchPickingSummary { get; set; }

        public DbQuery<DespatchPalletQueueDetail> DespatchPalletQueueDetails { get; set; }

        public DbSet<StorageLocation> StorageLocations { get; set; }

        public DbSet<InspectedState> InspectedStates { get; set; }

        public DbQuery<StockLocatorLocation> StockLocatorLocationsView { get; set; }

        public DbQuery<StockLocatorBatch> StockLocatorBatchesView { get; set; }

        public DbQuery<WandConsignment> WandConsignments { get; set; }

        public DbQuery<WandItem> WandItems { get; set; }

        public DbQuery<ExportRsn> Rsns { get; set; }

        public DbSet<SalesAccount> SalesAccounts { get; set; }

        public DbSet<SalesOutlet> SalesOutlets { get; set; }

        public DbQuery<StockQuantities> StockQuantitiesForMrView { get; set; }

        public DbSet<RequisitionHeader> RequisitionHeaders { get; set; }

        public DbQuery<TransferableStock> TransferableStock { get; set; }

        public DbSet<Consignment> Consignments { get; set; }

        public DbSet<ExportReturn> ExportReturns { get; set; }

        public DbSet<ExportReturnDetail> ExportReturnDetails { get; set; }

        public DbSet<WandLog> WandLogs { get; set; }

        public DbQuery<AvailableStock> StockAvailable { get; set; }

        public DbQuery<StockLocatorPrices> StockLocatorView { get; set; }

        public DbSet<PartStorageType> PartStorageTypes { get; set; }

        public DbSet<SalesArticle> SalesArticles { get; set; }

        public DbSet<SalesOrder> SalesOrders { get; set; }

        public DbQuery<SalesOrderDetail> SalesOrderDetails { get; set; }

        public DbSet<InterCompanyInvoice> IntercompanyInvoices { get; set; }

        public DbSet<ReqMove> ReqMoves { get; set; }

        public DbQuery<TqmsSummaryByCategory> TqmsSummaryByCategories { get; set; }

        public DbSet<TqmsMaster> TqmsMaster { get; set; }

        public DbSet<TqmsJobRef> TqmsJobRefs { get; set; }

        public DbQuery<TqmsOutstandingLoansByCategory> TqmsOutstandingLoansByCategories { get; set; }

        public DbSet<ConsignmentShipfile> ConsignmentShipfiles { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Person> Persons { get; set; }
        
        public DbSet<Address> Addresses { get; set; }

        public DbSet<PrinterMapping> PrinterMappings { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Hub> Hubs { get; set; }

        public DbSet<Carrier> Carriers { get; set; }

        public DbSet<ShippingTerm> ShippingTerms { get; set; }

        public DbQuery<LoanDetail> LoanDetails { get; set; }

        public DbSet<CartonType> CartonTypes { get; set; }
        
        public DbSet<ExportBook> ExportBooks { get; set; }

        public DbSet<GoodsInLogEntry> GoodsInLog { get; set; }

        public DbSet<StoresTransactionDefinition> StoresTransactionDefinitions { get; set; }
        
        public DbSet<PlCreditDebitNote> PlCreditDebitNotes { get; set; }

        public DbQuery<StoresLabelType> StoresLabelTypes { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbQuery<AuthUser> AuthUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildParts(builder);
            this.BuildParetoClasses(builder);
            this.BuildDepartments(builder);
            this.BuildProductAnalysisCodes(builder);
            this.BuildAccountingCompanies(builder);
            this.BuildEmployees(builder);
            this.BuildRootProducts(builder);
            this.BuildSosOptions(builder);
            this.BuildSernosSequences(builder);
            this.QueryUnitsOfMeasure(builder);
            this.QueryPartCategories(builder);
            this.BuildSuppliers(builder);
            this.BuildNominals(builder);
            this.BuildNominalAccounts(builder);
            this.BuildDespatchLocations(builder);
            this.BuildStockPools(builder);
            this.BuildDecrementRules(builder);
            this.BuildAssemblyTechnologies(builder);
            this.QueryChangeRequests(builder);
            this.QueryWwdWorks(builder);
            this.QueryWwdWorkDetails(builder);
            this.BuildCountries(builder);
            this.BuildQcControl(builder);
            this.BuildPartTemplates(builder);
            this.BuildPartDataSheets(builder);
            this.BuildMechPartSources(builder);
            this.BuildStockLocators(builder);
            this.QueryStoragePlaces(builder);
            this.QueryStoresBudgets(builder);
            this.QueryAuditLocations(builder);
            this.BuildSosAllocHeads(builder);
            this.BuildParcels(builder);
            this.BuildMechPartAlts(builder);
            this.BuildManufacturers(builder);
            this.BuildMechPartManufacturerAlts(builder);
            this.BuildImportBooks(builder);
            this.BuildImportBookInvoiceDetails(builder);
            this.BuildImportBookOrderDetails(builder);
            this.BuildImportBookDeliveryTerms(builder);
            this.BuildImportBookPostEntries(builder);
            this.BuildImportBookCpcNumbers(builder);
            this.BuildImportBookExchangeRates(builder);
            this.BuildImportBookTransactionCodes(builder);
            this.BuildImportBookTransportCodes(builder);
            this.BuildLedgerPeriods(builder);
            this.BuildPorts(builder);
            builder.Model.Relational().MaxIdentifierLength = 30;
            this.BuildPartParamDataSheets(builder);
            this.QueryPartDataSheetValues(builder);
            this.BuildSosAllocDetails(builder);
            this.BuildTqmsCategories(builder);
            this.BuildSalesOutlets(builder);
            this.BuildMechPartPurchasingQuotes(builder);
            this.BuildMechPartUsages(builder);
            this.BuildPtlMaster(builder);
            this.BuildTopUpJobRefs(builder);
            this.BuildStoresPallets(builder);
            this.QueryDespatchPickingSummary(builder);
            this.QueryDespatchPalletQueueDetails(builder);
            this.BuildStorageLocations(builder);
            this.BuildInspectedStates(builder);
            this.QueryStockLocatorLocationsView(builder);
            this.QueryStockLocatorBatches(builder);
            this.QueryWandConsignments(builder);
            this.QueryWandItems(builder);
            this.QueryExportRsns(builder);
            this.QuerySalesAccounts(builder);
            this.QueryStockQuantitIesForMrView(builder);
            this.BuildRequisitionHeaders(builder);
            this.QueryTpkView(builder);
            this.BuildConsignmentPallets(builder);
            this.BuildConsignments(builder);
            this.BuildExportReturns(builder);
            this.BuildExportReturnDetails(builder);
            this.BuildRequisitionLines(builder);
            this.BuildReqMoves(builder);
            this.BuildWandLogs(builder);
            this.QueryStockLocatorView(builder);
            this.QueryStockAvailable(builder);
            this.BuildPartStorageTypes(builder);
            this.BuildSalesArticles(builder);
            this.QuerySalesOrders(builder);
            this.QuerySalesOrderDetails(builder);
            this.BuildIntercompanyInvoices(builder);
            this.QueryTqmsSummaryByCategories(builder);
            this.BuildTqmsMaster(builder);
            this.BuildTqmsJobRefs(builder);
            this.QueryTqmsOutstandingLoansByCategories(builder);
            this.QueryConsignmentShipFiles(builder);
            this.QueryInvoices(builder);
            this.BuildConsignmentItems(builder);
            this.BuildContacts(builder);
            this.BuildPersons(builder);
            this.BuildAddresses(builder);
            this.BuildPrinterMappings(builder);
            this.BuildCurrencies(builder);
            this.BuildHubs(builder);
            this.BuildCarriers(builder);
            this.BuildShippingTerms(builder);
            this.QueryLoanDetails(builder);
            this.BuildCartonTypes(builder);
            this.BuildGoodsInLog(builder);
            this.BuildStoresTransactionDefinitions(builder);
            this.BuildExportBooks(builder);
            this.BuildPlCreditDebitNotes(builder);
            this.QueryStoresLabelTypes(builder);
            this.BuildPurchaseOrders(builder);
            this.QueryAuthUsers(builder);
            base.OnModelCreating(builder);
            this.BuildPurchaseOrderDetails(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = ConfigurationManager.Configuration["DATABASE_HOST"];
            var userId = ConfigurationManager.Configuration["DATABASE_USER_ID"];
            var password = ConfigurationManager.Configuration["DATABASE_PASSWORD"];
            var serviceId = ConfigurationManager.Configuration["DATABASE_NAME"];

            var dataSource =
                $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={serviceId})(SERVER=dedicated)))";

            optionsBuilder.UseOracle($"Data Source={dataSource};User Id={userId};Password={password};");
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }

        private void BuildAccountingCompanies(ModelBuilder builder)
        {
            var q = builder.Entity<AccountingCompany>().ToTable("ACCOUNTING_COMPANIES");
            q.HasKey(a => a.Name);
            q.Property(c => c.Name).HasColumnName("ACCOUNTING_COMPANY");
            q.Property(c => c.DateInvalid).HasColumnName("DATE_INVALID");
            q.Property(c => c.Description).HasColumnName("DESCRIPTION");
            q.Property(c => c.LatesSalesAllocationDate).HasColumnName("LATEST_SALES_ALLOCATION_DATE");
        }

        private void BuildCountries(ModelBuilder builder)
        {
            builder.Entity<Country>().ToTable("COUNTRIES");
            builder.Entity<Country>().HasKey(c => c.CountryCode);
            builder.Entity<Country>().Property(c => c.CountryCode).HasColumnName("COUNTRY_CODE").HasMaxLength(2);
            builder.Entity<Country>().Property(c => c.Name).HasColumnName("NAME").HasMaxLength(50);
            builder.Entity<Country>().Property(c => c.DisplayName).HasColumnName("DISPLAY_NAME").HasMaxLength(50);
            builder.Entity<Country>().Property(c => c.TradeCurrency).HasColumnName("TRADE_CURRENCY").HasMaxLength(4);
            builder.Entity<Country>().Property(c => c.ECMember).HasColumnName("EEC_MEMBER").HasMaxLength(1);
            builder.Entity<Country>().Property(c => c.NumberOfCopiesOfDispatchDocuments).HasColumnName("COPIES_OF_DISP_DOCS");
            builder.Entity<Country>().Property(c => c.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildEmployees(ModelBuilder builder)
        {
            var q = builder.Entity<Employee>();
            q.HasKey(e => e.Id);
            q.ToTable("AUTH_USER_NAME_VIEW");
            q.Property(e => e.Id).HasColumnName("USER_NUMBER");
            q.Property(e => e.FullName).HasColumnName("USER_NAME");
            q.Property(e => e.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildSuppliers(ModelBuilder builder)
        {
            var q = builder.Entity<Supplier>();
            q.HasKey(e => e.Id);
            q.ToTable("SUPPLIERS");
            q.Property(e => e.Id).HasColumnName("SUPPLIER_ID");
            q.Property(e => e.Name).HasColumnName("SUPPLIER_NAME").HasMaxLength(50);
            q.Property(e => e.CountryCode).HasColumnName("COUNTRY");
            q.Property(e => e.DateClosed).HasColumnName("DATE_CLOSED");
            q.Property(s => s.ApprovedCarrier).HasColumnName("APPROVED_CARRIER");
        }

        private void BuildParts(ModelBuilder builder)
        {
            var e = builder.Entity<Part>().ToTable("PARTS");
            e.HasKey(p => p.PartNumber);
            e.Property(p => p.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            e.Property(p => p.Id).HasColumnName("BRIDGE_ID");
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(200);
            e.Property(p => p.RootProduct).HasColumnName("ROOT_PRODUCT").HasMaxLength(10);
            e.Property(p => p.StockControlled).HasColumnName("STOCK_CONTROLLED").HasMaxLength(1);
            e.Property(p => p.SafetyCriticalPart).HasColumnName("SAFETY_CRITICAL_PART").HasMaxLength(1);
            e.Property(p => p.EmcCriticalPart).HasColumnName("EMC_CRITICAL_PART").HasMaxLength(1);
            e.Property(p => p.CccCriticalPart).HasColumnName("CCC_CRITICAL_PART").HasMaxLength(1);
            e.Property(p => p.PerformanceCriticalPart).HasColumnName("PERFORMANCE_CRITICAL_PART").HasMaxLength(1);
            e.Property(p => p.PsuPart).HasColumnName("PSU_PART").HasMaxLength(1);
            e.Property(p => p.SingleSourcePart).HasColumnName("SINGLE_SOURCE_PART").HasMaxLength(1);
            e.Property(p => p.SafetyCertificateExpirationDate).HasColumnName("SAFETY_CERTIFICATE_EXPIRY_DATE");
            e.Property(p => p.SafetyDataDirectory).HasColumnName("SAFETY_DATA_DIRECTORY").HasMaxLength(500);
            e.Property(p => p.LinnProduced).HasColumnName("LINN_PRODUCED").HasMaxLength(1);
            e.Property(p => p.BomType).HasColumnName("BOM_TYPE").HasMaxLength(1);
            e.Property(p => p.OptionSet).HasColumnName("OPTION_SET").HasMaxLength(14);
            e.Property(p => p.DrawingReference).HasColumnName("DRAWING_REFERENCE").HasMaxLength(100);
            e.Property(p => p.BomId).HasColumnName("BOM_ID");
            e.Property(p => p.OurUnitOfMeasure).HasColumnName("OUR_UNIT_OF_MEASURE").HasMaxLength(14);
            e.Property(p => p.Currency).HasColumnName("CURRENCY").HasMaxLength(4);
            e.Property(p => p.CurrencyUnitPrice).HasColumnName("CURRENCY_UNIT_PRICE");
            e.Property(p => p.BaseUnitPrice).HasColumnName("BASE_UNIT_PRICE");
            e.Property(p => p.MaterialPrice).HasColumnName("MATERIAL_PRICE");
            e.Property(p => p.LabourPrice).HasColumnName("LABOUR_PRICE");
            e.Property(p => p.CostingPrice).HasColumnName("COSTING_PRICE");
            e.Property(p => p.OrderHold).HasColumnName("ORDER_HOLD").HasMaxLength(1);
            e.Property(p => p.PartCategory).HasColumnName("PART_CATEGORY").HasMaxLength(2);
            e.Property(p => p.NonForecastRequirement).HasColumnName("NON_FC_REQT");
            e.Property(p => p.OneOffRequirement).HasColumnName("ONE_OFF_REQT");
            e.Property(p => p.SparesRequirement).HasColumnName("SPARES_REQT");
            e.Property(p => p.PlannedSurplus).HasColumnName("PLANNED_SURPLUS").HasMaxLength(1);
            e.Property(p => p.IgnoreWorkstationStock).HasColumnName("IGNORE_WORKSTN_STOCK").HasMaxLength(1);
            e.Property(p => p.ImdsIdNumber).HasColumnName("IMDS_ID_NUMBER");
            e.Property(p => p.ImdsWeight).HasColumnName("IMDS_WEIGHT_G");
            e.Property(p => p.MechanicalOrElectronic).HasColumnName("MECHANICAL_OR_ELECTRONIC").HasMaxLength(2);
            e.Property(p => p.QcOnReceipt).HasColumnName("QC_ON_RECEIPT").HasMaxLength(1);
            e.Property(p => p.QcInformation).HasColumnName("QC_INFORMATION").HasMaxLength(90);
            e.Property(p => p.RawOrFinished).HasColumnName("RM_FG").HasMaxLength(1);
            e.Property(p => p.OurInspectionWeeks).HasColumnName("OUR_INSP_WEEKS");
            e.Property(p => p.SafetyWeeks).HasColumnName("SAFETY_WEEKS");
            e.Property(p => p.RailMethod).HasColumnName("RAIL_METHOD").HasMaxLength(10);
            e.Property(p => p.MinStockRail).HasColumnName("MIN_RAIL");
            e.Property(p => p.MaxStockRail).HasColumnName("MAX_RAIL");
            e.Property(p => p.SecondStageBoard).HasColumnName("SECOND_STAGE_BOARD").HasMaxLength(1);
            e.Property(p => p.SecondStageDescription).HasColumnName("SS_DESCRIPTION").HasMaxLength(100);
            e.Property(p => p.TqmsCategoryOverride).HasColumnName("TQMS_CATEGORY_OVERRIDE").HasMaxLength(20);
            e.Property(p => p.StockNotes).HasColumnName("STOCK_NOTES").HasMaxLength(500);
            e.Property(p => p.DateCreated).HasColumnName("DATE_CREATED");
            e.Property(p => p.DateLive).HasColumnName("DATE_LIVE");
            e.Property(p => p.DatePhasedOut).HasColumnName("DATE_PURCH_PHASE_OUT");
            e.Property(p => p.ReasonPhasedOut).HasColumnName("REASON_PURCH_PHASED_OUT").HasMaxLength(250);
            e.Property(p => p.ScrapOrConvert).HasColumnName("SCRAP_OR_CONVERT").HasMaxLength(20);
            e.Property(p => p.PurchasingPhaseOutType).HasColumnName("PURCH_PHASE_OUT_TYPE").HasMaxLength(20);
            e.Property(p => p.DateDesignObsolete).HasColumnName("DATE_DESIGN_OBSOLETE");
            e.HasOne<Employee>(p => p.PhasedOutBy).WithMany(m => m.PartsPhasedOut).HasForeignKey("PURCH_PHASED_OUT_BY");
            e.HasOne<Employee>(p => p.MadeLiveBy).WithMany(m => m.PartsMadeLive).HasForeignKey("LIVE_BY");
            e.HasOne<Employee>(p => p.CreatedBy).WithMany(m => m.PartsCreated).HasForeignKey("CREATED_BY");
            e.HasOne(p => p.AccountingCompany).WithMany(c => c.PartsResponsibleFor).HasForeignKey("ACCOUNTING_COMPANY");
            e.HasOne(p => p.PreferredSupplier).WithMany(s => s.PartsPreferredSupplierOf)
                .HasForeignKey(p => p.PreferredSupplierId);
            e.Property(p => p.PreferredSupplierId).HasColumnName("PREFERRED_SUPPLIER");
            e.HasOne<ParetoClass>(p => p.ParetoClass).WithMany(c => c.Parts).HasForeignKey("PARETO_CODE");
            e.HasOne<ProductAnalysisCode>(p => p.ProductAnalysisCode).WithMany(c => c.Parts)
                .HasForeignKey("PRODUCT_ANALYSIS_CODE");
            e.Property(p => p.NominalAccountId).HasColumnName("NOMACC_NOMACC_ID");
            e.HasOne(p => p.NominalAccount).WithMany(a => a.Parts).HasForeignKey(p => p.NominalAccountId);
            e.HasOne(p => p.SernosSequence).WithMany(s => s.Parts).HasForeignKey("SERNOS_SEQUENCE");
            e.HasOne(p => p.AssemblyTechnology).WithMany(s => s.Parts).HasForeignKey("ASSEMBLY_TECHNOLOGY");
            e.HasOne(p => p.DecrementRule).WithMany(s => s.Parts).HasForeignKey("DECREMENT_RULE");
            e.HasOne(p => p.MechPartSource).WithOne(m => m.Part);
            e.HasOne(p => p.SalesArticle).WithOne(a => a.Part).HasForeignKey<Part>(x => x.PartNumber);
        }

        private void BuildPartDataSheets(ModelBuilder builder)
        {
            var e = builder.Entity<PartDataSheet>().ToTable("PART_DATASHEETS");
            e.Property(d => d.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            e.Property(d => d.Sequence).HasColumnName("SEQ").HasMaxLength(4);
            e.Property(d => d.PdfFilePath).HasColumnName("PDF_FILE_PATH").HasMaxLength(1000);
            e.HasKey(d => new { d.Sequence, d.PartNumber });
            e.HasOne(d => d.Part).WithMany(p => p.DataSheets).HasForeignKey(d => d.PartNumber);
        }

        private void BuildMechPartSources(ModelBuilder builder)
        {
            var e = builder.Entity<MechPartSource>().ToTable("MECH_PART_SOURCES");
            e.HasKey(s => s.Id);
            e.Property(s => s.Id).HasColumnName("MS_ID").HasMaxLength(8);
            e.HasOne<Employee>(s => s.ProposedBy).WithMany(m => m.SourcesProposed).HasForeignKey("PROPOSED_BY");
            e.Property(s => s.DateEntered).HasColumnName("DATE_ENTERED");
            e.Property(s => s.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(s => s.PartDescription).HasColumnName("PART_DESCRIPTION");
            e.Property(s => s.MechanicalOrElectrical).HasColumnName("MECH_ELEC_PART").HasMaxLength(1);
            e.Property(s => s.PartType).HasColumnName("PART_TYPE").HasMaxLength(14);
            e.Property(s => s.EstimatedVolume).HasColumnName("ESTIMATED_VOLUME");
            e.Property(s => s.SamplesRequired).HasColumnName("SAMPLES_REQUIRED").HasMaxLength(1);
            e.Property(s => s.SampleQuantity).HasColumnName("SAMPLE_QTY");
            e.Property(s => s.DateSamplesRequired).HasColumnName("DATE_SAMPLES_REQUIRED");
            e.Property(s => s.RohsReplace).HasColumnName("ROHS_REPLACE").HasMaxLength(1);
            e.Property(s => s.LinnPartNumber).HasColumnName("LINN_PART_NUMBER").HasMaxLength(14);
            e.Property(s => s.Notes).HasColumnName("NOTES").HasMaxLength(2000);
            e.Property(s => s.AssemblyType).HasColumnName("ASSEMBLY_TYPE").HasMaxLength(4);
            e.Property(s => s.SingleSource).HasColumnName("SINGLE_SOURCE").HasMaxLength(1);
            e.Property(s => s.EmcCritical).HasColumnName("EMC_CRITICAL").HasMaxLength(1);
            e.Property(s => s.PerformanceCritical).HasColumnName("PERFORMANCE_CRITICAL").HasMaxLength(1);
            e.Property(s => s.SafetyCritical).HasColumnName("SAFETY_CRITICAL").HasMaxLength(1);
            e.HasOne<Part>(s => s.Part).WithOne(p => p.MechPartSource).HasForeignKey<MechPartSource>(s => s.PartNumber);
            e.HasOne(s => s.PartToBeReplaced).WithMany(p => p.ReplacementParts).HasForeignKey(s => s.LinnPartNumber);
            e.Property(s => s.SafetyDataDirectory).HasColumnName("SAFETY_DATA_DIRECTORY").HasMaxLength(500);
            e.Property(s => s.ProductionDate).HasColumnName("PRODUCTION_DATE");
            e.Property(s => s.CapacitorRippleCurrent).HasColumnName("CAP_RIPPLE_CURRENT");
            e.Property(s => s.DrawingsPackage).HasColumnName("DRAWINGS_PACKAGE").HasMaxLength(200);
            e.Property(s => s.DrawingsPackageAvailable).HasColumnName("DRAWINGS_PACKAGE_AVAILABLE").HasMaxLength(1);
            e.Property(s => s.DrawingsPackageDate).HasColumnName("DRAWINGS_PACKAGE_DATE");
            e.Property(s => s.DrawingFile).HasColumnName("DRAWING_FILE").HasMaxLength(350);
            e.Property(s => s.ChecklistCreated).HasColumnName("CHECKLIST_CREATED").HasMaxLength(200);
            e.Property(s => s.ChecklistAvailable).HasColumnName("CHECKLIST_AVAILABLE").HasMaxLength(1);
            e.Property(s => s.ChecklistDate).HasColumnName("CHECKLIST_DATE");
            e.Property(s => s.PackingAvailable).HasColumnName("PACKING_AVAILABLE").HasMaxLength(1);
            e.Property(s => s.PackingRequired).HasColumnName("PACKING_REQUIRED").HasMaxLength(1);
            e.Property(s => s.PackingDate).HasColumnName("PACKING_DATE");
            e.Property(s => s.ProductKnowledge).HasColumnName("PRODUCT_KNOWLEDGE").HasMaxLength(200);
            e.Property(s => s.ProductKnowledgeAvailable).HasColumnName("PRODUCT_KNOWLEDGE_AVAILABLE").HasMaxLength(1);
            e.Property(s => s.ProductKnowledgeDate).HasColumnName("PRODUCT_KNOWLEDGE_DATE");
            e.Property(s => s.TestEquipment).HasColumnName("TEST_EQUIPMENT").HasMaxLength(200);
            e.Property(s => s.TestEquipmentAvailable).HasColumnName("TEST_EQUIPMENT_AVAILABLE").HasMaxLength(1);
            e.Property(s => s.TestEquipmentDate).HasColumnName("TEST_EQUIPMENT_DATE");
            e.Property(s => s.ApprovedReferenceStandards).HasColumnName("APPROVED_REFERENCE_STANDARDS")
                .HasMaxLength(200);
            e.Property(s => s.ApprovedReferencesAvailable).HasColumnName("APPROVED_REFERENCES_AVAILABLE")
                .HasMaxLength(1);
            e.Property(s => s.ApprovedReferencesDate).HasColumnName("APPROVED_REFERENCES_DATE").HasMaxLength(200);
            e.Property(s => s.ProcessEvaluation).HasColumnName("PROCESS_EVALUATION").HasMaxLength(200);
            e.Property(s => s.ProcessEvaluationAvailable).HasColumnName("PROCESS_EVALUATION_AVAILABLE").HasMaxLength(1);
            e.Property(s => s.ProcessEvaluationDate).HasColumnName("PROCESS_EVALUATION_DATE");
            e.Property(s => s.Capacitance).HasColumnName("CAP_CAPACITANCE");
            e.Property(s => s.CapacitorVoltageRating).HasColumnName("CAP_VOLTAGE_RATING");
            e.Property(s => s.CapacitorPositiveTolerance).HasColumnName("CAP_POSITIVE_TOLERANCE");
            e.Property(s => s.CapacitorNegativeTolerance).HasColumnName("CAP_NEGATIVE_TOLERANCE");
            e.Property(s => s.CapacitorDielectric).HasColumnName("CAP_DIELECTRIC").HasMaxLength(40);
            e.Property(s => s.Package).HasColumnName("PACKAGE").HasMaxLength(14);
            e.Property(s => s.CapacitorPitch).HasColumnName("CAP_PITCH");
            e.Property(s => s.CapacitorLength).HasColumnName("CAP_LENGTH");
            e.Property(s => s.CapacitorWidth).HasColumnName("CAP_WIDTH");
            e.Property(s => s.CapacitorHeight).HasColumnName("CAP_HEIGHT");
            e.Property(s => s.CapacitorDiameter).HasColumnName("CAP_DIAMETER");
            e.Property(s => s.CapacitanceLetterAndNumeralCode).HasColumnName("CAPACITANCE_CHAR");
            e.Property(s => s.Resistance).HasColumnName("RES_RESISTANCE");
            e.Property(s => s.ResistorTolerance).HasColumnName("RES_TOLERANCE");
            e.Property(s => s.Construction).HasColumnName("CONSTRUCTION").HasMaxLength(14);
            e.Property(s => s.ResistorLength).HasColumnName("RES_LENGTH");
            e.Property(s => s.ResistorWidth).HasColumnName("RES_WIDTH");
            e.Property(s => s.ResistorHeight).HasColumnName("RES_HEIGHT");
            e.Property(s => s.ResistorPowerRating).HasColumnName("RES_POWER_RATING");
            e.Property(s => s.ResistorVoltageRating).HasColumnName("RES_VOLTAGE_RATING");
            e.Property(s => s.TemperatureCoefficient).HasColumnName("TEMP_COEFF");
            e.Property(s => s.TransistorType).HasColumnName("TRAN_TYPE").HasMaxLength(10);
            e.Property(s => s.TransistorDeviceName).HasColumnName("TRAN_DEVICE_NAME").HasMaxLength(50);
            e.Property(s => s.TransistorPolarity).HasColumnName("TRAN_POLARITY").HasMaxLength(30);
            e.Property(s => s.TransistorVoltage).HasColumnName("TRAN_VOLTAGE_RATING");
            e.Property(s => s.TransistorCurrent).HasColumnName("TRAN_AMPS");
            e.Property(s => s.IcType).HasColumnName("IC_TYPE").HasMaxLength(50);
            e.Property(s => s.IcFunction).HasColumnName("IC_FUNCTION").HasMaxLength(50);
            e.Property(s => s.FootprintRef).HasColumnName("FOOTPRINT_REF").HasMaxLength(30);
            e.Property(s => s.LibraryRef).HasColumnName("LIBRARY_REF").HasMaxLength(30);
            e.Property(s => s.RkmCode).HasColumnName("RESISTANCE_CHAR").HasMaxLength(18);
            e.HasMany(s => s.PurchasingQuotes).WithOne(q => q.Source).HasForeignKey(q => q.SourceId);
            e.HasOne(s => s.PartCreatedBy).WithMany(m => m.PartsCreatedSourceRecords)
                .HasForeignKey(s => s.PartCreatedById);
            e.Property(s => s.PartCreatedById).HasColumnName("PCIT_PART_CREATED_BY");
            e.Property(s => s.PartCreatedDate).HasColumnName("PCIT_PART_CREATED_DATE");

            e.HasOne(s => s.VerifiedBy).WithMany(m => m.SourcesVerified).HasForeignKey(s => s.VerifiedById);
            e.Property(s => s.VerifiedById).HasColumnName("PURCH_VERIFIED_BY");
            e.Property(s => s.VerifiedDate).HasColumnName("PURCH_VERIFIED_DATE");
            e.HasOne(s => s.QualityVerifiedBy).WithMany(m => m.SourcesQualityVerified)
                .HasForeignKey(s => s.QualityVerifiedById);
            e.Property(s => s.QualityVerifiedById).HasColumnName("QUALITY_VERIFIED_BY");
            e.Property(s => s.QualityVerifiedDate).HasColumnName("QUALITY_VERIFIED_DATE");
            e.HasOne(s => s.McitVerifiedBy).WithMany(m => m.SourcesVerifiedMcit).HasForeignKey(s => s.McitVerifiedById);
            e.Property(s => s.McitVerifiedById).HasColumnName("MCIT_VERIFIED_BY");
            e.Property(s => s.McitVerifiedDate).HasColumnName("MCIT_VERIFIED_DATE");
            e.HasOne(s => s.ApplyTCodeBy).WithMany(m => m.SourcesTCodeApplied).HasForeignKey(s => s.ApplyTCodeId);
            e.Property(s => s.ApplyTCodeId).HasColumnName("APPLY_T_CODE_BY");
            e.Property(s => s.ApplyTCodeDate).HasColumnName("APPLY_T_CODE_DATE");
            e.HasOne(s => s.RemoveTCodeBy).WithMany(m => m.SourcesTCodeRemoved).HasForeignKey(s => s.RemoveTCodeId);
            e.Property(s => s.RemoveTCodeId).HasColumnName("REMOVE_T_CODE_BY");
            e.Property(s => s.RemoveTCodeDate).HasColumnName("REMOVE_T_CODE_DATE");
            e.HasOne(s => s.CancelledBy).WithMany(m => m.SourcesCancelled).HasForeignKey(s => s.CancelledById);
            e.Property(s => s.CancelledById).HasColumnName("CANCELLED_BY");
            e.Property(s => s.DateCancelled).HasColumnName("DATE_CANCELLED");
            e.Property(s => s.LifeExpectancyPart).HasColumnName("LIFE_EXPECTANCY_PART").HasMaxLength(50);
            e.Property(s => s.Configuration).HasColumnName("CONFIGURATION").HasMaxLength(200);
        }

        private void BuildMechPartAlts(ModelBuilder builder)
        {
            var e = builder.Entity<MechPartAlt>().ToTable("MECH_PART_ALTS");
            e.HasKey(m => new { m.MechPartSourceId, m.Sequence });
            e.Property(m => m.MechPartSourceId).HasColumnName("MS_ID").HasMaxLength(8);
            e.Property(m => m.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(30);
            e.Property(m => m.Sequence).HasColumnName("SEQ");
            e.HasOne<Supplier>(s => s.Supplier).WithMany(s => s.MechPartAlts).HasForeignKey("SUPPLIER_ID");
            e.HasOne<MechPartSource>(s => s.MechPartSource).WithMany(s => s.MechPartAlts)
                .HasForeignKey(x => x.MechPartSourceId);
        }

        private void BuildManufacturers(ModelBuilder builder)
        {
            var e = builder.Entity<Manufacturer>().ToTable("MANUFACTURERS");
            e.HasKey(m => m.Code);
            e.Property(m => m.Code).HasColumnName("CODE").HasMaxLength(6);
            e.Property(m => m.Description).HasColumnName("DESCRIPTION").HasMaxLength(30);
        }

        private void BuildMechPartManufacturerAlts(ModelBuilder builder)
        {
            var e = builder.Entity<MechPartManufacturerAlt>().ToTable("MECH_PART_MF_ALTS");
            e.HasKey(m => new { m.MechPartSourceId, m.Sequence });
            e.Property(m => m.MechPartSourceId).HasColumnName("MS_ID").HasMaxLength(8);
            e.Property(m => m.PartNumber).HasColumnName("MANUF_PART_NUMBER").HasMaxLength(30);
            e.Property(m => m.Sequence).HasColumnName("SEQ");
            e.Property(m => m.ManufacturerCode).HasColumnName("MANUF_CODE").HasMaxLength(6);
            e.HasOne<Manufacturer>(s => s.Manufacturer).WithMany(s => s.MechPartManufacturerAlts)
                .HasForeignKey(x => x.ManufacturerCode);
            e.HasOne<MechPartSource>(s => s.MechPartSource).WithMany(s => s.MechPartManufacturerAlts)
                .HasForeignKey(x => x.MechPartSourceId);
            e.Property(m => m.ReelSuffix).HasColumnName("REEL_SUFFIX").HasMaxLength(2);
            e.Property(m => m.RohsCompliant).HasColumnName("ROHS_COMPL").HasMaxLength(1);
            e.HasOne<Employee>(s => s.ApprovedBy).WithMany(m => m.MechPartManufacturerAltsApproved)
                .HasForeignKey("APPROVED_BY");
            e.Property(s => s.DateApproved).HasColumnName("DATE_APPROVED");
            e.Property(s => s.Preference).HasColumnName("PREFERENCE");
        }

        private void BuildPartTemplates(ModelBuilder builder)
        {
            var e = builder.Entity<PartTemplate>().ToTable("PART_NUMBER_TEMPLATES");
            e.HasKey(p => p.PartRoot);
            e.Property(p => p.PartRoot).HasColumnName("PART_ROOT").HasMaxLength(14);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(200);
            e.Property(p => p.StockControlled).HasColumnName("STOCK_CONTROLLED").HasMaxLength(1);
            e.Property(p => p.LinnProduced).HasColumnName("LINN_PRODUCED").HasMaxLength(1);
            e.Property(p => p.BomType).HasColumnName("BOM_TYPE").HasMaxLength(1);
            e.Property(p => p.ParetoCode).HasColumnName("PARETO_CODE").HasMaxLength(2);
            e.Property(p => p.AssemblyTechnology).HasColumnName("ASSEMBLY_TECHNOLOGY");
            e.Property(p => p.HasDataSheet).HasColumnName("HAS_DATASHEET").HasMaxLength(1);
            e.Property(p => p.HasNumberSequence).HasColumnName("NUMBER_SEQUENCE").HasMaxLength(1);
            e.Property(p => p.NextNumber).HasColumnName("NEXT_NUMBER");
            e.Property(p => p.ProductCode).HasColumnName("PRODUCT_CODE").HasMaxLength(10);
            e.Property(p => p.AllowPartCreation).HasColumnName("ALLOW_PART_CREATION").HasMaxLength(1);
            e.Property(p => p.AccountingCompany).HasColumnName("ACCOUNTING_COMPANY");
        }

        private void BuildQcControl(ModelBuilder builder)
        {
            var e = builder.Entity<QcControl>().ToTable("QC_CONTROL");
            e.HasKey(q => q.Id);
            e.Property(q => q.Id).HasColumnName("QC_CONTROL_ID");
            e.Property(q => q.PartNumber).HasColumnName("PART_NUMBER");
            e.Property(q => q.ChangedBy).HasColumnName("CHANGED_BY");
            e.Property(q => q.NumberOfBookIns).HasColumnName("NUMBER_OF_BOOKINS");
            e.Property(q => q.NumberOfBookInsDone).HasColumnName("NUMBER_OF_BOOKINS_DONE");
            e.Property(q => q.OnOrOffQc).HasColumnName("ON_OR_OFF_QC");
            e.Property(q => q.Reason).HasColumnName("REASON");
            e.Property(q => q.TransactionDate).HasColumnName("TRANSACTION_DATE");
        }

        private void BuildParetoClasses(ModelBuilder builder)
        {
            var e = builder.Entity<ParetoClass>().ToTable("PARETO_CLASSES");
            e.HasKey(p => p.ParetoCode);
            e.Property(p => p.ParetoCode).HasColumnName("PARETO_CODE").HasMaxLength(2);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void BuildDecrementRules(ModelBuilder builder)
        {
            var e = builder.Entity<DecrementRule>().ToTable("DECREMENT_RULES");
            e.HasKey(r => r.Rule);
            e.Property(r => r.Rule).HasColumnName("DECREMENT_RULE").HasMaxLength(10);
            e.Property(r => r.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void BuildAssemblyTechnologies(ModelBuilder builder)
        {
            var e = builder.Entity<AssemblyTechnology>().ToTable("ASSEMBLY_TECHNOLOGIES");
            e.HasKey(r => r.Name);
            e.Property(r => r.Name).HasColumnName("NAME").HasMaxLength(4);
            e.Property(r => r.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void BuildDepartments(ModelBuilder builder)
        {
            var e = builder.Entity<Department>().ToTable("LINN_DEPARTMENTS");
            e.HasKey(d => d.DepartmentCode);
            e.Property(d => d.DepartmentCode).HasColumnName("DEPARTMENT_CODE").HasMaxLength(10);
            e.Property(d => d.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(d => d.DateClosed).HasColumnName("DATE_CLOSED");
            e.HasMany(n => n.NominalAccounts).WithOne(a => a.Department).HasForeignKey("DEPARTMENT");
        }

        private void BuildProductAnalysisCodes(ModelBuilder builder)
        {
            var e = builder.Entity<ProductAnalysisCode>().ToTable("PRODUCT_ANALYSIS_CODES");
            e.HasKey(p => p.ProductCode);
            e.Property(p => p.ProductCode).HasColumnName("PRODUCT_CODE").HasMaxLength(10);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(100);
        }

        private void BuildRootProducts(ModelBuilder builder)
        {
            var q = builder.Entity<RootProduct>().ToTable("ROOT_PRODS");
            q.Property(p => p.Name).HasColumnName("ROOT_PRODUCT");
            q.HasKey(p => p.Name);
            q.Property(p => p.Description).HasColumnName("DESCRIPTION");
            q.Property(p => p.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildSosOptions(ModelBuilder builder)
        {
            var e = builder.Entity<SosOption>().ToTable("SOS_OPTIONS");
            e.HasKey(p => p.JobId);
            e.Property(p => p.JobId).HasColumnName("JOB_ID");
            e.Property(p => p.StockPoolCode).HasColumnName("STOCK_POOL_CODE").HasMaxLength(10);
            e.Property(p => p.AccountId).HasColumnName("ACCOUNT_ID");
            e.Property(p => p.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            e.Property(p => p.DespatchLocationCode).HasColumnName("DESPATCH_LOCATION_CODE").HasMaxLength(10);
            e.Property(p => p.AccountingCompany).HasColumnName("ACCOUNTING_COMPANY").HasMaxLength(10);
            e.Property(p => p.CountryCode).HasColumnName("COUNTRY_CODE").HasMaxLength(2);
            e.Property(p => p.CutOffDate).HasColumnName("CUT_OFF_DATE");
        }

        private void BuildSernosSequences(ModelBuilder builder)
        {
            var q = builder.Entity<SernosSequence>().ToTable("SERNOS_SEQUENCES");
            q.HasKey(p => p.Sequence);
            q.Property(p => p.Sequence).HasColumnName("SEQUENCE_NAME");
            q.Property(p => p.Description).HasColumnName("DESCRIPTION");
        }

        private void QueryUnitsOfMeasure(ModelBuilder builder)
        {
            builder.Query<UnitOfMeasure>().ToView("UNITS_OF_MEASURE");
            builder.Query<UnitOfMeasure>().Property(p => p.Unit).HasColumnName("UNIT_OF_MEASURE");
        }

        private void QueryPartCategories(ModelBuilder builder)
        {
            builder.Query<PartCategory>().ToView("PART_CATEGORIES");
            builder.Query<PartCategory>().Property(p => p.Category).HasColumnName("CATEGORY");
            builder.Query<PartCategory>().Property(p => p.Description).HasColumnName("DESCRIPTION");
        }

        private void BuildNominals(ModelBuilder builder)
        {
            builder.Entity<Nominal>().ToTable("LINN_NOMINALS");
            builder.Entity<Nominal>().HasKey(n => n.NominalCode);
            builder.Entity<Nominal>().Property(n => n.NominalCode).HasColumnName("NOMINAL_CODE");
            builder.Entity<Nominal>().Property(n => n.Description).HasColumnName("DESCRIPTION");
            builder.Entity<Nominal>().HasMany(n => n.NominalAccounts).WithOne(a => a.Nominal).HasForeignKey("NOMINAL");
        }

        private void BuildNominalAccounts(ModelBuilder builder)
        {
            builder.Entity<NominalAccount>().ToTable("NOMINAL_ACCOUNTS");
            builder.Entity<NominalAccount>().HasKey(a => a.NominalAccountId);
            builder.Entity<NominalAccount>().Property(a => a.NominalAccountId).HasColumnName("NOMACC_ID");
        }

        private void BuildDespatchLocations(ModelBuilder builder)
        {
            var q = builder.Entity<DespatchLocation>();
            q.HasKey(e => e.Id);
            q.ToTable("DESPATCH_LOCATIONS");
            q.Property(e => e.Id).HasColumnName("BRIDGE_ID");
            q.Property(e => e.LocationCode).HasColumnName("DESPATCH_LOCATION_CODE").HasMaxLength(10);
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.DateInvalid).HasColumnName("DATE_INVALID");
            q.Property(e => e.Sequence).HasColumnName("SEQUENCE");
            q.Property(e => e.UnAllocLocationId).HasColumnName("UNALLOC_LOCATION_ID");
            q.Property(e => e.DefaultCarrier).HasColumnName("DEFAULT_CARRIER").HasMaxLength(10);
        }

        private void BuildStockPools(ModelBuilder builder)
        {
            var q = builder.Entity<StockPool>();
            q.HasKey(e => e.Id);
            q.ToTable("STOCK_POOLS");
            q.Property(e => e.Id).HasColumnName("BRIDGE_ID");
            q.Property(e => e.StockPoolCode).HasColumnName("STOCK_POOL_CODE").HasMaxLength(10);
            q.Property(e => e.Description).HasColumnName("STOCK_POOL_DESCRIPTION").HasMaxLength(50);
            q.Property(e => e.DateInvalid).HasColumnName("DATE_INVALID");
            q.Property(e => e.Sequence).HasColumnName("SEQUENCE");
            q.Property(e => e.AccountingCompany).HasColumnName("ACCOUNTING_COMPANY").HasMaxLength(10);
            q.Property(e => e.StockCategory).HasColumnName("STOCK_CATEGORY").HasMaxLength(1);
            q.Property(e => e.DefaultLocation).HasColumnName("DEFAULT_LOCATION");
        }

        private void QueryChangeRequests(ModelBuilder builder)
        {
            var q = builder.Query<ChangeRequest>();
            q.ToView("CHANGE_REQUESTS");
            q.Property(e => e.ChangeState).HasColumnName("CHANGE_STATE");
            q.Property(e => e.DocumentNumber).HasColumnName("DOCUMENT_NUMBER");
            q.Property(e => e.NewPartNumber).HasColumnName("NEW_PART_NUMBER");
            q.Property(e => e.OldPartNumber).HasColumnName("OLD_PART_NUMBER");
        }

        private void QueryWwdWorkDetails(ModelBuilder builder)
        {
            var q = builder.Query<WwdWorkDetail>();
            q.ToView("WWD_WORK_DETAILS");
            q.Property(e => e.LocationGroup).HasColumnName("LOCATION_GROUP");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(e => e.Quantity).HasColumnName("QTY");
            q.Property(e => e.State).HasColumnName("STATE");
            q.Property(e => e.JobId).HasColumnName("JOB_ID");
        }

        private void QueryWwdWorks(ModelBuilder builder)
        {
            var q = builder.Query<WwdWork>();
            q.ToView("WWD_WORK");
            q.Property(e => e.JobId).HasColumnName("JOB_ID");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(e => e.QuantityKitted).HasColumnName("QTY_KITTED");
            q.Property(e => e.QuantityAtLocation).HasColumnName("QTY_AT_LOCATION");
            q.Property(e => e.StoragePlace).HasColumnName("STORAGE_PLACE");
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.Remarks).HasColumnName("REMARKS");
            q.HasOne(e => e.Part).WithMany(p => p.WwdWorks).HasForeignKey("PART_NUMBER");
        }

        private void BuildStockLocators(ModelBuilder builder)
        {
            var q = builder.Entity<StockLocator>();
            q.ToTable("STOCK_LOCATORS");
            q.HasKey(s => s.Id);
            q.Property(s => s.Id).HasColumnName("STOCK_LOCATOR_ID");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(e => e.BudgetId).HasColumnName("BUDGET_ID");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.HasOne(l => l.StorageLocation).WithMany(s => s.StockLocators).HasForeignKey(l => l.LocationId);
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.Quantity).HasColumnName("QTY");
            q.Property(e => e.QuantityAllocated).HasColumnName("QTY_ALLOCATED");
            q.Property(e => e.StockPoolCode).HasColumnName("STOCK_POOL_CODE");
            q.Property(e => e.Remarks).HasColumnName("REMARKS");
            q.Property(e => e.StockRotationDate).HasColumnName("STOCK_ROTATION_DATE");
            q.Property(e => e.BatchRef).HasColumnName("BATCH_REF");
            q.Property(e => e.State).HasColumnName("STATE").HasMaxLength(6).IsRequired();
            q.Property(e => e.Category).HasColumnName("CATEGORY").HasMaxLength(6).IsRequired();
        }

        private void QueryStoragePlaces(ModelBuilder builder)
        {
            var q = builder.Query<StoragePlace>();
            q.ToView("V_STORAGE_PLACES");
            q.Property(e => e.Description).HasColumnName("STORAGE_PLACE_DESCRIPTION");
            q.Property(e => e.Name).HasColumnName("STORAGE_PLACE");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.SiteCode).HasColumnName("SITE_CODE");
        }

        private void QueryStoresBudgets(ModelBuilder builder)
        {
            var q = builder.Query<StoresBudget>();
            q.ToView("STORES_BUDGETS");
            q.Property(e => e.BudgetId).HasColumnName("BUDGET_ID");
        }

        private void QueryAuditLocations(ModelBuilder builder)
        {
            var q = builder.Query<AuditLocation>();
            q.ToView("V_AUDIT_LOCATIONS");
            q.Property(e => e.StoragePlace).HasColumnName("STORAGE_PLACE");
        }

        private void BuildSosAllocHeads(ModelBuilder builder)
        {
            var table = builder.Entity<SosAllocHead>().ToTable("SOS_ALLOC_HEADS");
            table.HasKey(s => s.Id);
            table.Property(s => s.Id).HasColumnName("BRIDGE_ID");
            table.Property(s => s.JobId).HasColumnName("JOB_ID");
            table.Property(s => s.AccountId).HasColumnName("ACCOUNT_ID");
            table.Property(s => s.OutletNumber).HasColumnName("OUTLET_NUMBER");
            table.HasOne(s => s.SalesOutlet).WithMany(o => o.SosAllocHeads)
                .HasForeignKey(a => new { a.AccountId, a.OutletNumber });
            table.Property(s => s.EarliestRequestedDate).HasColumnName("EARLIEST_REQUESTED_DATE");
            table.Property(s => s.OldestOrder).HasColumnName("OLDEST_ORDER_NUMBER");
            table.Property(s => s.ValueToAllocate).HasColumnName("VALUE_TO_ALLOCATE");
            table.Property(s => s.OutletHoldStatus).HasColumnName("OUTLET_HOLD_STATUS").HasMaxLength(200);
        }

        private void BuildSosAllocDetails(ModelBuilder builder)
        {
            var table = builder.Entity<SosAllocDetail>().ToTable("SOS_ALLOC_DETAILS");
            table.HasKey(s => s.Id);
            table.Property(s => s.Id).HasColumnName("ID");
            table.Property(s => s.JobId).HasColumnName("JOB_ID");
            table.Property(s => s.AccountId).HasColumnName("ACCOUNT_ID");
            table.Property(s => s.OutletNumber).HasColumnName("OUTLET_NUMBER");
            table.Property(s => s.OrderNumber).HasColumnName("ORDER_NUMBER");
            table.Property(s => s.OrderLine).HasColumnName("ORDER_LINE");
            table.Property(s => s.QuantitySuppliable).HasColumnName("QTY_SUPPLIABLE");
            table.Property(s => s.DatePossible).HasColumnName("DATE_POSSIBLE");
            table.Property(s => s.SupplyInFullDate).HasColumnName("SUPPLY_IN_FULL_DATE");
            table.Property(s => s.QuantityToAllocate).HasColumnName("QTY_TO_ALLOCATE");
            table.Property(s => s.QuantityAllocated).HasColumnName("QTY_ALLOCATED");
            table.Property(s => s.UnitPriceIncludingVAT).HasColumnName("UNIT_PRICE_INCL_VAT");
            table.Property(s => s.SupplyInFullCode).HasColumnName("SUPPLY_IN_FULL_CODE").HasMaxLength(1);
            table.Property(s => s.OrderLineHoldStatus).HasColumnName("ORDER_LINE_HOLD_STATUS").HasMaxLength(200);
            table.Property(s => s.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            table.Property(s => s.MaximumQuantityToAllocate).HasColumnName("MAX_QTY_TO_ALLOCATE");
            table.Property(s => s.AllocationMessage).HasColumnName("ALLOCATION_MESSAGE").HasMaxLength(2000);
            table.Property(s => s.AllocationSuccessful).HasColumnName("ALLOCATION_SUCCESSFUL").HasMaxLength(1);
        }

        private void BuildSalesOutlets(ModelBuilder builder)
        {
            var table = builder.Entity<SalesOutlet>().ToTable("V_SALES_OUTLETS");
            table.HasKey(s => new { s.AccountId, s.OutletNumber });
            table.Property(s => s.AccountId).HasColumnName("ACCOUNT_ID");
            table.Property(s => s.OutletNumber).HasColumnName("OUTLET_NUMBER");
            table.Property(s => s.Name).HasColumnName("NAME").HasMaxLength(50);
            table.Property(s => s.SalesCustomerId).HasColumnName("SALES_CUSTOMER_ID");
            table.Property(s => s.CountryCode).HasColumnName("COUNTRY_CODE").HasMaxLength(2);
            table.Property(s => s.CountryName).HasColumnName("COUNTRY_NAME").HasMaxLength(50);
            table.Property(s => s.DateInvalid).HasColumnName("DATE_INVALID");
            table.Property(s => s.SendShipfile).HasColumnName("SEND_SHIPFILE").HasMaxLength(1);
            table.Property(s => s.OutletAddressId).HasColumnName("OUTLET_ADDRESS");
            table.Property(s => s.OrderContactId).HasColumnName("ORDER_CONTACT_ID");
            table.HasMany(o => o.SalesOrders).WithOne(r => r.SalesOutlet).HasForeignKey(o => new { o.AccountId, o.OutletNumber });
        }

        private void BuildParcels(ModelBuilder builder)
        {
            var e = builder.Entity<Parcel>().ToTable("PARCELS");
            e.HasKey(c => c.ParcelNumber);
            e.Property(c => c.ParcelNumber).HasColumnName("PARCEL_NUMBER");
            e.Property(c => c.DateCreated).HasColumnName("DATE_CREATED");
            e.Property(c => c.DateReceived).HasColumnName("DATE_RECEIVED");
            e.Property(c => c.SupplierInvoiceNo).HasColumnName("SUPPLIER_INV_NUMBERS").HasMaxLength(500);
            e.Property(c => c.ConsignmentNo).HasColumnName("CONSIGNMENT_NUMBER").HasMaxLength(20);
            e.Property(c => c.ImportBookNo).HasColumnName("IMPORT_BOOK_NUM").HasMaxLength(8);
            e.Property(c => c.Weight).HasColumnName("WEIGHT");
            e.Property(c => c.CheckedById).HasColumnName("CHECKED_BY");
            e.Property(c => c.SupplierId).HasColumnName("SUPPLIER_ID");
            e.Property(c => c.Comments).HasColumnName("COMMENTS").HasMaxLength(2000);
            e.Property(c => c.CarrierId).HasColumnName("CARRIER");
            e.Property(c => c.PalletCount).HasColumnName("NUMBER_OF_PALLETS");
            e.Property(c => c.CartonCount).HasColumnName("NUMBER_OF_CARTONS");
            e.Property(c => c.DateCancelled).HasColumnName("DATE_CANCELLED").HasMaxLength(2000);
            e.Property(c => c.CancellationReason).HasColumnName("REASON_CANCELLED");
            e.Property(c => c.CancelledBy).HasColumnName("CANCELLED_BY");
        }

        private void BuildImportBooks(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBook>().ToTable("IMPBOOKS");
            q.HasKey(e => e.Id);
            q.Property(e => e.Id).HasColumnName("IMPBOOK_ID");
            q.Property(e => e.DateCreated).HasColumnName("DATE_CREATED");
            q.Property(e => e.ParcelNumber).HasColumnName("PARCEL_NUMBER");
            q.Property(e => e.SupplierId).HasColumnName("SUPPLIER_ID");
            q.Property(e => e.ForeignCurrency).HasColumnName("FOREIGN_CURRENCY").HasMaxLength(1);
            q.Property(e => e.Currency).HasColumnName("CURRENCY").HasMaxLength(4);
            q.Property(e => e.CarrierId).HasColumnName("CARRIER_ID");
            q.Property(e => e.OldArrivalPort).HasColumnName("OLD_ARRIVAL_PORT").HasMaxLength(3);
            q.Property(e => e.FlightNumber).HasColumnName("FLIGHT_NUMBER").HasMaxLength(18);
            q.Property(e => e.TransportId).HasColumnName("TRANSPORT_ID");
            q.Property(e => e.TransportBillNumber).HasColumnName("TRANSPORT_BILL_NUMBER").HasMaxLength(30);
            q.Property(e => e.TransactionId).HasColumnName("TRANSACTION_ID");
            q.Property(e => e.DeliveryTermCode).HasColumnName("DELIVERY_TERM_CODE").HasMaxLength(6);
            q.Property(e => e.ArrivalPort).HasColumnName("ARRIVAL_PORT").HasMaxLength(16);
            q.Property(e => e.LineVatTotal).HasColumnName("LINE_VAT_TOTAL");
            q.Property(e => e.Hwb).HasColumnName("HWB").HasMaxLength(10);
            q.Property(e => e.SupplierCostCurrency).HasColumnName("SUPPLIER_COST_CURRENCY").HasMaxLength(4);
            q.Property(e => e.TransNature).HasColumnName("TRANS_NATURE").HasMaxLength(2);
            q.Property(e => e.ArrivalDate).HasColumnName("ARRIVAL_DATE");
            q.Property(e => e.FreightCharges).HasColumnName("FREIGHT_CHARGES");
            q.Property(e => e.HandlingCharge).HasColumnName("HANDLING_CHARGE");
            q.Property(e => e.ClearanceCharge).HasColumnName("CLEARANCE_CHARGE");
            q.Property(e => e.Cartage).HasColumnName("CARTAGE");
            q.Property(e => e.Duty).HasColumnName("DUTY");
            q.Property(e => e.Vat).HasColumnName("VAT");
            q.Property(e => e.Misc).HasColumnName("MISC");
            q.Property(e => e.CarriersInvTotal).HasColumnName("CARRIERS_INV_TOTAL");
            q.Property(e => e.CarriersVatTotal).HasColumnName("CARRIERS_VAT_TOTAL");
            q.Property(e => e.TotalImportValue).HasColumnName("TOTAL_IMPORT_VALUE");
            q.Property(e => e.Pieces).HasColumnName("PIECES");
            q.Property(e => e.Weight).HasColumnName("WEIGHT");
            q.Property(e => e.CustomsEntryCode).HasColumnName("CUSTOMS_ENTRY_CODE").HasMaxLength(20);
            q.Property(e => e.CustomsEntryCodeDate).HasColumnName("CUSTOMS_ENTRY_CODE_DATE");
            q.Property(e => e.LinnDuty).HasColumnName("LINN_DUTY");
            q.Property(e => e.LinnVat).HasColumnName("LINN_VAT");
            q.Property(e => e.IprCpcNumber).HasColumnName("IPR_CPC_NUMBER");
            q.Property(e => e.EecgNumber).HasColumnName("EECG_NUMBER");
            q.Property(e => e.DateCancelled).HasColumnName("DATE_CANCELLED");
            q.Property(e => e.CancelledBy).HasColumnName("CANCELLED_BY");
            q.Property(e => e.CancelledReason).HasColumnName("CANCELLED_REASON").HasMaxLength(500);
            q.Property(e => e.CarrierInvNumber).HasColumnName("CARRIER_INV_NUMBER").HasMaxLength(20);
            q.Property(e => e.CarrierInvDate).HasColumnName("CARRIER_INV_DATE");
            q.Property(e => e.CountryOfOrigin).HasColumnName("COUNTRY_OF_ORIGIN").HasMaxLength(2);
            q.Property(e => e.FcName).HasColumnName("FC_NAME").HasMaxLength(50);
            q.Property(e => e.VaxRef).HasColumnName("VAX_REF").HasMaxLength(6);
            q.Property(e => e.Storage).HasColumnName("STORAGE");
            q.Property(e => e.NumCartons).HasColumnName("NUM_CARTONS");
            q.Property(e => e.NumPallets).HasColumnName("NUM_PALLETS");
            q.Property(e => e.Comments).HasColumnName("COMMENTS").HasMaxLength(2000);
            q.Property(e => e.ExchangeRate).HasColumnName("EXCHANGE_RATE");
            q.Property(e => e.ExchangeCurrency).HasColumnName("EXCHANGE_CURRENCY").HasMaxLength(4);
            q.Property(e => e.BaseCurrency).HasColumnName("BASE_CURRENCY").HasMaxLength(4);
            q.Property(e => e.PeriodNumber).HasColumnName("PERIOD_NUMBER");
            q.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            q.Property(e => e.PortCode).HasColumnName("PORT_CODE").HasMaxLength(3);
            q.Property(e => e.CustomsEntryCodePrefix).HasColumnName("CUSTOMS_ENTRY_CODE_PREFIX").HasMaxLength(3);
            q.HasMany(t => t.InvoiceDetails).WithOne()
                .HasForeignKey(detail => detail.ImportBookId);
            q.HasMany(t => t.OrderDetails).WithOne()
                .HasForeignKey(detail => detail.ImportBookId);
            q.HasMany(t => t.PostEntries).WithOne()
                .HasForeignKey(detail => detail.ImportBookId);
            q.HasOne(d => d.Supplier).WithOne().HasForeignKey<ImportBook>(s => s.SupplierId);
            q.HasOne(d => d.Carrier).WithOne().HasForeignKey<ImportBook>(s => s.CarrierId);
        }

        private void BuildImportBookInvoiceDetails(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookInvoiceDetail>().ToTable("IMPBOOK_INV_DETAILS");
            q.HasKey(e => new { e.ImportBookId, e.LineNumber });
            q.Property(e => e.ImportBookId).HasColumnName("IMPBOOK_ID");
            q.Property(e => e.LineNumber).HasColumnName("LINE_NUMBER");
            q.Property(e => e.InvoiceNumber).HasColumnName("INVOICE_NUMBER").HasMaxLength(50);
            q.Property(e => e.InvoiceValue).HasColumnName("INVOICE_VALUE");
        }

        private void BuildImportBookOrderDetails(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookOrderDetail>().ToTable("IMPBOOK_ORDER_DETAILS");
            q.HasKey(e => new { e.ImportBookId, e.LineNumber });
            q.Property(e => e.ImportBookId).HasColumnName("IMPBOOK_ID");
            q.Property(e => e.LineNumber).HasColumnName("LINE_NUMBER").HasMaxLength(4);
            q.Property(e => e.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(e => e.RsnNumber).HasColumnName("RSN_NUMBER").HasMaxLength(6);
            q.Property(e => e.OrderDescription).HasColumnName("ORDER_DESCRIPTION").HasMaxLength(2000);
            q.Property(e => e.Qty).HasColumnName("QTY").HasMaxLength(6);
            q.Property(e => e.DutyValue).HasColumnName("DUTY_VALUE");
            q.Property(e => e.FreightValue).HasColumnName("FREIGHT_VALUE");
            q.Property(e => e.VatValue).HasColumnName("VAT_VALUE");
            q.Property(e => e.OrderValue).HasColumnName("ORDER_VALUE");
            q.Property(e => e.Weight).HasColumnName("WEIGHT");
            q.Property(e => e.LoanNumber).HasColumnName("LOAN_NUMBER").HasMaxLength(6);
            q.Property(e => e.LineType).HasColumnName("LINE_TYPE").HasMaxLength(10);
            q.Property(e => e.CpcNumber).HasColumnName("CPC_NUMBER").HasMaxLength(2);
            q.Property(e => e.TariffCode).HasColumnName("TARIFF_CODE").HasMaxLength(12);
            q.Property(e => e.InsNumber).HasColumnName("INS_NUMBER");
            q.Property(e => e.VatRate).HasColumnName("VAT_RATE");
        }

        private void BuildImportBookDeliveryTerms(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookDeliveryTerm>().ToTable("IMPBOOK_DELIVERY_TERMS");
            q.HasKey(e => e.DeliveryTermCode);
            q.Property(e => e.DeliveryTermCode).HasColumnName("DELIVERY_TERM_CODE").HasMaxLength(6);
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(60);
            q.Property(e => e.Comments).HasColumnName("COMMENTS").HasMaxLength(1000);
        }

        private void BuildImportBookPostEntries(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookPostEntry>().ToTable("IMPBOOK_POST_ENTRIES");
            q.HasKey(e => new { e.ImportBookId, e.LineNumber });
            q.Property(e => e.ImportBookId).HasColumnName("IMPBOOK_ID");
            q.Property(e => e.LineNumber).HasColumnName("LINE_NO");
            q.Property(e => e.EntryCodePrefix).HasColumnName("ENTRY_CODE_PREFIX").HasMaxLength(3);
            q.Property(e => e.EntryCode).HasColumnName("ENTRY_CODE").HasMaxLength(20);
            q.Property(e => e.EntryDate).HasColumnName("ENTRY_DATE");
            q.Property(e => e.Reference).HasColumnName("REFERENCE").HasMaxLength(50);
            q.Property(e => e.Duty).HasColumnName("DUTY");
            q.Property(e => e.Vat).HasColumnName("VAT");
        }

        private void BuildImportBookCpcNumbers(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookCpcNumber>().ToTable("IMPBOOK_CPC_NUMBERS");
            q.HasKey(e => e.CpcNumber);
            q.Property(e => e.CpcNumber).HasColumnName("CPC_NUMBER");
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(30);
        }

        private void BuildImportBookExchangeRates(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookExchangeRate>().ToTable("IMPBOOK_EXCHANGE_RATES");
            q.HasKey(e => new { e.PeriodNumber, e.ExchangeCurrency, e.BaseCurrency });
            q.Property(e => e.PeriodNumber).HasColumnName("PERIOD_NUMBER");
            q.Property(e => e.ExchangeCurrency).HasColumnName("EXCHANGE_CURRENCY").HasMaxLength(4);
            q.Property(e => e.BaseCurrency).HasColumnName("BASE_CURRENCY").HasMaxLength(4);
            q.Property(e => e.ExchangeRate).HasColumnName("EXCHANGE_RATE");
        }

        private void BuildImportBookTransactionCodes(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookTransactionCode>().ToTable("IMPBOOK_TRANSACTION_CODES");
            q.HasKey(e => e.TransactionId);
            q.Property(e => e.TransactionId).HasColumnName("TRANSACTION_ID");
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void BuildImportBookTransportCodes(ModelBuilder builder)
        {
            var q = builder.Entity<ImportBookTransportCode>().ToTable("IMPBOOK_TRANSPORT_CODES");
            q.HasKey(e => e.TransportId);
            q.Property(e => e.TransportId).HasColumnName("TRANSPORT_ID");
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void BuildLedgerPeriods(ModelBuilder builder)
        {
            var q = builder.Entity<LedgerPeriod>().ToTable("LEDGER_PERIODS");
            q.HasKey(e => e.PeriodNumber);
            q.Property(e => e.PeriodNumber).HasColumnName("PERIOD_NUMBER");
            q.Property(e => e.MonthName).HasColumnName("MONTH_NAME");
        }
        
        private void BuildPorts(ModelBuilder builder)
        {
            var q = builder.Entity<Port>().ToTable("PORTS");
            q.HasKey(e => e.PortCode);
            q.Property(e => e.PortCode).HasColumnName("PORT_CODE").HasMaxLength(3);
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(30);
        }

        private void BuildPartParamDataSheets(ModelBuilder builder)
        {
            var e = builder.Entity<PartParamData>().ToTable("PART_DATA_SHEETS");
            e.HasKey(d => d.PartNumber);
            e.Property(d => d.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            e.HasOne(d => d.Part).WithOne(p => p.ParamData).HasForeignKey<PartParamData>(s => s.PartNumber);
            e.Property(d => d.AttributeSet).HasColumnName("ATTRIBUTE_SET").HasMaxLength(14);
            e.Property(d => d.Capacitance).HasColumnName("CAPACITANCE");
            e.Property(d => d.Construction).HasColumnName("CONSTRUCTION").HasMaxLength(14);
            e.Property(d => d.Current).HasColumnName("AMPS");
            e.Property(d => d.Diameter).HasColumnName("DIAMETER");
            e.Property(d => d.Dielectric).HasColumnName("DIELECTRIC").HasMaxLength(14);
            e.Property(d => d.Height).HasColumnName("HEIGHT");
            e.Property(d => d.IcFunction).HasColumnName("IC_FUNCTION");
            e.Property(d => d.IcType).HasColumnName("IC_TYPE").HasMaxLength(14);
            e.Property(d => d.Length).HasColumnName("LENGTH");
            e.Property(d => d.NegativeTolerance).HasColumnName("NEG_TOLERANCE");
            e.Property(d => d.Package).HasColumnName("PACKAGE_NAME").HasMaxLength(14);
            e.Property(d => d.Pitch).HasColumnName("PITCH");
            e.Property(d => d.Polarity).HasColumnName("POLARITY").HasMaxLength(14);
            e.Property(d => d.PositiveTolerance).HasColumnName("POS_TOLERANCE");
            e.Property(d => d.Power).HasColumnName("POWER");
            e.Property(d => d.Voltage).HasColumnName("VOLTAGE");
            e.Property(d => d.Resistance).HasColumnName("RESISTANCE");
            e.Property(d => d.TemperatureCoefficient).HasColumnName("TEMP_COEFF");
            e.Property(d => d.TransistorType).HasColumnName("TRANSISTOR_TYPE").HasMaxLength(10);
            e.Property(d => d.Width).HasColumnName("WIDTH");
        }

        private void QueryPartDataSheetValues(ModelBuilder builder)
        {
            var q = builder.Query<PartDataSheetValues>().ToView("PART_DATA_SHEET_VALUES");
            q.Property(v => v.AttributeSet).HasColumnName("ATTRIBUTE_SET");
            q.Property(v => v.Description).HasColumnName("DESCRIPTION");
            q.Property(v => v.Field).HasColumnName("FIELD");
            q.Property(v => v.Value).HasColumnName("VALUE");
        }

        private void BuildTqmsCategories(ModelBuilder builder)
        {
            var e = builder.Entity<TqmsCategory>().ToTable("TQMS_CATEGORIES");
            e.HasKey(c => c.Name);
            e.Property(c => c.Name).HasColumnName("TQMS_CATEGORY");
            e.Property(c => c.Description).HasColumnName("DESCRIPTION");
        }

        private void BuildMechPartPurchasingQuotes(ModelBuilder builder)
        {
            var e = builder.Entity<MechPartPurchasingQuote>().ToTable("MECH_PART_QUOTES");
            e.HasKey(q => new { q.SourceId, q.SupplierId });
            e.Property(q => q.SourceId).HasColumnName("MS_ID");
            e.Property(q => q.SupplierId).HasColumnName("SUPPLIER_ID");
            e.HasOne(q => q.Supplier).WithMany(s => s.PurchasingQuotesSupplierOn).HasForeignKey(q => q.SupplierId);
            e.Property(q => q.LeadTime).HasColumnName("LEAD_TIME");
            e.Property(q => q.ManufacturerCode).HasColumnName("MANUFACTURERS_CODE").HasMaxLength(6);
            e.HasOne(q => q.Manufacturer).WithMany(m => m.PurchasingQuotes).HasForeignKey(q => q.ManufacturerCode);
            e.Property(q => q.ManufacturersPartNumber).HasColumnName("MANUFACTURERS_PART_NUMBER").HasMaxLength(30);
            e.Property(q => q.RohsCompliant).HasColumnName("ROHS_COMPLIANT").HasMaxLength(1);
            e.Property(q => q.UnitPrice).HasColumnName("UNIT_PRICE");
            e.Property(q => q.Moq).HasColumnName("MOQ");
        }

        private void BuildMechPartUsages(ModelBuilder builder)
        {
            var e = builder.Entity<MechPartUsage>().ToTable("MECH_PART_USAGES");
            e.Property(u => u.RootProductName).HasColumnName("ROOT_PRODUCT").HasMaxLength(14);
            e.HasKey(u => new { u.SourceId, u.RootProductName });
            e.Property(u => u.SourceId).HasColumnName("MS_ID");
            e.HasOne(u => u.Source).WithMany(s => s.Usages).HasForeignKey(u => u.SourceId);
            e.Property(u => u.QuantityUsed).HasColumnName("QTY_USED");
            e.HasOne(u => u.RootProduct).WithMany(p => p.UsagesRootProductOn).HasForeignKey(u => u.RootProductName);
        }

        private void BuildPtlMaster(ModelBuilder builder)
        {
            var table = builder.Entity<PtlMaster>().ToTable("PTL_MASTER");
            table.HasKey(a => a.LastFullJobRef);
            table.Property(s => s.LastFullJobRef).HasColumnName("LAST_FULL_RUN_JOBREF").HasMaxLength(6);
            table.Property(s => s.LastFullRunDate).HasColumnName("LAST_FULL_RUN_DATE");
            table.Property(s => s.LastFullRunMinutesTaken).HasColumnName("LAST_FULL_RUN_MINUTES_TAKEN");
            table.Property(s => s.LastDaysToLookAhead).HasColumnName("LAST_DAYS_TO_LOOK_AHEAD");
            table.Property(s => s.Status).HasColumnName("STATUS").HasMaxLength(2000);
        }

        private void BuildTopUpJobRefs(ModelBuilder builder)
        {
            var table = builder.Entity<TopUpListJobRef>().ToTable("WS_TOP_UP_LIST_JOBREFS");
            table.HasKey(a => a.JobRef);
            table.Property(s => s.JobRef).HasColumnName("JOBREF").HasMaxLength(6);
            table.Property(s => s.DateRun).HasColumnName("DATE_RUN");
            table.Property(s => s.FullRun).HasColumnName("FULL_RUN").HasMaxLength(1);
        }

        private void BuildStoresPallets(ModelBuilder builder)
        {
            var e = builder.Entity<StoresPallet>().ToTable("STORES_PALLETS");
            e.HasKey(p => p.PalletNumber);
            e.Property(p => p.PalletNumber).HasColumnName("PALLET_NUMBER").HasMaxLength(6);
            e.Property(p => p.AuditFrequencyWeeks).HasColumnName("AUDIT_FREQUENCY_WEEKS").HasMaxLength(10);
            e.Property(p => p.AuditedByDepartmentCode).HasColumnName("AUDITED_BY_DEPARTMENT_CODE").HasMaxLength(10);
        }

        private void QueryDespatchPickingSummary(ModelBuilder builder)
        {
            var q = builder.Query<DespatchPickingSummary>().ToView("V_DPS_LINN");
            q.Property(v => v.FromPlace).HasColumnName("FROM_PLACE");
            q.Property(v => v.Addressee).HasColumnName("ADDRESSEE");
            q.Property(v => v.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(v => v.LocationId).HasColumnName("LOCATION_ID");
            q.Property(v => v.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            q.Property(v => v.InvoiceDescription).HasColumnName("INVOICE_DESCRIPTION");
            q.Property(v => v.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            q.Property(v => v.Quantity).HasColumnName("QTY");
            q.Property(v => v.Location).HasColumnName("LOCATION");
            q.Property(v => v.QuantityOfItemsAtLocation).HasColumnName("QTY_AT_LOCATION");
            q.Property(v => v.QtyNeededFromLocation).HasColumnName("DPS_QTY_AT_LOCATION");
        }

        private void QueryDespatchPalletQueueDetails(ModelBuilder builder)
        {
            var q = builder.Query<DespatchPalletQueueDetail>().ToView("V_DPQ_SUMMARY");
            q.Property(v => v.KittedFromTime).HasColumnName("KITTED_FROM");
            q.Property(v => v.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(v => v.PickingSequence).HasColumnName("PICKING_SEQUENCE");
            q.Property(v => v.WarehouseInformation).HasColumnName("WAREHOUSE_INFO");
        }

        private void BuildStorageLocations(ModelBuilder builder)
        {
            var e = builder.Entity<StorageLocation>().ToTable("STORAGE_LOCATIONS");
            e.HasKey(l => l.LocationId);
            e.Property(l => l.LocationId).HasColumnName("LOCATION_ID").HasMaxLength(8);
            e.Property(l => l.LocationCode).HasColumnName("LOCATION_CODE").HasMaxLength(16);
            e.Property(l => l.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(l => l.DateInvalid).HasColumnName("DATE_INVALID");
            e.Property(l => l.LocationType).HasColumnName("LOCATION_TYPE").HasMaxLength(1);
            e.Property(l => l.DefaultStockPool).HasColumnName("DEFAULT_STOCK_POOL").HasMaxLength(10);
        }

        private void BuildInspectedStates(ModelBuilder builder)
        {
            var e = builder.Entity<InspectedState>().ToTable("INSPECTED_STATES");
            e.HasKey(l => l.State);
            e.Property(l => l.State).HasColumnName("STATE").HasMaxLength(6);
            e.Property(l => l.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void QueryStockLocatorLocationsView(ModelBuilder builder)
        {
            var q = builder.Query<StockLocatorLocation>().ToView("STOCK_LOCATOR_LOC_VIEW");
            q.Property(e => e.Quantity).HasColumnName("QTY");
            q.Property(e => e.StorageLocationId).HasColumnName("LOCATION_ID");
            q.HasOne(e => e.StorageLocation).WithMany(s => s.StockLocatorLocations).HasForeignKey("LOCATION_ID");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.LocationType).HasColumnName("LOCATION_TYPE").HasMaxLength(1);
            q.Property(e => e.State).HasColumnName("STATE").HasMaxLength(6);
            q.Property(e => e.Category).HasColumnName("CATEGORY").HasMaxLength(6);
            q.Property(e => e.StockPoolCode).HasColumnName("STOCK_POOL_CODE").HasMaxLength(10);
            q.Property(e => e.OurUnitOfMeasure).HasColumnName("OUR_UNIT_OF_MEASURE").HasMaxLength(14);
            q.Property(e => e.QuantityAllocated).HasColumnName("QTY_ALLOCATED");
            q.HasOne(e => e.Part).WithMany(p => p.Locations).HasForeignKey(l => l.PartNumber);
        }

        private void QueryStockLocatorBatches(ModelBuilder builder)
        {
            var q = builder.Query<StockLocatorBatch>().ToView("STOCK_LOCATOR_BATCH_VIEW");
            q.Property(e => e.Quantity).HasColumnName("QTY");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.LocationCode).HasColumnName("LOCATION_CODE").HasMaxLength(16);
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.LocationType).HasColumnName("LOCATION_TYPE").HasMaxLength(1);
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            q.Property(e => e.BatchRef).HasColumnName("BATCH_REF").HasMaxLength(20);
            q.Property(e => e.State).HasColumnName("STATE").HasMaxLength(6);
            q.Property(e => e.Category).HasColumnName("CATEGORY").HasMaxLength(6);
            q.Property(e => e.StockRotationDate).HasColumnName("STOCK_ROTATION_DATE");
            q.Property(e => e.StockPoolCode).HasColumnName("STOCK_POOL_CODE").HasMaxLength(10);
            q.Property(e => e.QuantityAllocated).HasColumnName("QTY_ALLOCATED");
        }

        private void QueryWandConsignments(ModelBuilder builder)
        {
            var q = builder.Query<WandConsignment>().ToView("WAND_CONSIGNMENTS_VIEW");
            q.Property(v => v.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            q.Property(v => v.Addressee).HasColumnName("ADDRESSEE");
            q.Property(v => v.IsDone).HasColumnName("DONE");
            q.Property(v => v.CountryCode).HasColumnName("COUNTRY");
            q.Property(v => v.Address).HasColumnName("ADDRESS");
        }

        private void QueryWandItems(ModelBuilder builder)
        {
            var q = builder.Query<WandItem>().ToView("WAND_ITEMS_VIEW");
            q.Property(v => v.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            q.Property(v => v.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(v => v.PartDescription).HasColumnName("INVOICE_DESCRIPTION");
            q.Property(v => v.Quantity).HasColumnName("QTY");
            q.Property(v => v.QuantityScanned).HasColumnName("RL_QTY_WANDED");
            q.Property(v => v.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(v => v.OrderLine).HasColumnName("ORDER_LINE");
            q.Property(v => v.LinnBarCode).HasColumnName("LINN_BAR_CODE");
            q.Property(v => v.RequisitionNumber).HasColumnName("REQ_NUMBER");
            q.Property(v => v.RequisitionLine).HasColumnName("LINE_NUMBER");
            q.Property(v => v.CountryCode).HasColumnName("COUNTRY");
            q.Property(v => v.BoxesPerProduct).HasColumnName("BOXES_PER_PRODUCT");
            q.Property(v => v.AllWanded).HasColumnName("ALL_WANDED");
            q.Property(v => v.BoxesWanded).HasColumnName("BOXES_WANDED");
            q.Property(v => v.TypeOfSerialNumber).HasColumnName("TYPE_OF_SERIAL_NUMBER");
        }

        private void QueryExportRsns(ModelBuilder builder)
        {
            var q = builder.Query<ExportRsn>().ToView("EXPORT_RSNS_VIEW");
            q.Property(e => e.RsnNumber).HasColumnName("RSN_NUMBER");
            q.Property(e => e.ReasonCodeAlleged).HasColumnName("REASON_CODE_ALLEGED").HasMaxLength(10);
            q.Property(e => e.DateEntered).HasColumnName("DATE_ENTERED");
            q.Property(e => e.Quantity).HasColumnName("QUANTITY");
            q.Property(e => e.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            q.Property(e => e.AccountId).HasColumnName("ACCOUNT_ID");
            q.Property(e => e.OutletNumber).HasColumnName("OUTLET_NUMBER");
            q.Property(e => e.OutletName).HasColumnName("OUTLET_NAME").HasMaxLength(50);
            q.Property(e => e.Country).HasColumnName("COUNTRY").HasMaxLength(2);
            q.Property(e => e.CountryName).HasColumnName("COUNTRY_NAME").HasMaxLength(50);
            q.Property(e => e.AccountType).HasColumnName("ACCOUNT_TYPE").HasMaxLength(10);
        }

        private void QuerySalesAccounts(ModelBuilder builder)
        {
            var q = builder.Entity<SalesAccount>().ToTable("SALES_ACCOUNTS");
            q.HasKey(e => e.AccountId);
            q.Property(e => e.AccountId).HasColumnName("ACCOUNT_ID");
            q.Property(e => e.AccountName).HasColumnName("ACCOUNT_NAME").HasMaxLength(50);
            q.Property(e => e.AccountType).HasColumnName("ACCOUNT_TYPE");
            q.Property(e => e.DateClosed).HasColumnName("DATE_CLOSED");
            q.Property(e => e.OrgId).HasColumnName("ORG_ID");
            q.Property(e => e.ContactId).HasColumnName("CONTACT_ID");
            q.HasOne(a => a.ContactDetails).WithMany(c => c.SalesAccounts).HasForeignKey(a => a.ContactId);
        }

        private void QueryStockQuantitIesForMrView(ModelBuilder builder)
        {
            var q = builder.Query<StockQuantities>().ToView("V_STOCK_QTIES_FOR_MR");
            q.Property(s => s.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(s => s.GoodStock).HasColumnName("GOOD_STOCK");
            q.Property(s => s.GoodStockAllocated).HasColumnName("GOOD_STOCK_ALLOCATED");
            q.Property(s => s.UninspectedStock).HasColumnName("UNINSPECTED_STOCK");
            q.Property(s => s.UninspectedStockAllocated).HasColumnName("UNINSP_STOCK_ALLOCATED");
            q.Property(s => s.FaultyStock).HasColumnName("FAULTY_STOCK");
            q.Property(s => s.FaultyStockAllocated).HasColumnName("FAULTY_STOCK_ALLOCATED");
            q.Property(s => s.DistributorStock).HasColumnName("DISTRIBUTOR_STOCK");
            q.Property(s => s.DistributorStockAllocated).HasColumnName("DISTRIB_STOCK_ALLOCATED");
            q.Property(s => s.SupplierStock).HasColumnName("SUPPLIER_STOCK");
            q.Property(s => s.SupplierStockAllocated).HasColumnName("SUPPLIER_STOCK_ALLOCATED");
            q.Property(s => s.OtherStock).HasColumnName("OTHER_STOCK");
            q.Property(s => s.OtherStockAllocated).HasColumnName("OTHER_STOCK_ALLOCATED");
        }

        private void BuildRequisitionHeaders(ModelBuilder builder)
        {
            var r = builder.Entity<RequisitionHeader>().ToTable("REQUISITION_HEADERS");
            r.HasKey(l => l.ReqNumber);
            r.Property(l => l.ReqNumber).HasColumnName("REQ_NUMBER");
            r.Property(l => l.Document1).HasColumnName("DOCUMENT_1");
            r.Property(l => l.DateCreated).HasColumnName("DATE_CREATED");
            r.HasMany(t => t.Lines).WithOne().HasForeignKey(requisitionLine => requisitionLine.ReqNumber);
            r.HasMany(h => h.Moves).WithOne(m => m.Header).HasForeignKey(h => h.ReqNumber);
        }

        private void BuildRequisitionLines(ModelBuilder builder)
        {
            var r = builder.Entity<RequisitionLine>().ToTable("REQUISITION_LINES");
            r.HasKey(l => new { l.ReqNumber, l.LineNumber });
            r.Property(l => l.ReqNumber).HasColumnName("REQ_NUMBER");
            r.Property(l => l.LineNumber).HasColumnName("LINE_NUMBER");
            r.Property(l => l.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            r.Property(l => l.TransactionCode).HasColumnName("TRANSACTION_CODE").HasMaxLength(10);
            r.HasMany(t => t.Moves).WithOne().HasForeignKey(reqMove => new { reqMove.ReqNumber, reqMove.LineNumber });
        }

        private void BuildReqMoves(ModelBuilder builder)
        {
            var r = builder.Entity<ReqMove>().ToTable("REQ_MOVES");
            r.HasKey(l => new { l.ReqNumber, l.LineNumber, l.Sequence });
            r.Property(l => l.ReqNumber).HasColumnName("REQ_NUMBER");
            r.Property(l => l.LineNumber).HasColumnName("LINE_NUMBER");
            r.Property(l => l.Sequence).HasColumnName("SEQ");
            r.Property(l => l.Quantity).HasColumnName("QTY");
            r.Property(l => l.StockLocatorId).HasColumnName("STOCK_LOCATOR_ID");
            r.HasOne(l => l.StockLocator).WithMany(s => s.ReqMoves).HasForeignKey(l => l.StockLocatorId);
            r.Property(l => l.PalletNumber).HasColumnName("PALLET_NUMBER");
            r.Property(l => l.Booked).HasColumnName("BOOKED");
            r.Property(l => l.StockPoolCode).HasColumnName("STOCK_POOL_CODE").HasMaxLength(10);
            r.Property(l => l.LocationId).HasColumnName("LOCATION_ID");
            r.Property(l => l.Remarks).HasColumnName("REMARKS").HasMaxLength(2000);
            r.HasOne(l => l.Location).WithMany(s => s.ReqMoves).HasForeignKey(l => l.LocationId);
            r.Property(l => l.DateBooked).HasColumnName("DATE_BOOKED");
            r.Property(l => l.DateCancelled).HasColumnName("DATE_CANCELLED");
        }

        private void BuildWandLogs(ModelBuilder builder)
        {
            var table = builder.Entity<WandLog>().ToTable("WANDLOG");
            table.HasKey(w => w.Id);
            table.Property(w => w.Id).HasColumnName("WANDLOG_ID");
            table.Property(w => w.TransType).HasColumnName("TRANS_TYPE").HasMaxLength(2);
            table.Property(w => w.DateWanded).HasColumnName("DATE_WANDED");
            table.Property(w => w.EmployeeNumber).HasColumnName("EMPLOYEE_NUMBER");
            table.Property(w => w.WandString).HasColumnName("WAND_STRING").HasMaxLength(20);
            table.Property(w => w.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            table.Property(w => w.QtyWanded).HasColumnName("QTY_WANDED");
            table.Property(w => w.SeriaNumber1).HasColumnName("SERIAL_NUMBER_1");
            table.Property(w => w.SeriaNumber2).HasColumnName("SERIAL_NUMBER_2");
            table.Property(w => w.OrderNumber).HasColumnName("ORDER_NUMBER");
            table.Property(w => w.OrderLine).HasColumnName("ORDER_LINE");
            table.Property(w => w.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            table.Property(w => w.ItemNo).HasColumnName("ITEM_NO");
            table.Property(w => w.ContainerNo).HasColumnName("CONTAINER_NO");
        }

        private void QueryStockLocatorView(ModelBuilder builder)
        {
            var view = builder.Query<StockLocatorPrices>().ToView("STOCK_LOCATOR_VIEW");
            view.Property(v => v.StockLocatorId).HasColumnName("STOCK_LOCATOR_ID");
            view.Property(v => v.BatchDate).HasColumnName("STOCK_ROTATION_DATE");
            view.Property(v => v.QuantityAtLocation).HasColumnName("QTY");
            view.Property(v => v.BatchRef).HasColumnName("BATCH_REF");
            view.Property(v => v.BudgetId).HasColumnName("BUDGET_ID");
            view.Property(v => v.LabourPrice).HasColumnName("LABOUR_PRICE");
            view.Property(v => v.LocationCode).HasColumnName("LOCATION_CODE");
            view.Property(v => v.MaterialPrice).HasColumnName("MATERIAL_PRICE");
            view.Property(v => v.OverheadPrice).HasColumnName("OVERHEAD_PRICE");
            view.Property(v => v.Pallet).HasColumnName("PALLET_NUMBER");
            view.Property(v => v.PartNumber).HasColumnName("PART_NUMBER");
            view.Property(v => v.Remarks).HasColumnName("REMARKS");
            view.Property(v => v.StockPool).HasColumnName("STOCK_POOL_CODE");
            view.Property(v => v.State).HasColumnName("STATE");
            view.Property(v => v.PartPrice).HasColumnName("PART_PRICE");
            view.Property(v => v.QuantityAllocated).HasColumnName("QTY_ALLOCATED");
            view.Property(v => v.Category).HasColumnName("CATEGORY");
        }

        private void QueryStockAvailable(ModelBuilder builder)
        {
            var q = builder.Query<AvailableStock>().ToView("STOCK_MOVE_STOCK_VIEW");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(e => e.QuantityAvailable).HasColumnName("QTY_FREE");
            q.Property(e => e.StockRotationDate).HasColumnName("STOCK_ROTATION_DATE");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.LocationCode).HasColumnName("LOCATION_CODE");
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.StockPoolCode).HasColumnName("STOCK_POOL_CODE");
            q.Property(e => e.State).HasColumnName("STATE");
            q.Property(e => e.DisplayLocation).HasColumnName("DISPLAY_LOCATION");
            q.Property(e => e.DisplayMoveLocation).HasColumnName("DISPLAY_MOVE_LOCATION");
        }

        private void BuildPartStorageTypes(ModelBuilder builder)
        {
            var r = builder.Entity<PartStorageType>().ToTable("PARTS_STORAGE_TYPES");
            r.HasKey(l => l.Id);
            r.Property(l => l.Id).HasColumnName("BRIDGE_ID");
            r.Property(l => l.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            r.Property(l => l.StorageType).HasColumnName("STORAGE_TYPE").HasMaxLength(4);
            r.Property(l => l.Remarks).HasColumnName("REMARKS").HasMaxLength(30);
            r.Property(l => l.Maximum).HasColumnName("MAXIMUM");
            r.Property(l => l.Increment).HasColumnName("INCR");
            r.Property(l => l.Preference).HasColumnName("PREFERENCE").HasMaxLength(1);
        }

        private void BuildSalesArticles(ModelBuilder builder)
        {
            var e = builder.Entity<SalesArticle>().ToTable("SALES_ARTICLES");
            e.HasKey(a => a.ArticleNumber);
            e.Property(a => a.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            e.Property(a => a.PhaseOutDate).HasColumnName("PHASE_OUT_DATE");
            e.Property(a => a.Description).HasColumnName("INVOICE_DESCRIPTION");
        }

        private void QueryTpkView(ModelBuilder builder)
        {
            var q = builder.Query<TransferableStock>().ToView("V_TPK_OO");
            q.Property(s => s.LocationId).HasColumnName("LOCATION_ID");
            q.Property(s => s.LocationCode).HasColumnName("LOCATION_CODE");
            q.Property(s => s.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(s => s.FromLocation).HasColumnName("FROM_LOCATION");
            q.Property(s => s.StoragePlaceDescription).HasColumnName("STORAGE_PLACE_DESCRIPTION");
            q.Property(s => s.VaxPallet).HasColumnName("VAX_PALLET");
            q.Property(s => s.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            q.Property(s => s.Quantity).HasColumnName("QTY");
            q.Property(s => s.InvoiceDescription).HasColumnName("INVOICE_DESCRIPTION");
            q.Property(s => s.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            q.Property(s => s.Addressee).HasColumnName("ADDRESSEE");
            q.Property(s => s.ReqNumber).HasColumnName("REQ_NUMBER");
            q.Property(s => s.ReqLine).HasColumnName("REQ_LINE");
            q.Property(s => s.DespatchLocationCode).HasColumnName("DESPATCH_LOCATION_CODE");
            q.Property(s => s.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(s => s.OrderLine).HasColumnName("ORDER_LINE");
        }

        private void BuildConsignments(ModelBuilder builder)
        {
            var q = builder.Entity<Consignment>().ToTable("CONSIGNMENTS");
            q.HasKey(c => c.ConsignmentId);
            q.Property(c => c.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            q.Property(c => c.AddressId).HasColumnName("ADDRESS_ID");
            q.Property(c => c.SalesAccountId).HasColumnName("SALES_ACCOUNT_ID");
            q.Property(c => c.DateClosed).HasColumnName("DATE_CLOSED");
            q.Property(c => c.CustomerName).HasColumnName("CUSTOMER_NAME").HasMaxLength(50);
            q.HasOne(c => c.SalesAccount).WithMany(a => a.Consignments).HasForeignKey(c => c.SalesAccountId);
            q.HasOne(c => c.Address).WithMany(a => a.Consignments).HasForeignKey(o => o.AddressId);
            q.Property(c => c.Carrier).HasColumnName("CARRIER").HasMaxLength(10);
            q.Property(c => c.ShippingMethod).HasColumnName("SHIPPING_METHOD").HasMaxLength(1);
            q.Property(c => c.Terms).HasColumnName("TERMS").HasMaxLength(30);
            q.Property(c => c.Status).HasColumnName("STATUS").HasMaxLength(1);
            q.Property(c => c.DateOpened).HasColumnName("DATE_OPENED");
            q.Property(c => c.DespatchLocationCode).HasColumnName("DESPATCH_LOCATION_CODE").HasMaxLength(10);
            q.Property(c => c.Warehouse).HasColumnName("WAREHOUSE").HasMaxLength(1);
            q.Property(c => c.HubId).HasColumnName("HUB_ID");
            q.Property(c => c.ClosedById).HasColumnName("CLOSED_BY");
            q.Property(c => c.CustomsEntryCodePrefix).HasColumnName("CUSTOMS_ENTRY_CODE_PREFIX").HasMaxLength(3);
            q.Property(c => c.CustomsEntryCode).HasColumnName("CUSTOMS_ENTRY_CODE").HasMaxLength(20);
            q.Property(c => c.CustomsEntryCodeDate).HasColumnName("CUSTOMS_ENTRY_CODE_DATE");
            q.HasOne(c => c.ClosedBy).WithMany(m => m.ConsignmentClosedBy).HasForeignKey(s => s.ClosedById);
            q.HasMany(c => c.Pallets).WithOne().HasForeignKey(cp => cp.ConsignmentId);
            q.HasMany(c => c.Items).WithOne().HasForeignKey(ci => ci.ConsignmentId);
        }

        private void QuerySalesOrders(ModelBuilder builder)
        {
            var q = builder.Entity<SalesOrder>().ToTable("SALES_ORDERS");
            q.HasKey(o => o.OrderNumber);
            q.Property(o => o.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(o => o.CurrencyCode).HasColumnName("CURRENCY_CODE");
            q.Property(o => o.OutletNumber).HasColumnName("OUTLET_NUMBER");
            q.Property(o => o.AccountId).HasColumnName("ACCOUNT_ID");
            q.HasOne(o => o.Account).WithMany(a => a.SalesOrders).HasForeignKey(o => o.AccountId);
        }

        private void QuerySalesOrderDetails(ModelBuilder builder)
        {
            var q = builder.Query<SalesOrderDetail>().ToView("SALES_ORDER_DETAILS");
            q.Property(d => d.OrderLine).HasColumnName("ORDER_LINE");
            q.Property(d => d.OrderNumber).HasColumnName("ORDER_NUMBER");
            q.Property(d => d.NettTotal).HasColumnName("NETT_TOTAL");
        }

        private void BuildExportReturns(ModelBuilder builder)
        {
            var q = builder.Entity<ExportReturn>().ToTable("EXPORT_RETURNS");
            q.HasKey(e => e.ReturnId);
            q.Property(e => e.CarrierCode).HasColumnName("CARRIER_CODE").HasMaxLength(10);
            q.Property(e => e.ReturnId).HasColumnName("RETURN_ID");
            q.Property(e => e.DateCreated).HasColumnName("DATE_CREATED");
            q.Property(e => e.Currency).HasColumnName("CURRENCY").HasMaxLength(4);
            q.Property(e => e.AccountId).HasColumnName("ACCOUNT_ID");
            q.Property(e => e.HubId).HasColumnName("HUB_ID");
            q.Property(e => e.OutletNumber).HasColumnName("OUTLET_NUMBER");
            q.Property(e => e.DateDispatched).HasColumnName("DATE_DISPATCHED");
            q.Property(e => e.DateCancelled).HasColumnName("DATE_CANCELLED");
            q.Property(e => e.CarrierRef).HasColumnName("CARRIER_REF").HasMaxLength(32);
            q.Property(e => e.Terms).HasColumnName("TERMS").HasMaxLength(30);
            q.Property(e => e.NumPallets).HasColumnName("NUM_PALLETS");
            q.Property(e => e.NumCartons).HasColumnName("NUM_CARTONS");
            q.Property(e => e.GrossWeightKg).HasColumnName("GROSS_WEIGHT_KG");
            q.Property(e => e.GrossDimsM3).HasColumnName("GROSS_DIMS_M3");
            q.Property(e => e.MadeInterCompanyInvoices).HasColumnName("MADE_INTERCO_INVS");
            q.Property(e => e.DateProcessed).HasColumnName("DATE_PROCESSED");
            q.Property(e => e.ReturnForCredit).HasColumnName("RETURN_FOR_CREDIT");
            q.Property(e => e.ExportCustomsEntryCode).HasColumnName("EXPORT_CUSTOMS_ENTRY_CODE");
            q.Property(e => e.ExportCustomsCodeDate).HasColumnName("EXPORT_CUSTOMS_CODE_DATE");
            q.HasMany(e => e.ExportReturnDetails).WithOne(e => e.ExportReturn).HasForeignKey(e => e.ReturnId);
            q.HasOne(e => e.RaisedBy).WithMany(l => l.ExportReturnsCreated).HasForeignKey("RAISED_BY");
            q.HasOne(e => e.SalesOutlet).WithMany(l => l.ExportReturns)
                .HasForeignKey(e => new { e.AccountId, e.OutletNumber });
        }

        private void BuildExportReturnDetails(ModelBuilder builder)
        {
            var q = builder.Entity<ExportReturnDetail>().ToTable("EXP_RETURN_DETAILS");
            q.HasKey(e => new { e.ReturnId, e.RsnNumber });
            q.Property(e => e.ReturnId).HasColumnName("RETURN_ID");
            q.Property(e => e.RsnNumber).HasColumnName("RSN_NUMBER");
            q.Property(e => e.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(14);
            q.Property(e => e.LineNo).HasColumnName("LINE_NO");
            q.Property(e => e.Qty).HasColumnName("QTY");
            q.Property(e => e.Description).HasColumnName("DESCRIPTION").HasMaxLength(200);
            q.Property(e => e.CustomsValue).HasColumnName("CUSTOMS_VALUE");
            q.Property(e => e.BaseCustomsValue).HasColumnName("BASE_CUSTOMS_VALUE");
            q.Property(e => e.TariffId).HasColumnName("TARIFF_ID");
            q.Property(e => e.ExpInvDocumentType).HasColumnName("EXPINV_DOCUMENT_TYPE").HasMaxLength(1);
            q.Property(e => e.ExpInvDocumentNumber).HasColumnName("EXPINV_DOCUMENT_NUMBER");
            q.Property(e => e.ExpInvDate).HasColumnName("EXPINV_DATE");
            q.Property(e => e.NumCartons).HasColumnName("NUM_CARTONS");
            q.Property(e => e.Weight).HasColumnName("WEIGHT");
            q.Property(e => e.Width).HasColumnName("WIDTH");
            q.Property(e => e.Height).HasColumnName("HEIGHT");
            q.Property(e => e.Depth).HasColumnName("DEPTH");
            q.HasOne(e => e.InterCompanyInvoice).WithOne()
                .HasPrincipalKey<InterCompanyInvoice>(x=>x.ExportReturnId)
                .HasForeignKey<ExportReturnDetail>(z=>z.ReturnId);
        }

        private void BuildIntercompanyInvoices(ModelBuilder builder)
        {
            var q = builder.Entity<InterCompanyInvoice>().ToTable("INTER_COMPANY_INVOICES");
            q.HasKey(e => new { e.DocumentNumber, e.DocumentType });
            q.Property(e => e.DocumentNumber).HasColumnName("DOCUMENT_NUMBER");
            q.Property(e => e.DocumentType).HasColumnName("DOCUMENT_TYPE").HasMaxLength(1);
            q.Property(e => e.ExportReturnId).HasColumnName("EXPORT_RETURN_ID");
        }

        private void QueryTqmsSummaryByCategories(ModelBuilder builder)
        {
            var q = builder.Query<TqmsSummaryByCategory>().ToView("TQMS_CATEGORY_SUMMARY_VIEW");
            q.Property(t => t.JobRef).HasColumnName("JOBREF");
            q.Property(t => t.HeadingCode).HasColumnName("TQMS_HEADING_CODE");
            q.Property(t => t.HeadingDescription).HasColumnName("HEADING_DESCRIPTION");
            q.Property(t => t.CategoryCode).HasColumnName("TQMS_CATEGORY");
            q.Property(t => t.CategoryDescription).HasColumnName("CATEGORY_DESCRIPTION");
            q.Property(t => t.HeadingOrder).HasColumnName("HEADING_ORDER");
            q.Property(t => t.CategoryOrder).HasColumnName("CATEGORY_ORDER");
            q.Property(t => t.TotalValue).HasColumnName("TOTAL_VALUE");
            q.Property(t => t.ActiveCategory).HasColumnName("ACTIVE");
        }

        private void BuildTqmsMaster(ModelBuilder builder)
        {
            var e = builder.Entity<TqmsMaster>().ToTable("TQMS_MASTER");
            e.HasKey(a => a.JobRef);
            e.Property(a => a.JobRef).HasColumnName("JOBREF").HasMaxLength(6);
        }

        private void BuildTqmsJobRefs(ModelBuilder builder)
        {
            var e = builder.Entity<TqmsJobRef>().ToTable("TQMS_JOBREFS");
            e.HasKey(a => a.JobRef);
            e.Property(a => a.JobRef).HasColumnName("JOBREF").HasMaxLength(6);
            e.Property(a => a.DateOfRun).HasColumnName("JOBREF_DATE");
        }

        private void QueryTqmsOutstandingLoansByCategories(ModelBuilder builder)
        {
            var q = builder.Query<TqmsOutstandingLoansByCategory>().ToView("TQMS_OUTSTANDING_LOANS");
            q.Property(t => t.JobRef).HasColumnName("JOBREF");
            q.Property(t => t.Group).HasColumnName("TQMS_GROUP");
            q.Property(t => t.Category).HasColumnName("CATEGORY");
            q.Property(t => t.TotalStoresValue).HasColumnName("TOTAL_STORES_VALUE");
            q.Property(t => t.TotalSalesValue).HasColumnName("TOTAL_SALES_VALUE");
        }

        private void QueryConsignmentShipFiles(ModelBuilder builder)
        {
            var q = builder.Entity<ConsignmentShipfile>().ToTable("CONSIGNMENT_SHIPFILES");
            q.Property(f => f.Id).HasColumnName("CONSH_ID");
            q.HasKey(f => f.Id);
            q.Property(f => f.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            q.Property(f => f.Message).HasColumnName("MESSAGE");
            q.Property(f => f.ShipfileSent).HasColumnName("SHIPFILE_SENT").HasMaxLength(1);
            q.HasOne(f => f.Consignment).WithOne(c => c.Shipfile).HasForeignKey<ConsignmentShipfile>(s => s.ConsignmentId);
        }

        private void QueryInvoices(ModelBuilder builder)
        {
            var q = builder.Entity<Invoice>().ToTable("INVOICES");
            q.HasKey(i => i.DocumentNumber);
            q.Property(i => i.DocumentType).HasColumnName("DOCUMENT_TYPE");
            q.Property(i => i.DocumentNumber).HasColumnName("DOCUMENT_NUMBER");
            q.Property(i => i.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            q.HasOne(i => i.Consignment).WithMany(c => c.Invoices).HasForeignKey(i => i.ConsignmentId);
        }

        private void BuildConsignmentItems(ModelBuilder builder)
        {   
            var entity = builder.Entity<ConsignmentItem>().ToTable("CONSIGNMENT_ITEMS");
            entity.ToTable("CONSIGNMENT_ITEMS");
            entity.HasKey(i => new { i.ConsignmentId, i.ItemNumber });
            entity.Property(i => i.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            entity.Property(i => i.ItemNumber).HasColumnName("ITEM_NO");
            entity.Property(i => i.ItemType).HasColumnName("ITEM_TYPE").HasMaxLength(1);
            entity.Property(i => i.SerialNumber).HasColumnName("SERIAL_NUMBER");
            entity.Property(i => i.Quantity).HasColumnName("QTY");
            entity.Property(i => i.MaybeHalfAPair).HasColumnName("MAY_BE_HALF_A_PAIR").HasMaxLength(1);
            entity.Property(i => i.Weight).HasColumnName("WEIGHT");
            entity.Property(i => i.Height).HasColumnName("HEIGHT");
            entity.Property(i => i.Depth).HasColumnName("DEPTH");
            entity.Property(i => i.Width).HasColumnName("WIDTH");
            entity.Property(i => i.ContainerType).HasColumnName("CONTAINER_TYPE").HasMaxLength(8);
            entity.Property(i => i.ContainerNumber).HasColumnName("CONTAINER_NO");
            entity.Property(i => i.PalletNumber).HasColumnName("PALLET_NO");
            entity.Property(i => i.ItemBaseWeight).HasColumnName("ITEM_BASE_WEIGHT");
            entity.Property(i => i.ItemDescription).HasColumnName("ITEM_DESCRIPTION").HasMaxLength(50);
            entity.Property(i => i.RsnNumber).HasColumnName("RSN_NUMBER");
            entity.Property(i => i.OrderNumber).HasColumnName("ORDER_NUMBER");
            entity.Property(i => i.OrderLine).HasColumnName("ORDER_LINE");
            entity.HasOne(i => i.SalesOrder).WithMany(o => o.ConsignmentItems).HasForeignKey(i => i.OrderNumber);
        }

        private void BuildContacts(ModelBuilder builder)
        {
            var entity = builder.Entity<Contact>().ToTable("CONTACTS");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasColumnName("CONTACT_ID");
            entity.Property(c => c.OrgId).HasColumnName("ORG_ID");
            entity.Property(c => c.EmailAddress).HasColumnName("EMAIL_ADDRESS");
            entity.Property(c => c.AddressId).HasColumnName("ADDRESS_ID");
            entity.Property(c => c.PersonId).HasColumnName("PERSON_ID");
            entity.HasMany(c => c.SalesOutlets).WithOne(o => o.OrderContact).HasForeignKey(o => o.OrderContactId);
            entity.HasOne(c => c.Person).WithMany(p => p.ContactDetails).HasForeignKey(c => c.PersonId);
        }

        private void BuildPersons(ModelBuilder builder)
        {
            var entity = builder.Entity<Person>().ToTable("PERSONS");
            entity.HasKey(p => p.PersonId);
            entity.Property(p => p.PersonId).HasColumnName("PERSON_ID");
            entity.Property(p => p.FirstName).HasColumnName("FIRST_NAME");
            entity.Property(p => p.LastName).HasColumnName("LAST_NAME");
        }

        private void BuildAddresses(ModelBuilder builder)
        {
            var q = builder.Entity<Address>();
            q.ToTable("ADDRESSES");
            q.Property(b => b.Id).HasColumnName("ADDRESS_ID").HasMaxLength(38);
            q.Property(b => b.Addressee).HasColumnName("ADDRESSEE").HasMaxLength(40);
            q.Property(b => b.Addressee2).HasColumnName("ADDRESSEE_2").HasMaxLength(40);
            q.Property(b => b.Line1).HasColumnName("ADDRESS_1").HasMaxLength(40);
            q.Property(b => b.Line2).HasColumnName("ADDRESS_2").HasMaxLength(40);
            q.Property(b => b.Line3).HasColumnName("ADDRESS_3").HasMaxLength(40);
            q.Property(b => b.Line4).HasColumnName("ADDRESS_4").HasMaxLength(40);
            q.Property(b => b.PostCode).HasColumnName("POSTAL_CODE").HasMaxLength(20);
            q.Property(b => b.DateInvalid).HasColumnName("DATE_INVALID");
            q.Property(b => b.CountryCode).HasColumnName("COUNTRY").HasMaxLength(2);
            q.HasOne(b => b.Country).WithMany(c => c.Addresses).HasForeignKey(f => f.CountryCode);
        }

        private void BuildPrinterMappings(ModelBuilder builder)
        {
            var e = builder.Entity<PrinterMapping>().ToTable("PRINTER_MAPPINGS");
            e.HasKey(a => a.Id);
            e.Property(a => a.Id).HasColumnName("ID");
            e.Property(a => a.PrinterName).HasColumnName("PRINTER_NAME").HasMaxLength(100);
            e.Property(a => a.UserNumber).HasColumnName("USER_NUMBER");
            e.Property(a => a.PrinterType).HasColumnName("PRINTER_TYPE").HasMaxLength(50);
            e.Property(a => a.PrinterGroup).HasColumnName("PRINTER_GROUP").HasMaxLength(50);
            e.Property(a => a.DefaultForGroup).HasColumnName("DEFAULT_FOR_GROUP").HasMaxLength(50);
        }

        private void BuildCurrencies(ModelBuilder builder)
        {
            var e = builder.Entity<Currency>().ToTable("CURRENCIES");
            e.HasKey(a => a.Code);
            e.Property(a => a.Code).HasColumnName("CODE");
            e.Property(a => a.Name).HasColumnName("NAME").HasMaxLength(100);
        }

        private void BuildHubs(ModelBuilder builder)
        {
            var h = builder.Entity<Hub>().ToTable("HUBS");
            h.HasKey(a => a.HubId);
            h.Property(a => a.HubId).HasColumnName("HUB_ID");
            h.Property(a => a.Description).HasColumnName("DESCRIPTION").HasMaxLength(240);
            h.Property(a => a.OrgId).HasColumnName("ORG_ID");
            h.Property(a => a.AddressId).HasColumnName("ADDRESS_ID");
            h.Property(a => a.CustomStamp).HasColumnName("CUSTOM_STAMP").HasMaxLength(10);
            h.Property(a => a.CarrierCode).HasColumnName("CARRIER_CODE").HasMaxLength(10);
            h.Property(a => a.EcHub).HasColumnName("EC_HUB").HasMaxLength(1);
        }

        private void BuildCarriers(ModelBuilder builder)
        {
            var c = builder.Entity<Carrier>().ToTable("CARRIERS");
            c.HasKey(a => a.CarrierCode);
            c.Property(a => a.CarrierCode).HasColumnName("CARRIER_CODE").HasMaxLength(10);
            c.Property(a => a.Name).HasColumnName("NAME").HasMaxLength(50);
            c.Property(a => a.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildShippingTerms(ModelBuilder builder)
        {
            var table = builder.Entity<ShippingTerm>().ToTable("SHIPPING_TERMS");
            table.HasKey(a => a.Id);
            table.Property(a => a.Id).HasColumnName("ID");
            table.Property(a => a.Code).HasColumnName("CODE").HasMaxLength(20);
            table.Property(a => a.Description).HasColumnName("DESCRIPTION").HasMaxLength(100);
            table.Property(a => a.DateInvalid).HasColumnName("DATE_INVALID");
        }

        private void BuildConsignmentPallets(ModelBuilder builder)
        {
            var table = builder.Entity<ConsignmentPallet>().ToTable("CONSIGNMENT_PALLETS");
            table.HasKey(a => new { a.ConsignmentId, a.PalletNumber });
            table.Property(a => a.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
            table.Property(a => a.PalletNumber).HasColumnName("PALLET_NO");
            table.Property(a => a.Weight).HasColumnName("WEIGHT");
            table.Property(a => a.Height).HasColumnName("HEIGHT");
            table.Property(a => a.Width).HasColumnName("WIDTH");
            table.Property(a => a.Depth).HasColumnName("DEPTH");
        }

        private void QueryLoanDetails(ModelBuilder builder)
        {
            var q = builder.Query<LoanDetail>().ToView("OS_LOAN_DETAIL_VIEW");
            q.Property(e => e.LoanNumber).HasColumnName("LOAN_NUMBER");
            q.Property(e => e.ArticleNumber).HasColumnName("ARTICLE_NUMBER");
            q.Property(e => e.Line).HasColumnName("LINE_NUMBER");
            q.Property(e => e.QtyOnLoan).HasColumnName("ITEM_QTY");
            q.Property(e => e.SerialNumber).HasColumnName("SERIAL_NUMBER");
            q.Property(e => e.SerialNumber2).HasColumnName("SERIAL_NUMBER2");
            q.Property(e => e.ItemNumber).HasColumnName("ITEM_NUMBER");
        }

        private void BuildCartonTypes(ModelBuilder builder)
        {
            var table = builder.Entity<CartonType>().ToTable("CARTON_TYPES");
            table.HasKey(a => a.CartonTypeName);
            table.Property(a => a.CartonTypeName).HasColumnName("CARTON_TYPE").HasMaxLength(10);
            table.Property(a => a.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void BuildGoodsInLog(ModelBuilder builder)
        {
            var table = builder.Entity<GoodsInLogEntry>().ToTable("GOODS_IN_LOG");
            table.HasKey(e => e.Id);
            table.Property(e => e.Id).HasColumnName("GILOG_ID");
            table.Property(e => e.LoanNumber).HasColumnName("LOAN_NUMBER");
            table.Property(e => e.ArticleNumber).HasColumnName("ARTICLE_NUMBER").HasMaxLength(15);
            table.Property(e => e.OrderLine).HasColumnName("ORDER_LINE");
            table.Property(e => e.ManufacturersPartNumber).HasColumnName("MANUF_PART_NUMBER").HasMaxLength(20);
            table.Property(e => e.State).HasColumnName("STATE").HasMaxLength(10);
            table.Property(e => e.BookInRef).HasColumnName("BOOKIN_REF");
            table.Property(e => e.Comments).HasColumnName("COMMENTS").HasMaxLength(2000);
            table.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            table.Property(e => e.DateCreated).HasColumnName("DATE_CREATED");
            table.Property(e => e.TransactionType).HasColumnName("TRANS_TYPE").HasMaxLength(1);
            table.Property(e => e.WandString).HasColumnName("WAND_STRING").HasMaxLength(20);
            table.Property(e => e.LoanLine).HasColumnName("LINE_NUMBER");
            table.Property(e => e.RsnNumber).HasColumnName("RSN_NUMBER");
            table.Property(e => e.Quantity).HasColumnName("QTY");
            table.Property(e => e.SerialNumber).HasColumnName("SERIAL_NUMBER");
            table.Property(e => e.OrderNumber).HasColumnName("ORDER_NUMBER");
            table.Property(e => e.StoragePlace).HasColumnName("STORAGE_PLACE").HasMaxLength(16);
            table.Property(e => e.DemLocation).HasColumnName("DEM_LOCATION").HasMaxLength(16);
            table.Property(e => e.LogCondition).HasColumnName("CONDITION").HasMaxLength(2000);
            table.Property(e => e.RsnAccessories).HasColumnName("RSN_ACCESSORIES").HasMaxLength(2000);
            table.Property(e => e.StorageType).HasColumnName("STORAGE_TYPE").HasMaxLength(4);
        }

        private void BuildStoresTransactionDefinitions(ModelBuilder builder)
        {
            var q = builder.Entity<StoresTransactionDefinition>().ToTable("STORES_TRANS_DEFS");
            q.HasKey(d => d.TransactionCode);
            q.Property(d => d.TransactionCode).HasColumnName("TRANSACTION_CODE");
            q.Property(d => d.QcType).HasColumnName("QC_TYPE");
            q.Property(d => d.DocType).HasColumnName("DOC1_TYPE");
            q.HasMany(d => d.RequisitionLines).WithOne(l => l.TransactionDefinition)
                .HasForeignKey(l => l.TransactionCode);
        }
        
        private void BuildExportBooks(ModelBuilder builder)
        {
            var table = builder.Entity<ExportBook>().ToTable("EXPBOOKS");
            table.HasKey(a => a.ExportId);
            table.Property(a => a.ExportId).HasColumnName("EXPBOOK_ID");
            table.Property(a => a.ConsignmentId).HasColumnName("CONSIGNMENT_ID");
        }

        private void BuildPlCreditDebitNotes(ModelBuilder builder)
        {
            var entity = builder.Entity<PlCreditDebitNote>().ToTable("PL_CREDIT_DEBIT_NOTES");
            entity.HasKey(a => a.NoteNumber);
            entity.Property(a => a.NoteNumber).HasColumnName("CDNOTE_ID");
            entity.Property(a => a.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            entity.Property(a => a.OrderQty).HasColumnName("ORDER_QTY");
            entity.Property(a => a.ClosedBy).HasColumnName("CLOSED_BY");
            entity.Property(a => a.DateClosed).HasColumnName("DATE_CLOSED");
            entity.Property(a => a.NetTotal).HasColumnName("NET_TOTAL");
            entity.Property(a => a.NoteType).HasColumnName("CDNOTE_TYPE").HasMaxLength(1);
            entity.Property(a => a.OriginalOrderNumber).HasColumnName("ORIGINAL_ORDER_NUMBER");
            entity.Property(a => a.ReturnsOrderNumber).HasColumnName("RETURNS_ORDER_NUMBER");
            entity.Property(a => a.Notes).HasColumnName("NOTES").HasMaxLength(200);
            entity.Property(a => a.ReasonClosed).HasColumnName("REASON_CLOSED").HasMaxLength(2000);
            entity.Property(a => a.SupplierId).HasColumnName("SUPPLIER_ID");
            entity.HasOne(a => a.Supplier).WithMany(s => s.PlCreditDebitNotes).HasForeignKey(a => a.SupplierId);
            entity.Property(a => a.DateCreated).HasColumnName("DATE_CREATED");
        }

        private void QueryStoresLabelTypes(ModelBuilder builder)
        {
            var query = builder.Query<StoresLabelType>().ToView("STORES_LABEL_TYPES");
            query.Property(t => t.Code).HasColumnName("LABEL_TYPE_CODE");
            query.Property(t => t.DefaultPrinter).HasColumnName("DEFAULT_PRINTER");
            query.Property(t => t.FileName).HasColumnName("FILENAME");
        }

        private void BuildPurchaseOrders(ModelBuilder builder)
        {
            var entity = builder.Entity<PurchaseOrder>().ToTable("PL_ORDERS");
            entity.HasKey(o => o.OrderNumber);
            entity.Property(o => o.OrderNumber).HasColumnName("ORDER_NUMBER");
            entity.Property(o => o.SupplierId).HasColumnName("SUPP_SUPPLIER_ID");
            entity.HasOne(o => o.Supplier).WithMany(s => s.PurchaseOrders).HasForeignKey(o => o.SupplierId);
            entity.Property(o => o.DocumentType).HasColumnName("DOCUMENT_TYPE");
        }

        private void BuildPurchaseOrderDetails(ModelBuilder builder)
        {
            var entity = builder.Entity<PurchaseOrderDetail>().ToTable("PL_ORDER_DETAILS");
            entity.HasKey(a => new { a.OrderNumber, a.Line}); 
            entity.Property(o => o.OrderNumber).HasColumnName("ORDER_NUMBER");
            entity.Property(o => o.Line).HasColumnName("ORDER_LINE");
            entity.Property(o => o.RohsCompliant).HasColumnName("ROHS_COMPLIANT");
            entity.Property(o => o.OurQty).HasColumnName("OUR_QTY");
            entity.HasOne(d => d.PurchaseOrder).WithMany(o => o.Details)
                .HasForeignKey(d => d.OrderNumber);
        }

        private void QueryAuthUsers(ModelBuilder builder)
        {
            var query = builder.Query<AuthUser>().ToView("AUTH_USER_VIEW");
            query.Property(t => t.UserNumber).HasColumnName("USER_NUMBER");
            query.Property(t => t.Initials).HasColumnName("INITIALS");
            query.Property(t => t.Name).HasColumnName("USER_NAME");
        }
    }
}
