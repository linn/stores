namespace Linn.Stores.Service.Tests.ParcelsModuleSpecs
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingNotAuthorised : ContextBase
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
                                 Weight = 00.70m,
                                 DateReceived = new DateTime(),
                                 CheckedById = 123456,
                                 Comments = "RSN 212, RSN 118"
                             };

            var parcelResource = new ParcelResource
                                     {
                                         SupplierId = 2,
                                         DateCreated = new DateTime().ToString("o"),
                                         CarrierId = 4,
                                         SupplierInvoiceNo = "Bond, James Bond",
                                         ConsignmentNo = "007",
                                         CartonCount = 0,
                                         PalletCount = 0,
                                         Weight = 00.70m,
                                         DateReceived = new DateTime().ToString("o"),
                                         CheckedById = 123456,
                                         Comments = "RSN 212, RSN 118"
                                     };

            this.AuthorisationService.HasPermissionFor("parcel.admin", Arg.Any<IEnumerable<string>>()).Returns(false);

            this.Response = this.Browser.Post(
                "/logistics/parcels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(parcelResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
