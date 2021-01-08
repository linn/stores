namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMechMechPartSourceSource : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var p = new MechPartSource
            {
                Id = 1, 
                Part = new Part
                {
                    Id = 1, StockControlled = "Y", CreatedBy = new Employee { Id = 1 }
                }, 
                DateEntered = DateTime.Today
            };
            this.MechPartSourceService.GetById(1).Returns(new SuccessResult<MechPartSource>(p));

            this.Response = this.Browser.Get(
                "inventory/parts/sources/1",
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
            this.MechPartSourceService.Received().GetById(1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<MechPartSourceResource>();
            resource.Id.Should().Be(1);
        }
    }
}
