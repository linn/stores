namespace Linn.Stores.Service.Tests.DepartmentsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingNominalForDepartment : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var n = new Nominal { NominalCode = "000123" };
            this.NominalService.GetNominalForDepartment("000123").Returns(
                new SuccessResult<Nominal>(n));

            this.Response = this.Browser.Get(
                "/inventory/nominal-for-department/000123",
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
            this.NominalService.Received().GetNominalForDepartment("000123");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<NominalResource>();
            resource.NominalCode.Should().Be("000123");
        }
    }
}