namespace Persistence.LinnApps
{
    using Domain.LinnApps;
    using Domain.LinnApps.Parts;


    using Linn.Common.Configuration;
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

        public DbSet<ProductAnalysisCode> ProuAnalysisCodes { get; set; }

        public DbQuery<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.BuildParts(builder);
            this.BuildParetoClasses(builder);
            this.BuildDepartments(builder);
            this.BuildProductAnalysisCodes(builder);
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

        private void BuildParts(ModelBuilder builder)
        {
            var e = builder.Entity<Part>().ToTable("PARTS");
            e.HasKey(p => p.PartNumber);
            e.Property(p => p.PartNumber).HasColumnName("PART_NUMBER").HasMaxLength(14);
            e.Property(p => p.BridgeId).HasColumnName("BRIDGE_ID");
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(200);
            e.Property(p => p.RootProduct).HasColumnName("ROOT_PRODUCT").HasMaxLength(10);
            e.Property(p => p.StockControlled).HasColumnName("STOCK_CONTROLLED").HasMaxLength(1);
            e.Property(p => p.SafetyCriticalPart).HasColumnName("SAFETY_CRITICAL_PART").HasMaxLength(1);
            e.Property(p => p.EmcCriticalPart).HasColumnName("EMC_CRITICAL_PART").HasMaxLength(1);
            e.Property(p => p.CccCriticalPart).HasColumnName("CCC_CRITICAL_PART").HasMaxLength(1);
            e.Property(p => p.PsuPart).HasColumnName("PSU_PART").HasMaxLength(1);
            e.Property(p => p.SingleSourcePart).HasColumnName("SINGLE_SOURCE_PART").HasMaxLength(1);
            e.Property(p => p.AccountingCompany).HasColumnName("ACCOUNTING_COMPANY").HasMaxLength(10);
            e.Property(p => p.SafetyCertificateExpirationDate).HasColumnName("SAFETY_CERTIFICATE_EXPIRATION_DATE");
            e.HasOne<ParetoClass>(p => p.ParetoClass).WithMany(c => c.Parts).HasForeignKey("PARETO_CODE"); // ?
            e.HasOne<ProductAnalysisCode>(p => p.ProductAnalysisCode).WithMany(c => c.Parts)
                .HasForeignKey("PRODUCT_ANALYSIS_CODE"); // ?
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
        }

        private void BuildProductAnalysisCodes(ModelBuilder builder)
        {
            var e = builder.Entity<ProductAnalysisCode>();
            e.HasKey(p => p.ProductCode);
            e.Property(p => p.ProductCode).HasColumnName("PRODUCT_CODE").HasMaxLength(10);
            e.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(100);
        }
    }
}
