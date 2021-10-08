namespace Linn.Stores.Facade.Tests.LoanHeadersServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var loanHeaders = new List<LoanHeader>
                                  {
                                      new LoanHeader {LoanNumber = 12345}, new LoanHeader {LoanNumber = 72345}
                                  };
            this.LoanHeaderRepository.FilterBy(Arg.Any<Expression<Func<LoanHeader, bool>>>())
                .Returns(loanHeaders.AsQueryable());
        }

        [Test]
        public void ShouldCallSearch()
        {
            this.Sut.Search("2345");
            this.LoanHeaderRepository.Received().FilterBy(Arg.Any<Expression<Func<LoanHeader, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            var result = this.Sut.Search("111");
            result.Should().BeOfType<SuccessResult<IEnumerable<LoanHeader>>>();
        }
    }
}
