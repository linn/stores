namespace Linn.Stores.Service.Tests.ParcelsModuleSpecs
{
    using System;
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

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var parcel = new Parcel
            {
                ParcelNumber = 1,
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
                Comments = "RSN 212, RSN 118",
                ImportNoVax = "19", //todo check if this should be int
                ImpbookId = 2121978

            };

            this.ParcelsService.GetById(Arg.Any<int>())
                .Returns(new SuccessResult<Parcel>(parcel));


            this.Response = this.Browser.Get(
                "/logistics/parcels/1",
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
            this.ParcelsService.Received().GetById(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ParcelResource>();
            resource.ParcelNumber.Should().Be(1);
            resource.SupplierId.Should().Be(2);
            resource.SupplierCountry.Should().Be("UK");
            resource.SupplierName.Should().Be("bathroom cabinet company");
            resource.CarrierId.Should().Be(4);
            resource.ParcelNumber.Should().Be(1);
            resource.ImpbookId.Should().Be(2121978);
        }
    }
}
