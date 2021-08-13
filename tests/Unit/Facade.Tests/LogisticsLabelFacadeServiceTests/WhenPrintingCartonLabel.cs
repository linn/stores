namespace Linn.Stores.Facade.Tests.LogisticsLabelFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingCartonLabel : ContextBase
    {
        private LogisticsLabelRequestResource resource;

        private ProcessResult serviceResult;

        private IResult<ProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new LogisticsLabelRequestResource
                                {
                                    ConsignmentId = 880, FirstItem = 123, LastItem = 456, LabelType = "Carton", UserNumber = 123
                                };
            this.serviceResult = new ProcessResult(true, "ok");
            this.LogisticsLabelService.PrintCartonLabel(
                    this.resource.ConsignmentId,
                    this.resource.FirstItem,
                    this.resource.LastItem, 
                    this.resource.UserNumber)
                .Returns(this.serviceResult);

            this.result = this.Sut.PrintLabel(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.LogisticsLabelService.Received().PrintCartonLabel(
                this.resource.ConsignmentId,
                this.resource.FirstItem,
                this.resource.LastItem,
                this.resource.UserNumber);
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
