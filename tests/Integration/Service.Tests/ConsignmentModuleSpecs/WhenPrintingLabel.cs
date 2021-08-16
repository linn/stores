namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingLabel : ContextBase
    {
        private LogisticsLabelRequestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new LogisticsLabelRequestResource
                                       {
                                           ConsignmentId = 1,
                                           FirstItem = 1,
                                           LastItem = 2,
                                           LabelType = "Carton"
                                       };

            this.LogisticsLabelFacade.PrintLabel(Arg.Any<LogisticsLabelRequestResource>())
                .Returns(new SuccessResult<ProcessResult>(new ProcessResult(true, "ok")));

            this.Response = this.Browser.Post(
                $"/logistics/labels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
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
            this.LogisticsLabelFacade.Received().PrintLabel(
                Arg.Is<LogisticsLabelRequestResource>(
                    r => r.LabelType == this.requestResource.LabelType
                         && r.ConsignmentId == this.requestResource.ConsignmentId
                         && r.FirstItem == this.requestResource.FirstItem
                         && r.LastItem == this.requestResource.LastItem));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProcessResult>();
            resource.Success.Should().BeTrue();
            resource.Message.Should().Be("ok");
        }
    }
}
