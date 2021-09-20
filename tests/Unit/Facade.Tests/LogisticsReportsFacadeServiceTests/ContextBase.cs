namespace Linn.Stores.Facade.Tests.LogisticsReportsFacadeServiceTests
{
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected LogisticsReportsFacadeService Sut { get; private set; }

        protected IConsignmentService ConsignmentService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ConsignmentService = Substitute.For<IConsignmentService>();
            this.Sut = new LogisticsReportsFacadeService(this.ConsignmentService);
        }
    }
}
