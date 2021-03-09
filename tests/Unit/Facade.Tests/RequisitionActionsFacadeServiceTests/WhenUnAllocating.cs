namespace Linn.Stores.Facade.Tests.RequisitionActionsFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnAllocating : ContextBase
    {
        private int reqNumber;

        private int? reqLine;

        private RequisitionActionResult requisition;

        private IResult<RequisitionActionResult> result;

        private int userNumber;

        [SetUp]
        public void SetUp()
        {
            this.reqNumber = 24352345;
            this.reqLine = 3;
            this.userNumber = 24354;
            this.requisition = new RequisitionActionResult
                                   {
                                       RequisitionHeader = new RequisitionHeader
                                                               {
                                                                   ReqNumber = this.reqNumber,
                                                                   Document1 = 124
                                                               }
                                   };
            this.RequisitionService.Unallocate(this.reqNumber, this.reqLine, this.userNumber)
                .Returns(this.requisition);

            this.result = this.Sut.Unallocate(this.reqNumber, this.reqLine, this.userNumber);
        }

        [Test]
        public void ShouldCallService()
        {
            this.RequisitionService.Received().Unallocate(this.reqNumber, this.reqLine, this.userNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<RequisitionActionResult>>();
            var dataResult = ((SuccessResult<RequisitionActionResult>)this.result).Data;
            dataResult.RequisitionHeader.ReqNumber.Should().Be(this.reqNumber);
            dataResult.RequisitionHeader.Document1.Should().Be(this.requisition.RequisitionHeader.Document1);
        }
    }
}
