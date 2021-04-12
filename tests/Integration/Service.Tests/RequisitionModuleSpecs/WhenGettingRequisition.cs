namespace Linn.Stores.Service.Tests.RequisitionModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Resources.Requisitions;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingRequisition : ContextBase
    {
        private RequisitionHeader requisition;

        [SetUp]
        public void SetUp()
        {
            this.requisition = new RequisitionHeader
                                   {
                                       ReqNumber = 3243,
                                       Document1 = 808,
                                       Lines = new List<RequisitionLine>
                                                   {
                                                       new RequisitionLine
                                                           {
                                                               LineNumber = 1, Moves = new List<ReqMove>()
                                                           }
                                                   }
                                   };
            this.RequisitionFacadeService.GetById(this.requisition.ReqNumber)
                .Returns(new SuccessResult<RequisitionHeader>(this.requisition));
            this.Response = this.Browser.Get(
                $"/logistics/requisitions/{this.requisition.ReqNumber}",
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
            this.RequisitionFacadeService.Received().GetById(this.requisition.ReqNumber);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<RequisitionResource>();
            resource.ReqNumber.Should().Be(this.requisition.ReqNumber);
            resource.Document1.Should().Be(this.requisition.Document1);
        }
    }
}
