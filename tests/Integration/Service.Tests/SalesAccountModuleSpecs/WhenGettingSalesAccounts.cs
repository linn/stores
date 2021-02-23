namespace Linn.Stores.Service.Tests.SalesAccountModuleSpecs
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

    public class WhenGettingSalesAccounts : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var acct1 = new SalesAccount { AccountId = 1, AccountName = "acct1" };
            var acct2 = new SalesAccount { AccountId = 2, AccountName = "acct2" };

            this.SalesAccountService.SearchSalesAccounts("acct").Returns(
                new SuccessResult<IEnumerable<SalesAccount>>(new List<SalesAccount> { acct1, acct2 }));

            this.Response = this.Browser.Get(
                "/inventory/sales-accounts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "acct");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.SalesAccountService.Received().SearchSalesAccounts("acct");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<SalesAccountResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(s => s.AccountName == "acct1");
            resource.Should().Contain(s => s.AccountName == "acct2");
        }
    }
}