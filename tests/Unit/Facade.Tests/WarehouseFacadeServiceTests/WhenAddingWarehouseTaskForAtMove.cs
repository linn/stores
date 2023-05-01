namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingWarehouseTaskForAtMove : ContextBase
    {
        private IResult<MessageResult> result;

        [SetUp]
        public void SetUp()
        {
            var employee = new Employee { Id = 123, FullName = "Rob Richy" };
            this.EmployeeRepository.FindById(123).Returns(employee);

            this.WarehouseService.AtMovePallet(100, "B10", "S", 10, Arg.Any<Employee>()).Returns(true);

            var resource = new WarehouseTaskResource
                               {
                                   PalletNumber = 100,
                                   OriginalLocation = "B10",
                                   Destination = "S",
                                   EmployeeId = 123,
                                   Priority = 10,
                                   TaskSource = "BK",
                                   TaskType = "AT"
                               };
            this.result = this.Sut.MakeWarehouseTask(resource);
        }

        [Test]
        public void ShouldCallMovePallet()
        {
            this.WarehouseService.AtMovePallet(100,"B10", "S", 10, Arg.Any<Employee>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<MessageResult>>();
            var task = ((SuccessResult<MessageResult>)this.result).Data;
            task.Should().NotBeNull();
        }


        [Test]
        public void ShouldGetDefaultStatus()
        {
            var task = ((SuccessResult<MessageResult>)this.result).Data;
            task.Message.Should().Be("Task submitted to move pallet 100 from B10 to S");
        }
    }
}
