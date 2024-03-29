﻿namespace Linn.Stores.Service.Tests.PartsModuleSpecs
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

    public class WhenUpdatingMechMechPartSource : ContextBase
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
            this.MechPartSourceService.Update(1, Arg.Any<MechPartSourceResource>()).Returns(new SuccessResult<MechPartSource>(p));
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
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.MechPartSourceService.Received().Update(1, Arg.Any<MechPartSourceResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<MechPartSourceResource>();
            resource.Id.Should().Be(1);
        }
    }
}
