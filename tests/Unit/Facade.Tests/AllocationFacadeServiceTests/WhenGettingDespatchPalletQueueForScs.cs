namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using Linn.Common.Facade;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Allocation;

    public class WhenGettingDespatchPalletQueueForScs : ContextBase
    {
        private IResult<IEnumerable<DespatchPalletQueueScsDetail>> result;

        [SetUp]
        public void SetUp()
        {
            var dpq = new List<DespatchPalletQueueScsDetail>()
                          {
                              new DespatchPalletQueueScsDetail
                                  {
                                      PalletNumber = 1, KittedFromTime = "12:00", PickingSequence = 1
                                  },
                              new DespatchPalletQueueScsDetail
                                  {
                                      PalletNumber = 2, KittedFromTime = "12:01", PickingSequence = 2
                                  },
                              new DespatchPalletQueueScsDetail
                                  {
                                      PalletNumber = 3, KittedFromTime = "12:02", PickingSequence = 3
                                  },
                          };


            this.DpqRepository.FindAll().Returns(dpq.AsQueryable());

            this.result = this.Sut.DespatchPalletQueueForScs();
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.DpqRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<DespatchPalletQueueScsDetail>>>();
            var dataResult = ((SuccessResult<IEnumerable<DespatchPalletQueueScsDetail>>)this.result).Data;
            dataResult.Count().Should().Be(3);
        }
    }
}
