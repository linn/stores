namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using System;

    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocationWithNoCutOff : ContextBase
    {
        private AllocationOptionsResource resource;

        private AllocationResult startDetails;

        [SetUp]
        public void SetUp()
        {
            this.startDetails = new AllocationResult(234);
            this.resource = new AllocationOptionsResource
                                {
                                    StockPoolCode = "LINN",
                                    AccountId = 24,
                                    ArticleNumber = "article",
                                    DespatchLocationCode = "dispatch",
                                    AccountingCompany = "LINN",
                                    CutOffDate = null,
                                    CountryCode = null,
                                    ExcludeOverCreditLimit = true,
                                    ExcludeUnsuppliableLines = true,
                                    ExcludeOnHold = true,
                                    ExcludeNorthAmerica = true,
                                    ExcludeEuropeanUnion = true
                                };

            this.AllocationService.StartAllocation(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<int>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<DateTime?>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>())
                .Returns(this.startDetails);

            this.Sut.StartAllocation(this.resource);
        }

        [Test]
        public void ShouldCallServiceWithoutDate()
        {
            this.AllocationService.Received().StartAllocation(
                this.resource.StockPoolCode,
                this.resource.DespatchLocationCode,
                this.resource.AccountId,
                this.resource.ArticleNumber,
                this.resource.AccountingCompany,
                this.resource.CountryCode,
                null,
                this.resource.ExcludeOverCreditLimit,
                this.resource.ExcludeUnsuppliableLines,
                this.resource.ExcludeOnHold,
                this.resource.ExcludeNorthAmerica,
                this.resource.ExcludeEuropeanUnion);
        }
    }
}
