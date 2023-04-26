namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Wcs;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingWarehouseTaskWithInvalidPallet : ContextBase
    {
        private IResult<MessageResult> result;

        [SetUp]
        public void SetUp()
        {

            var resource = new WarehouseTaskResource
                               {
                                   PalletNumber = 6666,
                                   OriginalLocation = "B10",
                                   Destination = "S",
                                   EmployeeId = 100,
                                   Priority = 10,
                                   TaskSource = "BK",
                                   TaskType = "MV"
                               };
            this.result = this.Sut.MakeWarehouseTask(resource);
        }

        [Test]
        public void ShouldntAddWarehouseTask()
        {
            this.WarehouseService.DidNotReceive().MovePallet(6666, "S", 10, Arg.Any<Employee>());
        }

        [Test]
        public void ShouldReturnBadRequestResult()
        {
            this.result.Should().BeOfType<BadRequestResult<MessageResult>>();
            var message = ((BadRequestResult<MessageResult>)this.result).Message;
            message.Should().Be("Not a valid pallet number");
        }
    }
}
