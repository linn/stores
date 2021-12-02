namespace Linn.Stores.Domain.LinnApps.Tests.RequisitionServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnAllocating : ContextBase
    {
        private ProcessResult storesPackResult;

        private int reqNumber;

        private int? reqLine;

        private int userNumber;

        private RequisitionActionResult result;

        private RequisitionHeader req;

        [SetUp]
        public void SetUp()
        {
            this.reqNumber = 4343;
            this.reqLine = 9;
            this.userNumber = 13;
            this.req = new RequisitionHeader { ReqNumber = this.reqNumber, Document1 = 808 };
            this.storesPackResult = new ProcessResult { Message = "ok", Success = true };
            this.StoresPack.UnallocateRequisition(this.reqNumber, this.reqLine, this.userNumber)
                .Returns(this.storesPackResult);
            this.RequistionHeaderRepository.FindById(this.reqNumber).Returns(this.req);

            this.result = this.Sut.Unallocate(this.reqNumber, this.reqLine, this.userNumber);
        }

        [Test]
        public void ShouldCallProxy()
        {
            this.StoresPack.Received().UnallocateRequisition(this.reqNumber, this.reqLine, this.userNumber);
        }

        [Test]
        public void ShouldGetReq()
        {
            this.RequistionHeaderRepository.Received().FindById(this.reqNumber);
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be(this.storesPackResult.Message);
            this.result.Success.Should().BeTrue();
            this.result.RequisitionHeader.ReqNumber.Should().Be(this.reqNumber);
        }
    }
}
