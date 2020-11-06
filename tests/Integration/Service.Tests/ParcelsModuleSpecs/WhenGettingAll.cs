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

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var parcels = new List<Parcel>
                              {
                                  new Parcel
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
                                        Comments = "RSN 212, RSN 118"
                                      },
                                  new Parcel
                                      {
                                          ParcelNumber = 22,
                                          SupplierId = 3,
                                          SupplierName = "electric things company",
                                          SupplierCountry = "UK",
                                          DateCreated = new DateTime(),
                                          CarrierId = 4,
                                          CarrierName = "DHL",
                                          SupplierInvoiceNo = "swift, t swift",
                                          ConsignmentNo = 222,
                                          CartonCount = 15,
                                          PalletCount = 1,
                                          Weight = (decimal)02.20,
                                          DateReceived = new DateTime(),
                                          CheckedById = 123456,
                                          CheckedByName = "partridge alan",
                                          Comments = "sent"
                                      }
                                    };

            this.ParcelsService.GetAll() 
                .Returns(new SuccessResult<IEnumerable<Parcel>>(parcels));

            this.Response = this.Browser.Get(
                "/logistics/parcels",
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
            this.ParcelsService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ParcelResource>>();
            resources.Count(x => x.ParcelNumber == 22).Should().Be(1);
            resources.Count(x => x.ParcelNumber == 1).Should().Be(1);
            resources.Count().Should().Be(2);
        }
    }
}
