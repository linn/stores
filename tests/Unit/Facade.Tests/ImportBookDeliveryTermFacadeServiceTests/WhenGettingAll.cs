namespace Linn.Stores.Facade.Tests.ImportBookDeliveryTermFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        private IResult<IEnumerable<ImportBookDeliveryTerm>> results;

        [SetUp]
        public void SetUp()
        {
            var deliveryTerms = new List<ImportBookDeliveryTerm>
                                             {
                                                 new ImportBookDeliveryTerm { DeliveryTermCode = "A", Description = "num1", Comments = "a" },
                                                 new ImportBookDeliveryTerm { DeliveryTermCode = "B", Description = "num2", Comments = "c" }
                                             }.AsQueryable();

            this.ImportBookDeliveryTermRepository.FindAll().Returns(deliveryTerms);

            this.results = this.Sut.GetAll();
        }

        [Test]
        public void ShouldSearch()
        {
            this.ImportBookDeliveryTermRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ImportBookDeliveryTerm>>>();
            var dataResult = ((SuccessResult<IEnumerable<ImportBookDeliveryTerm>>)this.results).Data.ToList();
            dataResult.Any(x => x.DeliveryTermCode == "A").Should().BeTrue();
            dataResult.Any(x => x.DeliveryTermCode == "B").Should().BeTrue();
            dataResult.Count.Should().Be(2);
        }
    }
}
