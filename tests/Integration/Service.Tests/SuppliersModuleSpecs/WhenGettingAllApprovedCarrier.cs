namespace Linn.Stores.Service.Tests.SuppliersModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAllApprovedCarrier : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var supplierA = new Supplier { Id = 1, Name = "Supplier A", CountryCode = "DE", ApprovedCarrier = "Y" };
            var supplierB = new Supplier { Id = 2, Name = "Supplier B", CountryCode = "RU", ApprovedCarrier = "Y" };

            this.SuppliersService.GetSuppliers(Arg.Any<string>(), false, true).Returns(
                new SuccessResult<IEnumerable<Supplier>>(new List<Supplier> { supplierA, supplierB }));

            this.Response = this.Browser.Get(
                "/inventory/suppliers-approved-carrier",
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
            this.SuppliersService.Received().GetSuppliers(Arg.Any<string>(), false, true);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<SupplierResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Name == "Supplier A");
            resource.Should().Contain(a => a.Name == "Supplier B");
        }
    }
}
