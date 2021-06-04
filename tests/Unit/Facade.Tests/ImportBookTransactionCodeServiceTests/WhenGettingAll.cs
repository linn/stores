namespace Linn.Stores.Facade.Tests.ImportBookTransactionCodeServiceTests
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
        private IResult<IEnumerable<ImportBookTransactionCode>> results;

        [SetUp]
        public void SetUp()
        {
            var transactionCodes = new List<ImportBookTransactionCode>
                                       {
                                           new ImportBookTransactionCode { TransactionId = 1, Description = "20" },
                                           new ImportBookTransactionCode { TransactionId = 2, Description = "60" }
                                       }.AsQueryable();

            this.ImportBookTransactionCodeRepository.FindAll().Returns(transactionCodes);

            this.results = this.Sut.GetTransactionCodes();
        }

        [Test]
        public void ShouldSearch()
        {
            this.ImportBookTransactionCodeRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ImportBookTransactionCode>>>();
            var dataResult = ((SuccessResult<IEnumerable<ImportBookTransactionCode>>)this.results).Data.ToList();
            dataResult.FirstOrDefault(x => x.TransactionId == 1).Should().NotBeNull();
            dataResult.FirstOrDefault(x => x.TransactionId == 2).Should().NotBeNull();
            dataResult.Count.Should().Be(2);
        }
    }
}
