namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IWandService Sut { get; private set; }

        protected IWandPack WandPack { get; private set; }

        protected IRepository<WandLog, int> WandLogRepository { get; private set; }

        protected IRepository<Consignment, int> ConsignmentRepository { get; private set; }

        protected IBartenderLabelPack BartenderLabelPack { get; private set; }

        protected IRepository<PrinterMapping, int> PrinterMappingRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WandPack = Substitute.For<IWandPack>();
            this.WandLogRepository = Substitute.For<IRepository<WandLog, int>>();
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();
            this.BartenderLabelPack = Substitute.For<IBartenderLabelPack>();
            this.PrinterMappingRepository = Substitute.For<IRepository<PrinterMapping, int>>();

            this.Sut = new WandService(
                this.WandPack,
                this.WandLogRepository,
                this.ConsignmentRepository,
                this.BartenderLabelPack,
                this.PrinterMappingRepository);
        }
    }
}
