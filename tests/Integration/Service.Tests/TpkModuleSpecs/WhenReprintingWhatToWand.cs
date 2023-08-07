namespace Linn.Stores.Service.Tests.TpkModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources.Tpk;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenReprintingWhatToWand : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var data = new WhatToWandConsignment 
                           { 
                               Consignment = new Consignment
                                                 {
                                                     ConsignmentId = 123,
                                                 },
                               Lines = new List<WhatToWandLine>(),
                               Account = new SalesAccount(),
                               Type = "REPRINT"
                           };

            this.TpkFacadeService.ReprintWhatToWand(123)
                .Returns(new SuccessResult<WhatToWandConsignment>(data));

            this.Response = this.Browser.Get(
                "/logistics/tpk/what-to-wand-reprint/123",
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
            this.TpkFacadeService.Received().ReprintWhatToWand(123);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<WhatToWandConsignmentResource>();
            resource.Type.Should().Be("REPRINT");
        }
    }
}
