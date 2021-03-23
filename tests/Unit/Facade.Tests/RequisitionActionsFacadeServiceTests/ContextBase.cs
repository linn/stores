namespace Linn.Stores.Facade.Tests.RequisitionActionsFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected RequisitionActionsFacadeService Sut { get; private set; }

        protected IRequisitionService RequisitionService { get; private set; }

        protected IRepository<RequisitionHeader, int> RequisitionRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.RequisitionService = Substitute.For<IRequisitionService>();
            this.RequisitionRepository = Substitute.For<IRepository<RequisitionHeader, int>>();
            this.Sut = new RequisitionActionsFacadeService(this.RequisitionService, this.RequisitionRepository);
        }
    }
}
