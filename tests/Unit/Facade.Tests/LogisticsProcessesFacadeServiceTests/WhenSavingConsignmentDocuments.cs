namespace Linn.Stores.Facade.Tests.LogisticsProcessesFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSavingConsignmentDocuments : ContextBase
    {
        private ConsignmentRequestResource resource;

        private ProcessResult serviceResult;

        private IResult<ProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ConsignmentRequestResource
                                {
                                    ConsignmentId = 880,
                                    UserNumber = 123
                                };
            this.serviceResult = new ProcessResult(true, "ok");
            this.ConsignmentService.SaveConsignmentDocuments(this.resource.ConsignmentId)
                .Returns(this.serviceResult);

            this.result = this.Sut.SaveConsignmentDocuments(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ConsignmentService.Received().SaveConsignmentDocuments(this.resource.ConsignmentId);
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
