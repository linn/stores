namespace Linn.Stores.Facade.Tests.LogisticsProcessesFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingPalletLabel : ContextBase
    {
        private LogisticsLabelRequestResource resource;

        private ProcessResult serviceResult;

        private IResult<ProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new LogisticsLabelRequestResource
                                {
                                    ConsignmentId = 880,
                                    FirstItem = 123,
                                    LastItem = 456,
                                    LabelType = "Pallet",
                                    UserNumber = 123,
                                    NumberOfCopies = 2
                                };
            this.serviceResult = new ProcessResult(true, "ok");
            this.LogisticsLabelService.PrintPalletLabel(
                    this.resource.ConsignmentId,
                    this.resource.FirstItem,
                    this.resource.LastItem, 
                    this.resource.UserNumber,
                    this.resource.NumberOfCopies)
                .Returns(this.serviceResult);

            this.result = this.Sut.PrintLabel(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LogisticsLabelService.Received().PrintPalletLabel(
                this.resource.ConsignmentId,
                this.resource.FirstItem,
                this.resource.LastItem,
                this.resource.UserNumber,
                this.resource.NumberOfCopies);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ProcessResult>>();
            var dataResult = ((SuccessResult<ProcessResult>)this.result).Data;
            dataResult.Message.Should().Be("ok");
            dataResult.Success.Should().BeTrue();
        }
    }
}
