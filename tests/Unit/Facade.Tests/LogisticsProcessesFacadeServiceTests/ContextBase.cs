namespace Linn.Stores.Facade.Tests.LogisticsProcessesFacadeServiceTests
{
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected LogisticsProcessesFacadeService Sut { get; private set; }

        protected ILogisticsLabelService LogisticsLabelService { get; private set; }

        protected IConsignmentService ConsignmentService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LogisticsLabelService = Substitute.For<ILogisticsLabelService>();
            this.ConsignmentService = Substitute.For<IConsignmentService>();
            this.Sut = new LogisticsProcessesFacadeService(this.LogisticsLabelService, this.ConsignmentService);
        }
    }
}
