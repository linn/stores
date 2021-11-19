namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

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
                    StockControlled = "Y",
                    CreatedBy = 1
                },
            };

            this.MechPartSourceService
                .Update(Arg.Any<int>(), Arg.Any<MechPartSourceResource>())
                    .Throws(_ => new UpdatePartException("You are not authorised"));
            
            this.Response = this.Browser.Put(
                "/parts/sources/1",
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
