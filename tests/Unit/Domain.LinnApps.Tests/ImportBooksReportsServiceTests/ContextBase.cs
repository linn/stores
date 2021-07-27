namespace Linn.Stores.Domain.LinnApps.Tests.ImportBooksReportsServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Reports;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IImportBookReportService Sut { get; private set; }

        protected IRepository<ImportBook, int> ImpbookRepository { get; private set; }

        protected IRepository<Country, string> CountryRepository { get; private set; }

        protected IRepository<ExportReturnDetail, ExportReturnDetailKey> ExportReturnDetailRepository { get; private set; }



        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ImpbookRepository = Substitute.For<IRepository<ImportBook, int>>();
            this.CountryRepository = Substitute.For<IRepository<Country, string>>();
            this.ExportReturnDetailRepository = Substitute.For<IRepository<ExportReturnDetail, ExportReturnDetailKey>>();

            this.ReportingHelper = new ReportingHelper();

            this.Sut = new ImportBookReportService(
                this.ImpbookRepository, this.CountryRepository, this.ExportReturnDetailRepository,
                this.ReportingHelper);
        }
    }
}
