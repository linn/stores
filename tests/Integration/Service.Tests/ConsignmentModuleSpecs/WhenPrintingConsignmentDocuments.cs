namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using System.Threading.Tasks;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingConsignmentDocuments : ContextBase
    {
        private ConsignmentRequestResource requestResource;

        [SetUp]
        public async Task SetUp()
        {
            this.requestResource = new ConsignmentRequestResource
            {
                ConsignmentId = 1,
                UserNumber = 32198
            };

            this.LogisticsProcessesFacadeService.PrintConsignmentDocuments(Arg.Any<ConsignmentRequestResource>())
                .Returns(Task.FromResult<IResult<ProcessResult>>(new SuccessResult<ProcessResult>(new ProcessResult(true, "ok"))));

            this.Response = await this.Browser.Post(
                "/logistics/print-consignment-documents",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                });
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LogisticsProcessesFacadeService.Received().PrintConsignmentDocuments(
                Arg.Is<ConsignmentRequestResource>(
                    r => r.ConsignmentId == this.requestResource.ConsignmentId
                         && r.UserNumber == this.requestResource.UserNumber));
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
