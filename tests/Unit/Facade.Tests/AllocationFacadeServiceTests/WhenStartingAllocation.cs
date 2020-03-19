namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocation : ContextBase
    {
        private AllocationOptionsResource resource;

        private AllocationStart startDetails;

        private IResult<AllocationStart> result;

        [SetUp]
        public void SetUp()
        {
            this.startDetails = new AllocationStart { Id = 234 };
            this.resource = new AllocationOptionsResource
            {
                StockPool = "LINN"
            };

            this.AllocationService.StartAllocation(Arg.Any<string>()).Returns(this.startDetails);

            this.result = this.Sut.StartAllocation(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.AllocationService.Received().StartAllocation(this.resource.StockPool);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<AllocationStart>>();
            var dataResult = ((SuccessResult<AllocationStart>)this.result).Data;
            dataResult.Id.Should().Be(this.startDetails.Id);
        }
    }
}