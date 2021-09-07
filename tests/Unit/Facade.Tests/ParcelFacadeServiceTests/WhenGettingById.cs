
namespace Linn.Stores.Facade.Tests.ParcelFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        private int parcelNo = 46287;

        private IResult<Parcel> result;

        [SetUp]
        public void SetUp()
        {
            var parcel = new Parcel
                             {
                                 ParcelNumber = this.parcelNo,
                                 DateCreated = new DateTime(2019, 09, 29),
                                 DateReceived = new DateTime(2019, 10, 13),
                                 Weight = 0,
                                 CheckedById = 33067,
                                 ConsignmentNo = "numma 2",
                                 CartonCount = 12,
                                 PalletCount = 2,
                                 CarrierId = 121,
                                 SupplierId = 77,
                                 SupplierInvoiceNo = "RSN 4618989",
                                 Impbooks = new List<ImportBook>
                                                {
                                                    new ImportBook { Id = 11111, ParcelNumber = this.parcelNo },
                                                    new ImportBook { Id = 22222, ParcelNumber = this.parcelNo }
                                                }
                             };

            this.ParcelRepository.FindById(Arg.Any<int>()).Returns(parcel);

            this.result = this.Sut.GetById(this.parcelNo);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<Parcel>>();

            var dataResult = ((SuccessResult<Parcel>)this.result).Data;
            dataResult.ParcelNumber.Should().Be(this.parcelNo);
            dataResult.Impbooks.FirstOrDefault(x => x.Id == 11111).Should().NotBeNull();
            dataResult.Impbooks.FirstOrDefault(x => x.Id == 22222).Should().NotBeNull();
        }

        [Test]
        public void ShouldSearch()
        {
            this.ParcelRepository.Received().FindById(Arg.Any<int>());
        }
    }
}
