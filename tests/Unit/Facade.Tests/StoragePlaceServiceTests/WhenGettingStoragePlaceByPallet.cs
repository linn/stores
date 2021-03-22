namespace Linn.Stores.Facade.Tests.StoragePlaceServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStoragePlaceByPallet : ContextBase
    {
        private readonly StoragePlaceRequestResource requestResource =
            new StoragePlaceRequestResource { PalletNumber = 298 };

        private readonly StoragePlace repositoryResult = new StoragePlace { PalletNumber = 298, Name = "P298" };

        private IResult<StoragePlace> result;

        [SetUp]
        public void SetUp()
        {
            this.StoragePlaceRepository.FindBy(Arg.Any<Expression<Func<StoragePlace, bool>>>())
                .Returns(this.repositoryResult);

            this.result = this.Sut.GetStoragePlace(this.requestResource);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.StoragePlaceRepository.Received().FindBy(Arg.Any<Expression<Func<StoragePlace, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.result.Should().BeOfType<SuccessResult<StoragePlace>>();
            ((SuccessResult<StoragePlace>)this.result).Data.Name.Should().Be("P298");
        }
    }
}
