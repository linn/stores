﻿namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingCartonTypes : ContextBase
    {
        private CartonType cartonType1;

        private CartonType cartonType2;

        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            this.cartonType1 = new CartonType { CartonTypeName = "C1" };
            this.cartonType2 = new CartonType { CartonTypeName = "C2" };
            this.searchTerm = "C";

            this.CartonTypeFacadeService.Search(this.searchTerm)
                .Returns(new SuccessResult<IEnumerable<CartonType>>(new List<CartonType>
                                                                        {
                                                                            this.cartonType1, this.cartonType2
                                                                        }));

            this.Response = this.Browser.Get(
                "/logistics/carton-types",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.searchTerm);
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
            this.CartonTypeFacadeService.Received().Search(this.searchTerm);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<CartonTypeResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.Should().Contain(a => a.CartonTypeName == "C1");
            resultResource.Should().Contain(a => a.CartonTypeName == "C2");
        }
    }
}
