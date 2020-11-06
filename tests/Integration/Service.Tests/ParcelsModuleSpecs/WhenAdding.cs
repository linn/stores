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

    public class WhenAdding: ContextBase
    {

        [SetUp]
        public void SetUp()
        {
            var parcel = new Parcel
            {
                ParcelNumber = 4,
                SupplierId = 2,
                SupplierName = "bathroom cabinet company",
                SupplierCountry = "UK",
                DateCreated = new DateTime(),
                CarrierId = 4,
                CarrierName = "DHL",
                SupplierInvoiceNo = "Bond, James Bond",
                ConsignmentNo = 007,
                CartonCount = 0,
                PalletCount = 0,
                Weight = (decimal)00.70,
                DateReceived = new DateTime(),
                CheckedById = 123456,
                CheckedByName = "DJ badboy",
                Comments = "RSN 212, RSN 118"
            };

            var parcelResource = new ParcelResource
                             {
                                 SupplierId = 2,
                                 SupplierName = "bathroom cabinet company",
                                 SupplierCountry = "UK",
                                 DateCreated = new DateTime().ToString("o"),
                                 CarrierId = 4,
                                 CarrierName = "DHL",
                                 SupplierInvoiceNo = "Bond, James Bond",
                                 ConsignmentNo = 007,
                                 CartonCount = 0,
                                 PalletCount = 0,
                                 Weight = (decimal)00.70,
                                 DateReceived = new DateTime().ToString("o"),
                                 CheckedById = 123456,
                                 CheckedByName = "DJ badboy",
                                 Comments = "RSN 212, RSN 118"
                             };

            this.ParcelsService.Add(Arg.Any<ParcelResource>()) 
                .Returns(new SuccessResult<Parcel>(parcel));


            this.Response = this.Browser.Post(
                "/logistics/parcels",
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
            this.ParcelsService.Received().Add(Arg.Any<ParcelResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ParcelResource>();
            resource.ParcelNumber.Should().Be(4);
            resource.SupplierId.Should().Be(2);
            resource.SupplierCountry.Should().Be("UK");
            resource.SupplierName.Should().Be("bathroom cabinet company");
            resource.CarrierId.Should().Be(4);
        }
    }
}
