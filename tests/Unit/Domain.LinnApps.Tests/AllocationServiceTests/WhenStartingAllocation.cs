namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Sos;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocation : ContextBase
    {
        private AllocationStart result;

        [SetUp]
        public void SetUp()
        {
            this.SosPack.GetJobId().Returns(808);

            this.result = this.Sut.StartAllocation("stores", "loc1", 123, "article", "LINN", 1.December(2021));
        }

        [Test]
        public void ShouldSetId()
        {
            this.SosPack.Received().SetNewJobId();
        }

        [Test]
        public void ShouldSaveOptions()
        {
            this.SosOptionRepository.Received().Add(
                Arg.Is<SosOption>(
                    o => o.ArticleNumber == "article"
                         && o.AccountId == 123
                         && o.DespatchLocationCode == "loc1"
                         && o.StockPoolCode == "stores"
                         && o.JobId == 808
                         && o.AccountingCompany == "LINN"
                         && o.CutOffDate == 1.December(2021)));
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
