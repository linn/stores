namespace Linn.Stores.Facade.Tests.RequisitionActionsFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnAllocatingWithoutUserNumber : ContextBase
    {
        private int reqNumber;

        private int? reqLine;

        private IResult<RequisitionActionResult> result;

        private int? userNumber;

        [SetUp]
        public void SetUp()
        {
            this.reqNumber = 24352345;
            this.reqLine = 3;
            this.userNumber = null;

            this.result = this.Sut.Unallocate(this.reqNumber, this.reqLine, this.userNumber);
        }

        [Test]
        public void ShouldNotCallService()
        {
            this.RequisitionService.DidNotReceive().Unallocate(this.reqNumber, this.reqLine, Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<BadRequestResult<RequisitionActionResult>>();
            var dataResult = (BadRequestResult<RequisitionActionResult>)this.result;
            dataResult.Message.Should().Be("You must supply a user number");
        }
    }
}
