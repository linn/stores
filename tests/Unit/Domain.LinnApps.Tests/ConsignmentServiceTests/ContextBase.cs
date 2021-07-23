namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IConsignmentService Sut { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        protected Consignment Consignment { get; set; }

        protected int ConsignmentId { get; set; }

        protected IConsignmentProxyService ConsignmentProxyService { get; private set; }

        protected IInvoicingPack InvoicingPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.ConsignmentProxyService = Substitute.For<IConsignmentProxyService>();
            this.InvoicingPack = Substitute.For<IInvoicingPack>();

            this.ConsignmentId = 808;
            this.Consignment = new Consignment { ConsignmentId = this.ConsignmentId, Status = "L" };

            this.Sut = new ConsignmentService(
                this.EmployeeRepository,
                this.ConsignmentProxyService,
                this.InvoicingPack);
        }
    }
}
