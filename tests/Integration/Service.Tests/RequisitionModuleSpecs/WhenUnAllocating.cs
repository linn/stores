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

    public class WhenUnAllocating : ContextBase
    {
        private int reqNumber;

        private int? lineNumber;

        private RequisitionActionResult requisition;

        [SetUp]
        public void SetUp()
        {
            this.reqNumber = 4345;
            this.lineNumber = 12;
            this.requisition = new RequisitionActionResult
                                   {
                                       RequisitionHeader =
                                           new RequisitionHeader { ReqNumber = this.reqNumber, Document1 = 808 }
                                   };
            this.RequisitionActionsFacadeService.Unallocate(this.reqNumber, this.lineNumber, 100)
                .Returns(new SuccessResult<RequisitionActionResult>(this.requisition));
            this.Response = this.Browser.Post(
                $"/logistics/requisitions/{this.reqNumber}/lines/{this.lineNumber}/un-allocate",
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
            this.RequisitionActionsFacadeService.Received().Unallocate(this.reqNumber, this.lineNumber, 100);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<RequisitionActionResource>();
            resource.RequisitionHeader.ReqNumber.Should().Be(this.reqNumber);
            resource.RequisitionHeader.Document1.Should().Be(this.requisition.RequisitionHeader.Document1);
        }
    }
}
