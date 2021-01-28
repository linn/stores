namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using System;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Sos;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocation : ContextBase
    {
        private readonly int? accountId = 123;

        private readonly DateTime? cutOffDate = 1.December(2021);

        private readonly string stockPoolCode = "STORES";

        private readonly string despatchLocation = "LOC1";

        private readonly string articleNumber = "ARTICLE";

        private readonly string accountingCompany = "LINN";

        private readonly string countryCode = "FR";

        private readonly bool excludeUnsuppliable = true;

        private readonly bool excludeHold = true;

        private readonly bool excludeOverCredit = true;

        private readonly bool excludeNorthAmerica = true;

        private readonly bool excludeEuropeanUnion = true;

        private AllocationResult result;

        [SetUp]
        public void SetUp()
        {
            this.AllocPack.StartAllocation(
                this.stockPoolCode,
                this.despatchLocation,
                this.accountId,
                null,
                this.articleNumber,
                this.accountingCompany,
                this.cutOffDate,
                this.countryCode,
                this.excludeUnsuppliable,
                this.excludeHold,
                this.excludeOverCredit,
                this.excludeNorthAmerica,
                this.excludeEuropeanUnion,
                out _,
                out _).Returns(808);

            this.result = this.Sut.StartAllocation(
                this.stockPoolCode,
                this.despatchLocation,
                this.accountId,
                this.articleNumber,
                this.accountingCompany,
                this.countryCode,
                this.cutOffDate,
                this.excludeUnsuppliable,
                this.excludeHold,
                this.excludeOverCredit,
                this.excludeNorthAmerica,
                this.excludeEuropeanUnion);
        }

        [Test]
        public void ShouldSaveOptions()
        {
            this.SosOptionRepository.Received().Add(
                Arg.Is<SosOption>(
                    o => o.ArticleNumber == this.articleNumber
                         && o.AccountId == this.accountId
                         && o.DespatchLocationCode == this.despatchLocation
                         && o.StockPoolCode == this.stockPoolCode
                         && o.CountryCode == this.countryCode
                         && o.AccountingCompany == this.accountingCompany
                         && o.CutOffDate == this.cutOffDate));
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldReturnStartDetails()
        {
            this.result.Id.Should().Be(808);
        }
    }
}
