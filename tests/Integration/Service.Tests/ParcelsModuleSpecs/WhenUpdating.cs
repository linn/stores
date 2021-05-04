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

    public class WhenUpdating : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var parcel = new Parcel
            {
                ParcelNumber = 4,
                SupplierId = 2,
                DateCreated = new DateTime(),
                CarrierId = 4,
                SupplierInvoiceNo = "Bond, James Bond",
                ConsignmentNo = "007",
                CartonCount = 0,
                PalletCount = 0,
                Weight = (decimal)00.70,
                DateReceived = new DateTime(),
                CheckedById = 123456,
                Comments = "RSN 212, RSN 118"
            };

            var parcelResource = new ParcelResource
                             {
                                 ParcelNumber = 4,
                                 SupplierId = 2,
                                 DateCreated = new DateTime().ToString("o"),
                                 CarrierId = 4,
                                 SupplierInvoiceNo = "Bond, James Bond",
                                 ConsignmentNo = "007",
                                 CartonCount = 0,
                                 PalletCount = 0,
                                 Weight = (decimal)00.70,
                                 DateReceived = new DateTime().ToString("o"),
                                 CheckedById = 123456,
                                 Comments = "RSN 212, RSN 118"
                             };

            this.ParcelsFacadeService.Update(Arg.Any<int>(), Arg.Any<ParcelResource>()) 
                .Returns(new SuccessResult<Parcel>(parcel));


            this.Response = this.Browser.Put(
                "/inventory/parcels/4",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(parcelResource);
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
            this.ParcelsFacadeService.Received().Update(4, Arg.Any<ParcelResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ParcelResource>();
            resource.ParcelNumber.Should().Be(4);
            resource.SupplierId.Should().Be(2);
            resource.CarrierId.Should().Be(4);
        }
    }
}
