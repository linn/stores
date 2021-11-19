namespace Linn.Stores.Facade.Tests.ExportRsnService
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

    public class WhenGettingExportRsnsWithNumber : ContextBase
    {
        private IResult<IEnumerable<ExportRsn>> result;

        [SetUp]
        public void SetUp()
        {
            var rsns = new List<ExportRsn> { new ExportRsn { RsnNumber = 10 }, new ExportRsn { RsnNumber = 20 }, new ExportRsn { RsnNumber = 30 } };

            this.ExportRsnRepository.FilterBy(Arg.Any<Expression<Func<ExportRsn, bool>>>()).Returns(rsns.AsQueryable());

            this.result = this.Sut.SearchRsns(123, null, "2");
        }

        [Test]

        public void ShouldSearch()
        {
            this.ExportRsnRepository.Received().FilterBy(Arg.Any<Expression<Func<ExportRsn, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<ExportRsn>>>();
        }

        [Test]
        public void ShouldReturnOneMatchRSN()
        {
            ((SuccessResult<IEnumerable<ExportRsn>>)this.result).Data.Count().Should().Be(1);
            var rsn = ((SuccessResult<IEnumerable<ExportRsn>>)this.result).Data.FirstOrDefault();
            rsn.RsnNumber.Should().Be(20);
        }
    }
}