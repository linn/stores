namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocationWithNoCutOff : ContextBase
    {
        private AllocationOptionsResource resource;

        private AllocationStart startDetails;

        private IResult<AllocationStart> result;

        [SetUp]
        public void SetUp()
        {
            this.startDetails = new AllocationStart(234);
            this.resource = new AllocationOptionsResource
                                {
                                    StockPoolCode = "LINN",
                                    AccountId = 24,
                                    ArticleNumber = "article",
                                    DespatchLocationCode = "dispatch",
                                    AccountingCompany = "LINN",
                                    CutOffDate = null
                                };

            this.AllocationService.StartAllocation(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<int>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<DateTime?>())
                .Returns(this.startDetails);

            this.result = this.Sut.StartAllocation(this.resource);
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
                null);
        }
    }
}