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

    public class WhenGettingAll : ContextBase
    {
        private Consignment consignment1;

        private Consignment consignment2;

        [SetUp]
        public void SetUp()
        {
            this.consignment1 = new Consignment { ConsignmentId = 1 };
            this.consignment2 = new Consignment { ConsignmentId = 2 };

            this.ConsignmentFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Consignment>>(new List<Consignment>
                                                                         {
                                                                             this.consignment1, this.consignment2
                                                                         }));

            this.Response = this.Browser.Get(
                "/logistics/consignments",
                with =>
                {
                    with.Header("Accept", "application/json");
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
            this.ConsignmentFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<ConsignmentResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.Should().Contain(a => a.ConsignmentId == 1);
            resultResource.Should().Contain(a => a.ConsignmentId == 2);
        }
    }
}
