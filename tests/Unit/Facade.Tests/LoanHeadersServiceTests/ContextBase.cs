namespace Linn.Stores.Facade.Tests.LoanHeadersServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IQueryRepository<LoanHeader> LoanHeaderRepository { get; private set; }

        protected LoanHeaderService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LoanHeaderRepository = Substitute.For<IQueryRepository<LoanHeader>>();
            this.Sut = new LoanHeaderService(this.LoanHeaderRepository);
        }
    }
}
