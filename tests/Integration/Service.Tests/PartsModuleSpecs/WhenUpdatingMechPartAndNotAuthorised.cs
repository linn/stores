namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingMechPartSourceAndUnauthorised : ContextBase
    {
        private MechPartSourceResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new MechPartSourceResource
            {
                Id = 1,
                DateEntered = DateTime.Today.ToString("o"),
                Part = new PartResource
                {
                    PartNumber = "PART",
                    Id = 1,
                    Description = "Desc",
                    StockControlled = true,
                    CreatedBy = 1
                },
            };

            var p = new MechPartSource
            {
                Id = 1,
                Part = new Part
                {
                    Id = 1,
                    StockControlled = "Y",
                    CreatedBy = new Employee { Id = 1 }
                },
                DateEntered = DateTime.Today
            };
            this.MechPartSourceService.Update(1, Arg.Any<MechPartSourceResource>())
                .Returns(new SuccessResult<MechPartSource>(p));
            this.AuthService.
                HasPermissionFor(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(false);
            this.Response = this.Browser.Put(
                "inventory/parts/sources/1",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
