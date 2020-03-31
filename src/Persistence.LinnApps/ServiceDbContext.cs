namespace Linn.Stores.Persistence.LinnApps
{
    using Linn.Common.Configuration;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Sos;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class ServiceDbContext : DbContext
    {
        public static readonly LoggerFactory MyLoggerFactory =
            new LoggerFactory(new[]
                                  {
                                      new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
                                  });

        public DbSet<Part> Parts { get; set; }

        public DbSet<ParetoClass> ParetoClasses { get; set; }

        public DbSet<ProductAnalysisCode> ProductAnalysisCodes { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbQuery<RootProduct> RootProducts { get; set; }

        public DbSet<AccountingCompany> AccountingCompanies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<SosOption> SosOptions { get; set; }

        public DbQuery<SernosSequence> SernosSequences { get; set; }

        public DbQuery<UnitOfMeasure> UnitsOfMeasure { get; set; }

        public DbQuery<PartCategory> PartCategories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Nominal> Nominals { get; set; }

        public DbSet<NominalAccount> NominalAccounts { get; set; }

        public DbSet<DespatchLocation> DespatchLocations { get; set; }

        public DbSet<StockPool> StockPools { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildParts(builder);
            this.BuildParetoClasses(builder);
            this.BuildDepartments(builder);
            this.BuildProductAnalysisCodes(builder);
            this.BuildAccountingCompanies(builder);
            this.BuildEmployees(builder);
            this.QueryRootProducts(builder);
            this.BuildSosOptions(builder);
            this.QuerySernosSequences(builder);
            this.QueryUnitsOfMeasure(builder);
            this.QueryPartCategories(builder);
            this.BuildSuppliers(builder);
            this.BuildNominals(builder);
            this.BuildNominalAccounts(builder);
            this.BuildDespatchLocations(builder);
            this.BuildStockPools(builder);
            base.OnModelCreating(builder);
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
        }

        private void BuildParts(ModelBuilder builder)
        {
            var e = builder.Entity<Part>().ToTable("PARTS");
            e.HasKey(p => p.Id);
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
            e.Property(p => p.DecrementRule).HasColumnName("DECREMENT_RULE").HasMaxLength(10);
            e.Property(p => p.BomType).HasColumnName("BOM_TYPE").HasMaxLength(1);
            e.Property(p => p.OptionSet).HasColumnName("OPTION_SET").HasMaxLength(14);
            e.Property(p => p.DrawingReference).HasColumnName("DRAWING_REFERENCE").HasMaxLength(100);
            e.Property(p => p.BomId).HasColumnName("BOM_ID");
            e.Property(p => p.UnitOfMeasure).HasColumnName("UNIT_OF_MEASURE").HasMaxLength(14);
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
            e.Property(p => p.IgnoreWorkstationStock)
                .HasColumnName("IGNORE_WORKSTN_STOCK").HasMaxLength(1);
            e.Property(p => p.ImdsIdNumber).HasColumnName("IMDS_ID_NUMBER");
            e.Property(p => p.ImdsWeight).HasColumnName("IMDS_WEIGHT_G");
            e.Property(p => p.MechanicalOrElectronic)
                .HasColumnName("MECHANICAL_OR_ELECTRONIC").HasMaxLength(2);
            e.Property(p => p.QcOnReceipt).HasColumnName("QC_ON_RECEIPT").HasMaxLength(1);
            e.Property(p => p.QcInformation).HasColumnName("QC_INFORMATION").HasMaxLength(90);
            e.Property(p => p.RawOrFinished).HasColumnName("RM_FG").HasMaxLength(1);
            e.Property(p => p.OurInspectionWeeks).HasColumnName("OUR_INSP_WEEKS");
            e.Property(p => p.SafetyWeeks).HasColumnName("SAFETY_WEEKS");
            e.Property(p => p.RailMethod).HasColumnName("RAIL_METHOD").HasMaxLength(10);
            e.Property(p => p.MinStockRail).HasColumnName("MIN_RAIL");
            e.Property(p => p.MaxStockRail).HasColumnName("MAX_RAIL");
            e.Property(p => p.SecondStageBoard).HasColumnName("SECOND_STAGE_BOARD")
                .HasMaxLength(1);
            e.Property(p => p.SecondStageDescription).HasColumnName("SS_DESCRIPTION").HasMaxLength(100);
            e.Property(p => p.TqmsCategoryOverride).HasColumnName("TQMS_CATEGORY_OVERRIDE").HasMaxLength(20);
            e.Property(p => p.StockNotes).HasColumnName("STOCK_NOTES").HasMaxLength(500);
            e.Property(p => p.DateCreated).HasColumnName("DATE_CREATED");
            e.Property(p => p.DateLive).HasColumnName("DATE_LIVE");
            e.Property(p => p.DatePhasedOut).HasColumnName("DATE_PURCH_PHASE_OUT");
            e.Property(p => p.ReasonPhasedOut)
                .HasColumnName("REASON_PURCH_PHASED_OUT").HasMaxLength(250);
            e.Property(p => p.ScrapOrConvert)
                .HasColumnName("SCRAP_OR_CONVERT").HasMaxLength(20);
            e.Property(p => p.PurchasingPhaseOutType).HasColumnName("PURCH_PHASE_OUT_TYPE").HasMaxLength(20);
            e.Property(p => p.DateDesignObsolete).HasColumnName("DATE_DESIGN_OBSOLETE");
            e.HasOne<Employee>(p => p.PhasedOutBy)
                .WithMany(m => m.PartsPhasedOut).HasForeignKey("PURCH_PHASED_OUT_BY");
            e.HasOne<Employee>(p => p.MadeLiveBy)
                .WithMany(m => m.PartsMadeLive).HasForeignKey("LIVE_BY");
            e.HasOne<Employee>(p => p.CreatedBy).WithMany(m => m.PartsCreated).HasForeignKey("CREATED_BY");
            e.HasOne(p => p.AccountingCompany).WithMany(c => c.PartsResponsibleFor)
                .HasForeignKey("ACCOUNTING_COMPANY");
            e.HasOne(p => p.PreferredSupplier).WithMany(s => s.PartsPreferredSupplierOf)
                .HasForeignKey("PREFERRED_SUPPLIER");
            e.HasOne<ParetoClass>(p => p.ParetoClass).WithMany(c => c.Parts).HasForeignKey("PARETO_CODE");
            e.HasOne<ProductAnalysisCode>(p => p.ProductAnalysisCode).WithMany(c => c.Parts)
                .HasForeignKey("PRODUCT_ANALYSIS_CODE");
            e.HasOne(p => p.NominalAccount).WithMany(a => a.Parts).HasForeignKey("NOMACC_NOMACC_ID");
        }

        private void BuildParetoClasses(ModelBuilder builder)
        {
            var e = builder.Entity<ParetoClass>().ToTable("PARETO_CLASSES");
            e.HasKey(p => p.ParetoCode);
            e.Property(p => p.ParetoCode).HasColumnName("PARETO_CODE").HasMaxLength(2);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
        }

        private void BuildDepartments(ModelBuilder builder)
        {
            var e = builder.Entity<Department>().ToTable("LINN_DEPARTMENTS");
            e.HasKey(d => d.DepartmentCode);
            e.Property(d => d.DepartmentCode).HasColumnName("DEPARTMENT_CODE").HasMaxLength(10);
            e.Property(d => d.Description).HasColumnName("DESCRIPTION").HasMaxLength(50);
            e.Property(d => d.DateClosed).HasColumnName("DATE_CLOSED");
            e.HasMany(n => n.NominalAccounts).WithOne(a => a.Department)
                .HasForeignKey("DEPARTMENT");
        }

        private void BuildProductAnalysisCodes(ModelBuilder builder)
        {
            var e = builder.Entity<ProductAnalysisCode>().ToTable("PRODUCT_ANALYSIS_CODES");
            e.HasKey(p => p.ProductCode);
            e.Property(p => p.ProductCode).HasColumnName("PRODUCT_CODE").HasMaxLength(10);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(100);
        }

        private void QueryRootProducts(ModelBuilder builder)
        {
            var q = builder.Query<RootProduct>().ToView("ROOT_PRODS");
            q.Property(p => p.Name).HasColumnName("ROOT_PRODUCT");
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
        }

        private void QuerySernosSequences(ModelBuilder builder)
        {
            var q = builder.Query<SernosSequence>().ToView("SERNOS_SEQUENCES");
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
            builder.Entity<Nominal>().HasMany(n => n.NominalAccounts).WithOne(a => a.Nominal)
                .HasForeignKey("NOMINAL");
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
    }
}
