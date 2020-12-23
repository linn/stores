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
        
        public DbQuery<StockLocator> StockLocators { get; set; }

        public DbQuery<StoragePlace> StoragePlaces { get; set; }

        public DbQuery<StoresBudget> StoresBudgets { get; set; }

        public DbQuery<AuditLocation> AuditLocations { get; set; }

        public DbSet<SosAllocHead> SosAllocHeads { get; set; }

        public DbSet<Carrier> Carriers { get; set; }

        public DbSet<Parcel> Parcels { get; set; }

        public DbSet<SosAllocDetail> SosAllocDetails { get; set; }

        public DbSet<MechPartAlt> MechPartAlts { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<MechPartManufacturerAlt> MechPartManufacturerAlts { get; set; }

        public DbSet<PartParamData> PartParamDataSheets { get; set; }

        public DbSet<MechPartPurchasingQuote> MechPartPurchasingQuotes { get; set; }


        public DbQuery<PartDataSheetValues> PartDataSheetValues { get; set; }

        public DbSet<TqmsCategory> TqmsCategories { get; set; }

        public DbSet<MechPartUsage> MechPartUsages { get; set; }

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
            this.QueryStockLocators(builder);
            this.QueryStoragePlaces(builder);
            this.QueryStoresBudgets(builder);
            this.QueryAuditLocations(builder);
            this.BuildSosAllocHeads(builder);
            this.BuildCarriers(builder);
            this.BuildParcels(builder);
            this.BuildMechPartAlts(builder);
            this.BuildManufacturers(builder);
            this.BuildMechPartManufacturerAlts(builder);
            this.BuildPartParamDataSheets(builder);
            this.QueryPartDataSheetValues(builder);
            this.BuildSosAllocDetails(builder);
            this.BuildTqmsCategories(builder);
            this.BuildSalesOutlets(builder);
            this.BuildMechPartPurchasingQuotes(builder);
            this.BuildMechPartUsages(builder);
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

        private void BuildCountries(ModelBuilder builder)
        {
            builder.Entity<Country>().ToTable("COUNTRIES");
            builder.Entity<Country>().HasKey(c => c.CountryCode);
            builder.Entity<Country>().Property(c => c.CountryCode).HasColumnName("COUNTRY_CODE").HasMaxLength(2);
            builder.Entity<Country>().Property(c => c.Name).HasColumnName("NAME").HasMaxLength(50);
            builder.Entity<Country>().Property(c => c.DisplayName).HasColumnName("DISPLAY_NAME").HasMaxLength(50);
            builder.Entity<Country>().Property(c => c.TradeCurrency).HasColumnName("TRADE_CURRENCY").HasMaxLength(4);
            builder.Entity<Country>().Property(c => c.ECMember).HasColumnName("EEC_MEMBER").HasMaxLength(1);
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
            e.Property(s => s.ApprovedReferenceStandards).HasColumnName("APPROVED_REFERENCE_STANDARDS").HasMaxLength(200);
            e.Property(s => s.ApprovedReferencesAvailable).HasColumnName("APPROVED_REFERENCES_AVAILABLE").HasMaxLength(1);
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

            e.HasOne(s => s.VerifiedBy).WithMany(m => m.SourcesVerified)
                .HasForeignKey(s => s.VerifiedById);
            e.Property(s => s.VerifiedById).HasColumnName("PURCH_VERIFIED_BY");
            e.Property(s => s.VerifiedDate).HasColumnName("PURCH_VERIFIED_DATE");
            e.HasOne(s => s.QualityVerifiedBy).WithMany(m => m.SourcesQualityVerified)
                .HasForeignKey(s => s.QualityVerifiedById);
            e.Property(s => s.QualityVerifiedById).HasColumnName("QUALITY_VERIFIED_BY");
            e.Property(s => s.QualityVerifiedDate).HasColumnName("QUALITY_VERIFIED_DATE");
            e.HasOne(s => s.McitVerifiedBy).WithMany(m => m.SourcesVerifiedMcit)
                .HasForeignKey(s => s.McitVerifiedById);
            e.Property(s => s.McitVerifiedById).HasColumnName("MCIT_VERIFIED_BY");
            e.Property(s => s.McitVerifiedDate).HasColumnName("MCIT_VERIFIED_DATE");
            e.HasOne(s => s.ApplyTCodeBy).WithMany(m => m.SourcesTCodeApplied)
                .HasForeignKey(s => s.ApplyTCodeId);
            e.Property(s => s.ApplyTCodeId).HasColumnName("APPLY_T_CODE_BY");
            e.Property(s => s.ApplyTCodeDate).HasColumnName("APPLY_T_CODE_DATE");
            e.HasOne(s => s.RemoveTCodeBy).WithMany(m => m.SourcesTCodeRemoved)
                .HasForeignKey(s => s.RemoveTCodeId);
            e.Property(s => s.RemoveTCodeId).HasColumnName("REMOVE_T_CODE_BY");
            e.Property(s => s.RemoveTCodeDate).HasColumnName("REMOVE_T_CODE_DATE");
            e.HasOne(s => s.CancelledBy).WithMany(m => m.SourcesCancelled)
                .HasForeignKey(s => s.CancelledById);
            e.Property(s => s.CancelledById).HasColumnName("CANCELLED_BY");
            e.Property(s => s.DateCancelled).HasColumnName("DATE_CANCELLED");
            e.Property(s => s.LifeExpectancyPart).HasColumnName("LIFE_EXPECTANCY_PART").HasMaxLength(50);
            e.Property(s => s.Configuration).HasColumnName("CONFIGURATION").HasMaxLength(200);
        }

        private void BuildMechPartAlts(ModelBuilder builder)
        {
            var e = builder.Entity<MechPartAlt>().ToTable("MECH_PART_ALTS");
            e.HasKey(m => new { m.MechPartSourceId, m.Sequence});
            e.Property(m => m.MechPartSourceId).HasColumnName("MS_ID").HasMaxLength(8);
            e.Property(m => m.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(30);
            e.Property(m => m.Sequence).HasColumnName("SEQ");
            e.HasOne<Supplier>(s => s.Supplier).WithMany(s => s.MechPartAlts).HasForeignKey("SUPPLIER_ID");
            e.HasOne<MechPartSource>(s => s.MechPartSource).WithMany(s => s.MechPartAlts).HasForeignKey(x => x.MechPartSourceId);
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
            e.HasOne<Manufacturer>(s => s.Manufacturer).WithMany(s => s.MechPartManufacturerAlts).HasForeignKey(x => x.ManufacturerCode);
            e.HasOne<MechPartSource>(s => s.MechPartSource).WithMany(s => s.MechPartManufacturerAlts).HasForeignKey(x => x.MechPartSourceId);
            e.Property(m => m.ReelSuffix).HasColumnName("REEL_SUFFIX").HasMaxLength(2);
            e.Property(m => m.RohsCompliant).HasColumnName("ROHS_COMPL").HasMaxLength(1);
            e.HasOne<Employee>(s => s.ApprovedBy).WithMany(m => m.MechPartManufacturerAltsApproved).HasForeignKey("APPROVED_BY");
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
        }

        private void QueryStockLocators(ModelBuilder builder)
        {
            var q = builder.Query<StockLocator>();
            q.ToView("STOCK_LOCATORS");
            q.Property(e => e.PartNumber).HasColumnName("PART_NUMBER");
            q.Property(e => e.BudgetId).HasColumnName("BUDGET_ID");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
            q.Property(e => e.Quantity).HasColumnName("QTY");
            q.Property(e => e.QuantityAllocated).HasColumnName("QTY_ALLOCATED");
        }

        private void QueryStoragePlaces(ModelBuilder builder)
        {
            var q = builder.Query<StoragePlace>();
            q.ToView("V_STORAGE_PLACES");
            q.Property(e => e.StoragePlaceDescription).HasColumnName("STORAGE_PLACE_DESCRIPTION");
            q.Property(e => e.Name).HasColumnName("STORAGE_PLACE");
            q.Property(e => e.LocationId).HasColumnName("LOCATION_ID");
            q.Property(e => e.PalletNumber).HasColumnName("PALLET_NUMBER");
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
            table.HasOne(s => s.SalesOutlet).WithMany(o => o.SosAllocHeads).HasForeignKey(a => new { a.AccountId, a.OutletNumber });
            table.Property(s => s.EarliestRequestedDate).HasColumnName("EARLIEST_REQUESTED_DATE");
            table.Property(s => s.OldestOrder).HasColumnName("OLDEST_ORDER_NUMBER");
            table.Property(s => s.ValueToAllocate).HasColumnName("VALUE_TO_ALLOCATE");
            table.Property(s => s.OutletHoldStatus).HasColumnName("OUTLET_HOLD_STATUS").HasMaxLength(200);
        }

        private void BuildCarriers(ModelBuilder builder)
        {
            var e = builder.Entity<Carrier>().ToTable("CARRIERS");
            e.HasKey(c => c.CarrierCode);
            e.Property(c => c.CarrierCode).HasColumnName("CARRIER_CODE").HasMaxLength(10);
            e.Property(c => c.Name).HasColumnName("NAME");
            e.Property(c => c.OrganisationId).HasColumnName("ORG_ID");
            e.Property(c => c.DateInvalid).HasColumnName("DATE_INVALID");
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
            var table = builder.Entity<SalesOutlet>().ToTable("SALES_OUTLETS");
            table.HasKey(s => new { s.AccountId, s.OutletNumber });
            table.Property(s => s.AccountId).HasColumnName("ACCOUNT_ID");
            table.Property(s => s.OutletNumber).HasColumnName("OUTLET_NUMBER");
            table.Property(s => s.Name).HasColumnName("NAME").HasMaxLength(50);
            table.Property(s => s.SalesCustomerId).HasColumnName("SALES_CUSTOMER_ID");
        }

        private void BuildParcels(ModelBuilder builder)
        {
            var e = builder.Entity<Parcel>().ToTable("PARCELS");
            e.HasKey(c => c.ParcelNumber);
            e.Property(c => c.ParcelNumber).HasColumnName("PARCEL_NUMBER");
            e.Property(c => c.DateCreated).HasColumnName("DATE_CREATED");
            e.Property(c => c.DateReceived).HasColumnName("DATE_RECEIVED");
            e.Property(c => c.SupplierInvoiceNo).HasColumnName("SUPPLIER_INV_NUMBERS");
            e.Property(c => c.ConsignmentNo).HasColumnName("CONSIGNMENT_NUMBER");
            e.Property(c => c.Weight).HasColumnName("WEIGHT");
            e.Property(c => c.CheckedById).HasColumnName("CHECKED_BY");
            e.Property(c => c.SupplierId).HasColumnName("SUPPLIER_ID");
            e.Property(c => c.Comments).HasColumnName("COMMENTS");
            e.Property(c => c.CarrierId).HasColumnName("CARRIER");
            e.Property(c => c.PalletCount).HasColumnName("NUMBER_OF_PALLETS");
            e.Property(c => c.CartonCount).HasColumnName("NUMBER_OF_CARTONS");
            e.Property(c => c.DateCancelled).HasColumnName("DATE_CANCELLED");
            e.Property(c => c.CancellationReason).HasColumnName("REASON_CANCELLED");
            e.Property(c => c.CancelledBy).HasColumnName("CANCELLED_BY");
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
            e.HasOne(u => u.RootProduct).WithMany(p => p.UsagesRootProductOn)
                .HasForeignKey(u => u.RootProductName);
        }
    }
}
