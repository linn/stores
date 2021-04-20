namespace Linn.Stores.Service.Tests.StockMoveModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStock : ContextBase
    {
        private RequisitionProcessResult processResult;

        private MoveStockRequestResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new MoveStockRequestResource { From = "Loc1", To = "Loc2" };
            this.processResult = new RequisitionProcessResult { Success = true, Message = "ok", ReqNumber = 23 };
            this.MoveStockFacadeService.MoveStock(Arg.Is<MoveStockRequestResource>(
                    a => a.From == this.resource.From && a.To == this.resource.To))
                .Returns(new SuccessResult<RequisitionProcessResult>(this.processResult));

            this.Response = this.Browser.Post(
                "/inventory/move-stock",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(this.resource);
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
            this.MoveStockFacadeService.Received()
                .MoveStock(Arg.Is<MoveStockRequestResource>(
                    a => a.From == this.resource.From && a.To == this.resource.To));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<ProcessResultResource>();
            resultResource.Message.Should().Be(this.processResult.Message);
            resultResource.Success.Should().Be(this.processResult.Success);
            resultResource.Links.Should().HaveCount(2);
            resultResource.Links.First(l => l.Rel == "requisition").Href.Should().Be("/logistics/requisitions/23");
            resultResource.Links.First(l => l.Rel == "req-moves").Href.Should().Be("/logistics/requisitions/23/moves");
        }
    }
}
