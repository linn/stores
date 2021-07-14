namespace Linn.Stores.Facade.Tests.ImportBookCpcNumberFacadeServiceTests
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
        private IResult<IEnumerable<ImportBookCpcNumber>> results;

        [SetUp]
        public void SetUp()
        {
            var cpcNumbers = new List<ImportBookCpcNumber>
                                              {
                                                  new ImportBookCpcNumber { CpcNumber = 1, Description = "num1" },
                                                  new ImportBookCpcNumber { CpcNumber = 2, Description = "num2" }
                                              }.AsQueryable();

            this.ImportBookCpcNumberRepository.FindAll().Returns(cpcNumbers);

            this.results = this.Sut.GetAll();
        }

        [Test]
        public void ShouldSearch()
        {
            this.ImportBookCpcNumberRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ImportBookCpcNumber>>>();
            var dataResult = ((SuccessResult<IEnumerable<ImportBookCpcNumber>>)this.results).Data.ToList();
            dataResult.FirstOrDefault(x => x.CpcNumber == 1 && x.Description == "num1").Should().NotBeNull();
            dataResult.FirstOrDefault(x => x.CpcNumber == 2 && x.Description == "num2").Should().NotBeNull();
            dataResult.Count.Should().Be(2);
        }
    }
}
