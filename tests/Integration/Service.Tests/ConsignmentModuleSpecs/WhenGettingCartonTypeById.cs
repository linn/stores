namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingCartonTypeById : ContextBase
    {
        private CartonType cartonType;

        private string cartonTypeName;

        private string cartonTypeDescription;

        [SetUp]
        public void SetUp()
        {
            this.cartonTypeName = "BigBox";
            this.cartonTypeDescription = "It is a big box";
            this.cartonType = new CartonType { CartonTypeName = this.cartonTypeName, Description = this.cartonTypeDescription };

            this.CartonTypeFacadeService.GetById(this.cartonTypeName)
                .Returns(new SuccessResult<CartonType>(this.cartonType));

            this.Response = this.Browser.Get(
                $"/logistics/carton-types/{this.cartonTypeName}",
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
            this.CartonTypeFacadeService.Received().GetById(this.cartonTypeName);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<CartonTypeResource>();
            resultResource.CartonTypeName.Should().Be(this.cartonTypeName);
            resultResource.Description.Should().Be(this.cartonTypeDescription);
        }
    }
}
