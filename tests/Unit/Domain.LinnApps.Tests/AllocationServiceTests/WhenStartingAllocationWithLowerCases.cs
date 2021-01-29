namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using System;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Sos;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocationWithLowerCases : ContextBase
    {
        private readonly int? accountId = 123;

        private readonly DateTime? cutOffDate = 1.December(2021);

        private readonly string stockPoolCode = "stores";

        private readonly string despatchLocation = "loc1";

        private readonly string articleNumber = "article";

        private readonly string accountingCompany = "linn";

        private readonly string countryCode = "fr";

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
                this.stockPoolCode.ToUpper(),
                this.despatchLocation.ToUpper(),
                this.accountId,
                null,
                this.articleNumber.ToUpper(),
                this.accountingCompany.ToUpper(),
                this.cutOffDate,
                this.countryCode.ToUpper(),
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
                    o => o.ArticleNumber == this.articleNumber.ToUpper()
                         && o.AccountId == this.accountId
                         && o.DespatchLocationCode == this.despatchLocation.ToUpper()
                         && o.StockPoolCode == this.stockPoolCode.ToUpper()
                         && o.CountryCode == this.countryCode.ToUpper()
                         && o.AccountingCompany == this.accountingCompany.ToUpper()
                         && o.CutOffDate == this.cutOffDate));
        }

        [Test]
        public void ShouldReturnStartDetails()
        {
            this.result.Id.Should().Be(808);
        }
    }
}
