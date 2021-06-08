namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentShipfileServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IConsignmentShipfileService Sut { get; private set; }

        protected IPdfService PdfService { get; private set; }

        protected IRepository<ConsignmentShipfile, int> ShipfileRepository { get; private set; }

        protected IQueryRepository<SalesOrder> SalesOrderRepository { get; private set; }

        protected IConsignmentShipfileDataService DataService { get; private set; }

        protected IEmailService EmailService { get; private set; }

        protected ITemplateEngine TemplateEngine { get; private set; }

        protected IQueryRepository<SalesOutlet> OutletRepository { get; private set; }

        protected IRepository<Consignment, int> ConsignmentRepository { get; private set; }

        protected IPackingListService PackingListService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.PdfService = Substitute.For<IPdfService>();
            this.ShipfileRepository = Substitute.For<IRepository<ConsignmentShipfile, int>>();
            this.SalesOrderRepository = Substitute.For<IQueryRepository<SalesOrder>>();
            this.DataService = Substitute.For<IConsignmentShipfileDataService>();
            this.EmailService = Substitute.For<IEmailService>();
            this.OutletRepository = Substitute.For<IQueryRepository<SalesOutlet>>();
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();
            this.TemplateEngine = Substitute.For<ITemplateEngine>();
            this.PackingListService = Substitute.For<IPackingListService>();

            this.Sut = new ConsignmentShipfileService(
                this.EmailService,
                this.TemplateEngine,
                this.PdfService,
                this.ShipfileRepository,
                this.SalesOrderRepository,
                this.DataService,
                this.OutletRepository,
                this.ConsignmentRepository,
                this.PackingListService);
        }
    }
}
