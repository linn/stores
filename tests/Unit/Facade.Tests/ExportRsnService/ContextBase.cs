namespace Linn.Stores.Facade.Tests.ExportRsnService
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected ExportReturnService Sut { get; private set; }

        protected IQueryRepository<ExportRsn> ExportRsnRepository { get; private set; }

        protected IExportReturnsPack ExportReturnsPack { get; private set; }

        protected IRepository<ExportReturn, int> ExportReturnRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ExportRsnRepository = Substitute.For<IQueryRepository<ExportRsn>>();
            this.ExportReturnsPack = Substitute.For<IExportReturnsPack>();
            this.ExportReturnRepository = Substitute.For<IRepository<ExportReturn, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new ExportReturnService(
                this.ExportRsnRepository,
                this.ExportReturnsPack,
                this.ExportReturnRepository,
                this.TransactionManager);
        }
    }
}
