namespace Linn.Stores.Facade.Tests.SosAllocDetailFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private SosAllocDetailResource updateResource;

        private IResult<SosAllocDetail> result;

        private SosAllocDetail detail;

        [SetUp]
        public void SetUp()
        {
            this.detail = new SosAllocDetail { QuantityToAllocate = 4, Id = 606 };
            this.updateResource = new SosAllocDetailResource { QuantityToAllocate = 400 };
            this.Repository.FindById(606).Returns(this.detail);
            this.result = this.Sut.Update(606, this.updateResource);
        }

        [Test]
        public void ShouldUpdateQtyToAllocate()
        {
            this.result.Should().BeOfType<SuccessResult<SosAllocDetail>>();
            var data = ((SuccessResult<SosAllocDetail>)this.result).Data;
            data.QuantityToAllocate.Should().Be(this.updateResource.QuantityToAllocate);
            data.Id.Should().Be(606);
        }
    }
}
