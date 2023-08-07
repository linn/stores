namespace Linn.Stores.Domain.LinnApps.Tests.StoresMoveLogReportTests
{
    using FluentAssertions;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Text;

    public class WhenGettingReport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void Setup()
        {
            var results = new List<StoresMoveLog>
                              {
                                  new StoresMoveLog { Id = 1, BudgetId = 1, PartNumber = "CAP 003" },
                                  new StoresMoveLog { Id = 2, BudgetId = 2, PartNumber = "CAP 003" }
                              };
            var date = new DateTime(2023, 3, 1);

            this.StoresMoveLogRepository.FilterBy(Arg.Any<Expression<Func<StoresMoveLog, bool>>>()).Returns(results.AsQueryable());

            this.result = this.Sut.GetReport(
                date,
                date,
                "CAP 003",
                string.Empty,
                string.Empty,
                string.Empty);
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("01Mar23 - 01Mar23 CAP 003 ");
        }

        [Test]
        public void ShouldSetRows()
        {
            this.result.Rows.Should().HaveCount(2);
        }

        [Test]
        public void ShouldSetColumns()
        {
            this.result.Columns.Should().HaveCount(9);
        }
    }
}
