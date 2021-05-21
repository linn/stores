namespace Linn.Stores.Facade.Tests.ParcelFacadeServiceTests
{
    using System;
    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenAdding : ContextBase
    {
        private ParcelResource resource;

        private IResult<Parcel> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ParcelResource
            {
                DateCreated = "2019-09-29T00:00:00.0000000",
                DateReceived = "2019-10-13T00:00:00.0000000",
                Weight = 0,
                CheckedById = 33067,
                ConsignmentNo = "numma 1"
            };

            this.DatabaseService.GetNextVal("PARCEL_SEQ").Returns(12798);
            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAdd()
        {
            this.ParcelRepository.Received().Add(Arg.Any<Parcel>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<CreatedResult<Parcel>>();
            var dataResult = ((CreatedResult<Parcel>)this.result).Data;
            dataResult.ParcelNumber.Should().Be(12798);
            dataResult.DateCreated.Should().Be(new DateTime(2019, 09, 29));
            dataResult.DateReceived.Should().Be(new DateTime(2019, 10, 13));
            dataResult.Weight.Should().Be(0);
            dataResult.CheckedById.Should().Be(33067);
        }
    }
}
