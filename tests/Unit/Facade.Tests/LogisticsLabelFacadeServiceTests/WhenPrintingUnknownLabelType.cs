namespace Linn.Stores.Facade.Tests.LogisticsLabelFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using NUnit.Framework;

    public class WhenPrintingUnknownLabelType : ContextBase
    {
        private LogisticsLabelRequestResource resource;

        private IResult<ProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new LogisticsLabelRequestResource
                                {
                                    LabelType = "Does Not Exist"
                                };

            this.result = this.Sut.PrintLabel(this.resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<ProcessResult>>();
            var dataResult = (BadRequestResult<ProcessResult>)this.result;
            dataResult.Message.Should().Be("Cannot print label type Does Not Exist");
        }
    }
}
