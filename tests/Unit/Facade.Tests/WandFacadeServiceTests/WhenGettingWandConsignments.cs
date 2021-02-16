namespace Linn.Stores.Facade.Tests.WandFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWandConsignments : ContextBase
    {
        private List<WandConsignment> consignments;

        private IResult<IEnumerable<WandConsignment>> results;

        [SetUp]
        public void SetUp()
        {
            this.consignments = new List<WandConsignment>
                                    {
                                        new WandConsignment { ConsignmentId = 1 },
                                        new WandConsignment { ConsignmentId = 2 }
                                    };
            this.WandConsignmentsRepository.FindAll()
                .Returns(this.consignments.AsQueryable());

            this.results = this.Sut.GetWandConsignments();
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.WandConsignmentsRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<WandConsignment>>>();
            var dataResult = ((SuccessResult<IEnumerable<WandConsignment>>)this.results).Data;
            dataResult.Should().HaveCount(2);
        }
    }
}
