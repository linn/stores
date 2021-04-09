namespace Linn.Stores.Facade.Tests.ExportRsnService
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected ExportRsnService Sut { get; private set; }

        protected IQueryRepository<ExportRsn> ExportRsnRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ExportRsnRepository = Substitute.For<IQueryRepository<ExportRsn>>();
            this.Sut = new ExportRsnService(this.ExportRsnRepository);
        }
    }
}
