﻿namespace Linn.Stores.Facade.Tests.LoansServiceTests
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
            var loans = new List<Loan> { new Loan { LoanNumber = 12345 }, new Loan { LoanNumber = 72345 } };
            this.LoanRepository.FilterBy(Arg.Any<Expression<Func<Loan, bool>>>()).Returns(loans.AsQueryable());
        }

        [Test]
        public void ShouldCallSearch()
        {
            this.Sut.Search("2345");
            this.LoanRepository.Received().FilterBy(Arg.Any<Expression<Func<Loan, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            var result = this.Sut.Search("111");
            result.Should().BeOfType<SuccessResult<IEnumerable<Loan>>>();
        }
    }
}
