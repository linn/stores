namespace Linn.Stores.Service.Tests.AccountingCompaniesModuleSpecs
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

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var companyA = new AccountingCompany
                               {
                                   Name = "A", Description = "description A"
                               };
            var companyB = new AccountingCompany
                               {
                                   Name = "B", Description = "description B"
                               };

            this.AccountingCompaniesService.GetValid()
                .Returns(new SuccessResult<IEnumerable<AccountingCompany>>(new List<AccountingCompany> { companyA, companyB }));

            this.Response = this.Browser.Get(
                "/inventory/accounting-companies",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.AccountingCompaniesService.Received().GetValid();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<AccountingCompanyResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Name == "A");
            resource.Should().Contain(a => a.Name == "B");
        }
    }
}