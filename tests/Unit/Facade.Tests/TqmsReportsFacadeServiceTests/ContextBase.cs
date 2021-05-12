namespace Linn.Stores.Facade.Tests.TqmsReportsFacadeServiceTests
{
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected TqmsReportsFacadeService Sut { get; private set; }

        protected ITqmsReportsService TqmsReportsService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TqmsReportsService = Substitute.For<ITqmsReportsService>();
            this.Sut = new TqmsReportsFacadeService(this.TqmsReportsService);
        }
    }
}
