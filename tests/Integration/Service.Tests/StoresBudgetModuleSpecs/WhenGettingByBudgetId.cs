namespace Linn.Stores.Service.Tests.StoresBudgetModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByBudgetId : ContextBase
    {
        private int budgetId;

        [SetUp]
        public void SetUp()
        {
            this.budgetId = 1234;
            var postings = new List<StoresBudgetPosting> { new StoresBudgetPosting { BudgetId = this.budgetId } };

            this.StoresBudgetPostingsFacadeService.FilterBy(Arg.Any<StoresBudgetPostingResource>())
                .Returns(new SuccessResult<IEnumerable<StoresBudgetPosting>>(postings));

            this.Response = this.Browser.Get(
                $"/inventory/stores-budget-postings/{this.budgetId}",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.StoresBudgetPostingsFacadeService.Received().FilterBy(Arg.Any<StoresBudgetPostingResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<StoresBudgetPostingResource>>().ToList();
            resources.Should().HaveCount(1);
            resources.First().BudgetId.Should().Be(this.budgetId);
        }
    }
}
