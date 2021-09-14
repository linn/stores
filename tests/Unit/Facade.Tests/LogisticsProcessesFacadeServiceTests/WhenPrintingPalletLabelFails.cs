namespace Linn.Stores.Facade.Tests.LogisticsProcessesFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    public class WhenPrintingPalletLabelFails : ContextBase
    {
        private LogisticsLabelRequestResource resource;

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
            this.LogisticsLabelService.PrintPalletLabel(
                    this.resource.ConsignmentId,
                    this.resource.FirstItem,
                    this.resource.LastItem, 
                    this.resource.UserNumber,
                    this.resource.NumberOfCopies)
                .Throws(new ProcessException("Failed to print pallet label"));

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
            this.result.Should().BeOfType<BadRequestResult<ProcessResult>>();
            var dataResult = (BadRequestResult<ProcessResult>)this.result;
            dataResult.Message.Should().Be("Failed to print pallet label");
        }
    }
}
