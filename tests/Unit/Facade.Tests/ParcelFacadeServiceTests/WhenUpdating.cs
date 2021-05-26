namespace Linn.Stores.Facade.Tests.ParcelFacadeServiceTests
{
    using System;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private ParcelResource resource;

        private IResult<Parcel> result;

        [SetUp]
        public void SetUp()
        {
            var parcel = new Parcel
            {
                ParcelNumber = 46287,
                DateCreated = new DateTime(2019, 09, 29),
                DateReceived = new DateTime(2019, 10, 13),
                Weight = 0,
                CheckedById = 33067,
                ConsignmentNo = "numma 1"
            };

            this.resource = new ParcelResource
            {
                ParcelNumber = 46287,
                DateCreated = "2019-09-30T00:00:00.0000000",
                DateReceived = "2019-10-14T00:00:00.0000000",
                Weight = (decimal)0.12,
                CheckedById = 33066,
                ConsignmentNo = "numma 2",
                CartonCount = 12,
                PalletCount = 2,
                CarrierId = 121,
                SupplierId = 77,
                SupplierInvoiceNo = "invoice no 33 & 1/3",
                ImportBookNo = 2222
            };

            this.result = this.Sut.Add(this.resource);

            this.ParcelRepository.FindById(46287).Returns(parcel);

            this.result = this.Sut.Update(this.resource.ParcelNumber, this.resource);
        }

        [Test]
        public void ShouldGet()
        {
            this.ParcelRepository.Received().FindById(46287);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<Parcel>>();
            var dataResult = ((SuccessResult<Parcel>)this.result).Data;
            dataResult.ParcelNumber.Should().Be(46287);

            dataResult.DateCreated.Should().Be(new DateTime(2019, 09, 30));
            dataResult.DateReceived.Should().Be(new DateTime(2019, 10, 14));
            dataResult.Weight.Should().Be((decimal)0.12);
            dataResult.CheckedById.Should().Be(33066);
            dataResult.ConsignmentNo.Should().Be("numma 2");
            dataResult.CartonCount.Should().Be(12);
            dataResult.PalletCount.Should().Be(2);
            dataResult.CarrierId.Should().Be(121);
            dataResult.SupplierId.Should().Be(77);
            dataResult.SupplierInvoiceNo.Should().Be("invoice no 33 & 1/3");
            dataResult.ImportBookNo.Should().Be(2222);
        }
    }
}
