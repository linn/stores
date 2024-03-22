namespace Linn.Stores.Facade.Tests.ConsignmentFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingConsignmentFromInvoiceNumber : ContextBase
    {
        private IResult<IEnumerable<Consignment>> result;

        private int consignmentId;

        private int invoiceNumber;

        [SetUp]
        public void SetUp()
        {
            this.invoiceNumber = 1001;
            this.consignmentId = 202;

            var consignment = new Consignment
                                  {
                                      ConsignmentId = this.consignmentId,
                                      HubId = 1,
                                      Carrier = "Clumsy",
                                      Terms = "R2D2",
                                      Status = "C",
                                      ShippingMethod = "Throw"
                                  };

            this.ConsignmentService.GetByInvoiceNumber(this.invoiceNumber)
                .Returns(new List<Consignment>() {consignment});

            this.ConsignmentRepository.FindById(this.consignmentId).Returns(consignment);

            this.result = this.Sut.GetByInvoiceNumber(this.invoiceNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Consignment>>>();
        }

        [Test]
        public void ShouldReturnConsignment()
        {
            var consignments = ((SuccessResult<IEnumerable<Consignment>>)this.result).Data;
            consignments.Count().Should().Be(1);
            
            var returnedConsignment = consignments.First();
            returnedConsignment.Should().NotBeNull();
            returnedConsignment.ConsignmentId.Should().Be(this.consignmentId);
        }
    }
}
