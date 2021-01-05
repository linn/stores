namespace Linn.Stores.Domain.LinnApps.Tests.WorkstationServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Workstation;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IWorkstationService Sut { get; private set; }

        protected ISingleRecordRepository<PtlMaster> PtlRepository { get; private set; }

        protected IRepository<TopUpListJobRef, string> TopUpListJobRefRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PtlRepository = Substitute.For<ISingleRecordRepository<PtlMaster>>();
            this.TopUpListJobRefRepository = Substitute.For<IRepository<TopUpListJobRef, string>>();
            this.Sut = new WorkstationService(this.PtlRepository, this.TopUpListJobRefRepository);
        }
    }
}
