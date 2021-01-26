namespace Linn.Stores.Service.Tests.ParcelsModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var parcels = new List<Parcel>
                              {
                                  new Parcel
                                      {
                                          ParcelNumber = 22,
                                          SupplierId = 3,
                                          SupplierName = "electric things company",
                                          SupplierCountry = "UK",
                                          DateCreated = new DateTime(),
                                          CarrierId = 4,
                                          CarrierName = "DHL",
                                          SupplierInvoiceNo = "7127",
                                          ConsignmentNo = 222,
                                          CartonCount = 15,
                                          PalletCount = 1,
                                          Weight = (decimal)02.20,
                                          DateReceived = new DateTime(),
                                          CheckedById = 123456,
                                          CheckedByName = "partridge alan",
                                          Comments = "sent"
                                      },
                                  new Parcel
                                      {
                                          ParcelNumber = 223,
                                          SupplierId = 3,
                                          SupplierName = "things company",
                                          SupplierCountry = "UK",
                                          DateCreated = new DateTime(),
                                          CarrierId = 4,
                                          CarrierName = "DHL",
                                          SupplierInvoiceNo = "1291",
                                          ConsignmentNo = 222,
                                          CartonCount = 15,
                                          PalletCount = 1,
                                          Weight = (decimal)02.40,
                                          DateReceived = new DateTime(),
                                          CheckedById = 123456,
                                          CheckedByName = "partridge jim",
                                          Comments = "sentttt"
                                      }
                                    };

            this.ParcelsService.Search(Arg.Any<string>()) 
                .Returns(new SuccessResult<IEnumerable<Parcel>>(parcels));

            var searchRequestResource = new SearchRequestResource { SearchTerm = "22" };

            this.Response = this.Browser.Get(
                "/logistics/parcels",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(searchRequestResource);
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
            this.ParcelsService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ParcelResource>>();
            resources.Count().Should().Be(2);
        }
    }
}
