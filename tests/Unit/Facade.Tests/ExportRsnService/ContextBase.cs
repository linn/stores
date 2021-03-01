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
        protected ExportRsnService Sut { get; private set; }

        protected IQueryRepository<ExportRsn> ExportRsnRepository { get; private set; }

        protected IExportReturnsPack ExportReturnsPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ExportRsnRepository = Substitute.For<IQueryRepository<ExportRsn>>();
            this.ExportReturnsPack = Substitute.For<IExportReturnsPack>();
            this.Sut = new ExportRsnService(this.ExportRsnRepository, this.ExportReturnsPack);
        }
    }
}
