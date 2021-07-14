namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPorts : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var ports = new List<Port>
                            {
                                new Port { PortCode = "GLA", Description = "Glasscow" },
                                new Port { PortCode = "LDN", Description = "Landan" },
                            };

            this.PortFacadeService.GetAll().Returns(new SuccessResult<IEnumerable<Port>>(ports));

            this.Response = this.Browser.Get(
                "/logistics/import-books/ports",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PortFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PortResource>>();
            resource.Count().Should().Be(2);
            resource.FirstOrDefault(x => x.PortCode == "GLA" && x.Description == "Glasscow").Should().NotBeNull();
            resource.FirstOrDefault(x => x.PortCode == "LDN" && x.Description == "Landan").Should().NotBeNull();
        }
    }
}
