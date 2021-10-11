namespace Linn.Stores.Service.Tests.LoansModuleSpecs
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

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var loans = new List<Loan> { new Loan { LoanNumber = 12345 }, new Loan { LoanNumber = 72345 } };

            this.LoanFacadeService.Search(Arg.Any<string>()).Returns(new SuccessResult<IEnumerable<Loan>>(loans));

            this.Response = this.Browser.Get(
                "/logistics/loans",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "2345");
                    }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.LoanFacadeService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<LoanResource>>();
            resources.Count().Should().Be(2);
        }
    }
}
