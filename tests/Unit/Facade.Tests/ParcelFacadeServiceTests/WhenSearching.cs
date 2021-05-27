namespace Linn.Stores.Facade.Tests.ParcelFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        private IResult<IEnumerable<Parcel>> results;

        [SetUp]
        public void SetUp()
        {
            var parcels = new List<Parcel>
                              {
                                  new Parcel
                                      {
                                          ParcelNumber = 46287,
                                          DateCreated = new DateTime(2019, 09, 29),
                                          DateReceived = new DateTime(2019, 10, 13),
                                          Weight = 0,
                                          CheckedById = 33067,
                                          ConsignmentNo = "numma 2",
                                          CartonCount = 12,
                                          PalletCount = 2,
                                          CarrierId = 121,
                                          SupplierId = 77,
                                          SupplierInvoiceNo = "invoice no 33 & 1/3 potato",
                                          ImportBookNo = 2222
                                      },
                                  new Parcel
                                      {
                                          ParcelNumber = 2,
                                          DateCreated = new DateTime(2020, 02, 29),
                                          DateReceived = new DateTime(2020, 03, 02),
                                          Weight = 23.33m,
                                          CheckedById = 123,
                                          ConsignmentNo = "cosnginment 1",
                                          CartonCount = 1,
                                          PalletCount = 1,
                                          CarrierId = 111,
                                          SupplierId = 2222,
                                          SupplierInvoiceNo = "invoice no potato",
                                          ImportBookNo = 2232
                                      }
                              }.AsQueryable();

            this.ParcelRepository.FilterBy(Arg.Any<Expression<Func<Parcel, bool>>>()).Returns(parcels);

            this.results = this.Sut.FilterBy(new ParcelSearchRequestResource { SupplierInvNoSearchTerm = "potato" });
        }

        [Test]
        public void ShouldSearch()
        {
            this.ParcelRepository.Received().FilterBy(Arg.Any<Expression<Func<Parcel, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<Parcel>>>();
            var dataResult = ((SuccessResult<IEnumerable<Parcel>>)this.results).Data.ToList();
            dataResult.FirstOrDefault(x => x.ParcelNumber == 46287).Should().NotBeNull();
            dataResult.FirstOrDefault(x => x.ParcelNumber == 2).Should().NotBeNull();
            dataResult.Count.Should().Be(2);
        }
    }
}
