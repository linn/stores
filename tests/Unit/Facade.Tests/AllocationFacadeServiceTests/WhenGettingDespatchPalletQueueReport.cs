namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDespatchPalletQueueReport : ContextBase
    {
        private DespatchPalletQueueResult resultsModel;

        private IResult<DespatchPalletQueueResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resultsModel = new DespatchPalletQueueResult { NumberOfPalletsToMove = 10 };

            this.AllocationReportsService.DespatchPalletQueue()
                .Returns(this.resultsModel);

            this.result = this.Sut.DespatchPalletQueueReport();
        }

        [Test]
        public void ShouldCallService()
        {
            this.AllocationReportsService.Received().DespatchPalletQueue();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<DespatchPalletQueueResult>>();
            var dataResult = ((SuccessResult<DespatchPalletQueueResult>)this.result).Data;
            dataResult.NumberOfPalletsToMove.Should().Be(10);
        }
    }
}
