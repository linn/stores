namespace Linn.Stores.Facade.Tests.LogisticsLabelFacadeServiceTests
{
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected LogisticsLabelFacadeService Sut { get; private set; }

        protected ILogisticsLabelService LogisticsLabelService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LogisticsLabelService = Substitute.For<ILogisticsLabelService>();
            this.Sut = new LogisticsLabelFacadeService(this.LogisticsLabelService);
        }
    }
}
