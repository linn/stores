namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Consignments;

    using NSubstitute;

    public class WhenGettingConsignmentFromInvoiceNumber : ContextBase
    {
        private IEnumerable<Consignment> result;

        private int invoiceNumber;

        [SetUp]
        public void SetUp()
        {
            this.invoiceNumber = 100;

            var invoice = new Invoice() {DocumentNumber = this.invoiceNumber, ConsignmentId = this.ConsignmentId};


            this.InvoiceRepository.FindById(this.invoiceNumber).Returns(invoice);
            
            this.ConsignmentRepository.FindById(this.ConsignmentId).Returns(this.Consignment);

            this.result = this.Sut.GetByInvoiceNumber(this.invoiceNumber);
        }

        [Test]
        public void GetConsignment()
        {
            this.ConsignmentRepository.Received().FindById(this.ConsignmentId);
        }

        [Test]
        public void ShouldReturnConsignmentDetails()
        {
            this.result.Count().Should().Be(1);
            var consignment = this.result.FirstOrDefault();
            consignment.Should().NotBeNull();
            consignment.ConsignmentId.Should().Be(this.ConsignmentId);
        }
    }
}
