namespace Linn.Stores.Facade.Tests.LoansServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IQueryRepository<Loan> LoanHeaderRepository { get; private set; }

        protected LoanService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.LoanHeaderRepository = Substitute.For<IQueryRepository<Loan>>();
            this.Sut = new LoanService(this.LoanHeaderRepository);
        }
    }
}
