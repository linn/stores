namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByInvoiceNumber : ContextBase
    {
        private int invoiceNumber;

        private Consignment consignment;

        [SetUp]
        public void SetUp()
        {
            this.invoiceNumber = 1;
            this.consignment = new Consignment { ConsignmentId = 2 };

            this.ConsignmentFacadeService.GetByInvoiceNumber(this.invoiceNumber)
                .Returns(new SuccessResult<IEnumerable<Consignment>>(new List<Consignment>
                                                                         {
                                                                             this.consignment
                                                                         }));

            this.Response = this.Browser.Get(
                "/logistics/consignments",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("invoiceNumber", this.invoiceNumber.ToString());
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ConsignmentFacadeService.Received().GetByInvoiceNumber(this.invoiceNumber);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<ConsignmentResource>>().ToList();
            resultResource.Should().HaveCount(1);
            resultResource.Should().Contain(a => a.ConsignmentId == 2);
        }
    }
}
