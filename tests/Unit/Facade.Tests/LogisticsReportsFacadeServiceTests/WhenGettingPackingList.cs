namespace Linn.Stores.Facade.Tests.LogisticsReportsFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPackingList : ContextBase
    {
        private int consignmentId;

        private IResult<PackingList> result;

        [SetUp]
        public void SetUp()
        {
            this.consignmentId = 123;
            this.ConsignmentService.GetPackingList(this.consignmentId)
                .Returns(new PackingList { ConsignmentId = this.consignmentId });
            this.result = this.Sut.GetPackingList(this.consignmentId);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ConsignmentService.Received().GetPackingList(this.consignmentId);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<PackingList>>();
            var dataResult = ((SuccessResult<PackingList>)this.result).Data;
            dataResult.ConsignmentId.Should().Be(this.consignmentId);
        }
    }
}
