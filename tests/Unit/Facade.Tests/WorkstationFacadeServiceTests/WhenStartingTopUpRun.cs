namespace Linn.Stores.Facade.Tests.WorkstationFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingTopUpRun : ContextBase
    {
        private IResult<ResponseModel<WorkstationTopUpStatus>> result;

        private WorkstationTopUpStatus status;

        private IEnumerable<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.status = new WorkstationTopUpStatus { ProductionTriggerRunJobRef = "B", WorkstationTopUpJobRef = "B" };
            this.privileges = new List<string> { "p1" };
            this.WorkstationService.StartTopUpRun()
                .Returns(this.status);

            this.result = this.Sut.StartTopUpRun(this.privileges);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WorkstationService.Received().StartTopUpRun();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ResponseModel<WorkstationTopUpStatus>>>();
            var dataResult = ((SuccessResult<ResponseModel<WorkstationTopUpStatus>>)this.result).Data;
            dataResult.ResponseData.ProductionTriggerRunJobRef.Should().Be(this.status.ProductionTriggerRunJobRef);
            dataResult.ResponseData.WorkstationTopUpJobRef.Should().Be(this.status.WorkstationTopUpJobRef);
            dataResult.Privileges.Should().HaveCount(1);
            dataResult.Privileges.First().Should().Be("p1");
        }
    }
}
