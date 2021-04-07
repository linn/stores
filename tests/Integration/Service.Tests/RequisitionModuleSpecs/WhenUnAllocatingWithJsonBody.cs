namespace Linn.Stores.Service.Tests.RequisitionModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;
    using Linn.Stores.Resources.Requisitions;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnAllocatingWithJsonBody : ContextBase
    {
        private int reqNumber;

        private int? lineNumber;

        private RequisitionActionResult requisition;

        private RequisitionRequestResource requestResource;

        private int? userNumber;

        [SetUp]
        public void SetUp()
        {
            this.reqNumber = 4345;
            this.lineNumber = 12;
            this.userNumber = 2432;
            this.requisition = new RequisitionActionResult
                                   {
                                       RequisitionHeader = new RequisitionHeader { ReqNumber = this.reqNumber, Document1 = 808 }
                                   };
            this.RequisitionActionsFacadeService.Unallocate(this.reqNumber, this.lineNumber, this.userNumber)
                .Returns(new SuccessResult<RequisitionActionResult>(this.requisition));
            this.requestResource = new RequisitionRequestResource
                                       {
                                           RequisitionNumber = this.reqNumber,
                                           RequisitionLine = this.lineNumber,
                                           UserNumber = this.userNumber
                                       };
            this.Response = this.Browser.Post(
                "/logistics/requisitions/actions/un-allocate",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(this.requestResource);
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
            this.RequisitionActionsFacadeService.Received().Unallocate(this.reqNumber, this.lineNumber, this.userNumber);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<RequisitionActionResource>();
            resource.Requisition.ReqNumber.Should().Be(this.reqNumber);
            resource.Requisition.Document1.Should().Be(this.requisition.RequisitionHeader.Document1);
        }
    }
}
