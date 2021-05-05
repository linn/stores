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
 

    public class WhenGettingAll : ContextBase
    {
        ParcelSearchRequestResource requestResource;
        [SetUp]
        public void SetUp()
        {
            var parcels = new List<Parcel>
                              {
                                  new Parcel
                                      {
                                          ParcelNumber = 1,
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
                                      },
                                  new Parcel
                                      {
                                          ParcelNumber = 21,
                                          SupplierId = 3,
                                          DateCreated = new DateTime(),
                                          CarrierId = 4,
                                          SupplierInvoiceNo = "swift, t swift",
                                          ConsignmentNo = "222",
                                          CartonCount = 15,
                                          PalletCount = 1,
                                          Weight = (decimal)02.20,
                                          DateReceived = new DateTime(),
                                          CheckedById = 123456,
                                          Comments = "sent"
                                      }
                                    };

            this.requestResource = new ParcelSearchRequestResource();

            this.ParcelsFacadeService.Search(Arg.Any<ParcelSearchRequestResource>())
                .Returns(new SuccessResult<IEnumerable<Parcel>>(parcels));

            this.Response = this.Browser.Get(
                "/logistics/parcels",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", String.Empty);
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
            this.ParcelsFacadeService.Received().Search(Arg.Any<ParcelSearchRequestResource>());
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ParcelResource>>();
            resources.Count(x => x.ParcelNumber == 21).Should().Be(1);
            resources.Count(x => x.ParcelNumber == 1).Should().Be(1);
            resources.Count().Should().Be(2);
        }
    }
}
