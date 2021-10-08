namespace Linn.Stores.Service.Tests.LoanHeadersModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
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
            var loanHeaders = new List<LoanHeader>
                           {
                               new LoanHeader
                                   {
                                       LoanNumber = 12345
                                   },
                               new LoanHeader
                                   {
                                      LoanNumber = 72345
                                   }
                           };

            this.LoanHeaderFacadeService.Search(Arg.Any<string>()).Returns(new SuccessResult<IEnumerable<LoanHeader>>(loanHeaders));

            this.Response = this.Browser.Get(
                "/logistics/loan-headers",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "2345");
                    }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.LoanHeaderFacadeService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<LoanHeaderResource>>();
            resources.Count().Should().Be(2);
        }
    }
}
