﻿namespace Linn.Stores.Service.Tests.ParcelsModuleSpecs
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
                                  DateCreated = new DateTime(),
                                  CarrierId = 4,
                                  SupplierInvoiceNo = "7127",
                                  ConsignmentNo = "222",
                                  CartonCount = 15,
                                  PalletCount = 1,
                                  Weight = 02.20m,
                                  DateReceived = new DateTime(),
                                  CheckedById = 123456,
                                  Comments = "sent"
                              },
                              new Parcel
                              {
                                  ParcelNumber = 223,
                                  SupplierId = 3,
                                  DateCreated = new DateTime(),
                                  CarrierId = 4,
                                  SupplierInvoiceNo = "1291",
                                  ConsignmentNo = "222",
                                  CartonCount = 15,
                                  PalletCount = 1,
                                  Weight = 02.40m,
                                  DateReceived = new DateTime(),
                                  CheckedById = 123456,
                                  Comments = "sentttt"
                              }
                          };

            this.ParcelsFacadeService.Search(Arg.Any<string>())
             .Returns(new SuccessResult<IEnumerable<Parcel>>(parcels));

            this.Response = this.Browser.Get(
                "/logistics/parcels-by-number",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", "22");
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
            this.ParcelsFacadeService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ParcelResource>>();
            resources.Count().Should().Be(2);
        }
    }
}
