namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using System;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocation : ContextBase
    {
        private AllocationOptionsResource resource;

        private AllocationResult startDetails;

        private IResult<AllocationResult> result;

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
                                    CutOffDate = 1.July(2021).ToString("o"),
                                    CountryCode = null,
                                    ExcludeOverCreditLimit = true,
                                    ExcludeUnsuppliableLines = true,
                                    ExcludeOnHold = true
                                };

            this.AllocationService.StartAllocation(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<int>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<DateTime?>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>(),
                    Arg.Any<bool>())
                .Returns(this.startDetails);

            this.result = this.Sut.StartAllocation(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.AllocationService.Received().StartAllocation(
                this.resource.StockPoolCode,
                this.resource.DespatchLocationCode,
                this.resource.AccountId,
                this.resource.ArticleNumber,
                this.resource.AccountingCompany,
                1.July(2021),
                this.resource.ExcludeOverCreditLimit,
                this.resource.ExcludeUnsuppliableLines,
                this.resource.ExcludeOnHold);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<AllocationResult>>();
            var dataResult = ((SuccessResult<AllocationResult>)this.result).Data;
            dataResult.Id.Should().Be(this.startDetails.Id);
        }
    }
}