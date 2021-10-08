namespace Linn.Stores.Facade.Tests.RsnsServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IQueryRepository<Rsn> RsnRepository { get; private set; }

        protected RsnService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.RsnRepository = Substitute.For<IQueryRepository<Rsn>>();
            this.Sut = new RsnService(this.RsnRepository);
        }
    }
}
