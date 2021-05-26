namespace Linn.Stores.Service.Tests.ParcelsModuleSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var parcel = new Parcel
                         {
                             ParcelNumber = 1,
                             SupplierId = 2,
                             DateCreated = new DateTime(),
                             CarrierId = 4,
                             SupplierInvoiceNo = "Bond, James Bond",
                             ConsignmentNo = "007",
                             CartonCount = 0,
                             PalletCount = 0,
                             Weight = 00.70m,
                             DateReceived = new DateTime(),
                             CheckedById = 123456,
                             Comments = "RSN 212, RSN 118"
                         };

            this.ParcelsFacadeService.GetById(Arg.Any<int>()).Returns(new SuccessResult<Parcel>(parcel));

            this.Response = this.Browser.Get(
                "/logistics/parcels/1",
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
            this.ParcelsFacadeService.Received().GetById(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ParcelResource>();
            resource.ParcelNumber.Should().Be(1);
            resource.SupplierId.Should().Be(2);
            resource.CarrierId.Should().Be(4);
        }
    }
}
