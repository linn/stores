namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
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

    public class WhenSearchingLoanDetails : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Service.GetLoanDetails(Arg.Any<int>())
                .Returns(new SuccessResult<IEnumerable<LoanDetail>>(new List<LoanDetail>
                                                                        {
                                                                            new LoanDetail { Line = 1 },
                                                                            new LoanDetail { Line = 2 }
                                                                        }));

            this.Response = this.Browser.Get(
                "/logistics/loan-details",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("loanNumber", "1891");
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
            this.Service.Received().GetLoanDetails(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<LoanDetailResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.Line == 1);
            resource.Should().Contain(a => a.Line == 2);
        }
    }
}
