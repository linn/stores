namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentShipfileServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IConsignmentShipfileService Sut { get; private set; }

        protected IPdfBuilder PdfBuilder { get; private set; }

        protected IRepository<ConsignmentShipfile, int> ShipfileRepository { get; private set; }

        protected IQueryRepository<SalesOrder> SalesOrderRepository { get; private set; }

        protected IConsignmentShipfileDataService DataService { get; private set; }

        protected IEmailService EmailService { get; private set; }

        protected IQueryRepository<SalesOutlet> OutletRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PdfBuilder = Substitute.For<IPdfBuilder>();
            this.ShipfileRepository = Substitute.For<IRepository<ConsignmentShipfile, int>>();
            this.SalesOrderRepository = Substitute.For<IQueryRepository<SalesOrder>>();
            this.DataService = Substitute.For<IConsignmentShipfileDataService>();
            this.EmailService = Substitute.For<IEmailService>();
            this.OutletRepository = Substitute.For<IQueryRepository<SalesOutlet>>();

            this.Sut = new ConsignmentShipfileService(
                this.EmailService,
                this.PdfBuilder,
                this.ShipfileRepository,
                this.SalesOrderRepository,
                this.DataService,
                this.OutletRepository);
        }
    }
}
