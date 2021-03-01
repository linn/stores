namespace Linn.Stores.Service.Tests.RsnModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingExportReturn : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var rsns = new List<int> { 123, 456 };

            var requestResource = new MakeExportReturnRequestResource { HubReturn = true, Rsns = rsns };

            this.ExportRsnService.MakeExportReturn(Arg.Any<IEnumerable<int>>(), true).Returns(
                new SuccessResult<MakeExportReturnResult>(new MakeExportReturnResult { ExportReturnId = 1111 }));

            this.Response = this.Browser.Post(
                "/inventory/exports/make-export-return",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldMakeExportReturn()
        {
            this.ExportRsnService.Received().MakeExportReturn(Arg.Any<IEnumerable<int>>(), true);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<MakeExportReturnResource>();
            resource.ExportReturnId.Should().Be(1111);
        }
    }
}