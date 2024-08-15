namespace Linn.Stores.Domain.LinnApps.Tests.StockReportServiceTests
{
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected StockReportService Sut { get; set; }

        protected IFilterByWildcardRepository<StockLocator, int> StockLocatorRepository { get; private set; }

        protected IReportingHelper ReportingHelper { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StockLocatorRepository = Substitute.For<IFilterByWildcardRepository<StockLocator, int>>();
            this.ReportingHelper = new ReportingHelper();
            this.Sut = new StockReportService(
                this.StockLocatorRepository,
                this.ReportingHelper);
        }
    }
}
