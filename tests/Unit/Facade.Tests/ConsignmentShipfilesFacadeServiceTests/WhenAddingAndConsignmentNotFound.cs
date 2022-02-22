namespace Linn.Stores.Facade.Tests.ConsignmentShipfilesFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    using NUnit.Framework;

    public class WhenAddingAndConsignmentNotFound : ContextBase
    {
        private IResult<ConsignmentShipfile> result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.Add(new ConsignmentShipfileResource { InvoiceNumbers = "123" });
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.GetType().Should().Be(typeof(BadRequestResult<ConsignmentShipfile>));
            ((BadRequestResult<ConsignmentShipfile>)this.result).Message.Should().Be("Consignment Not Found");
        }
    }
}
