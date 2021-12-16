namespace Linn.Stores.Facade.Tests.TpkFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnpickingStockAndFailure : ContextBase
    {
        private IResult<ProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.DomainService.UnpickStock(1, 2, 3, 4, 5, 6, 7).Returns(new ProcessResult(false, "Errors, errors and more errors."));

            this.result = this.Sut.UnpickStock(
                new UnpickStockRequestResource
                    {
                        ReqNumber = 1,
                        LineNumber = 2,
                        OrderNumber = 3,
                        OrderLine = 4,
                        AmendedBy = 5,
                        PalletNumber = 6,
                        LocationId = 7
                    });
        }

        [Test]
        public void ShouldCallDomainService()
        {
            this.DomainService.Received().UnpickStock(1, 2, 3, 4, 5, 6, 7);
        }

        [Test]
        public void ShouldNotCommitTransaction()
        {
            this.TransactionManager.DidNotReceive().Commit();
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            ((SuccessResult<ProcessResult>)this.result).Data.Success.Should().BeFalse();
            ((SuccessResult<ProcessResult>)this.result).Data.Message.Should().Be("Errors, errors and more errors.");
        }
    }
}
