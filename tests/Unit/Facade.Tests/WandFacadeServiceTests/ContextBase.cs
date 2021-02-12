namespace Linn.Stores.Facade.Tests.WandFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WandFacadeService Sut { get; private set; }

        protected IQueryRepository<WandConsignment> WandConsignmentsRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WandConsignmentsRepository = Substitute.For<IQueryRepository<WandConsignment>>();
            this.Sut = new WandFacadeService(this.WandConsignmentsRepository);
        }
    }
}
