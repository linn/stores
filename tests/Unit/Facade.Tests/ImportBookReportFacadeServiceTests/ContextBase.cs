namespace Linn.Stores.Facade.Tests.ImportBookReportFacadeServiceTests

{
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ImportBookReportFacadeService Sut { get; set; }

        protected IImportBookReportService ReportService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ReportService = Substitute.For<IImportBookReportService>();
            this.Sut = new ImportBookReportFacadeService(this.ReportService);
        }
    }
}
