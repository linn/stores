namespace Linn.Stores.Domain.LinnApps.Tests.TqmsReportsServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Tqms;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTqmsSummaryReportWithPence : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            var stock = new List<TqmsSummaryByCategory>
                              {
                                  new TqmsSummaryByCategory
                                      {
                                          TotalValue = 12, CategoryDescription = "c1", HeadingDescription = "c", HeadingCode = "c", HeadingOrder = 1
                                      },
                                  new TqmsSummaryByCategory
                                      {
                                          TotalValue = 10, CategoryDescription = "c2", HeadingDescription = "c", HeadingCode = "c", HeadingOrder = 1
                                      },
                                  new TqmsSummaryByCategory
                                      {
                                          TotalValue = 20, CategoryDescription = "d", HeadingDescription = "d", HeadingCode = "d", HeadingOrder = 2
                                      }
                              };
            var loans = new List<TqmsOutstandingLoansByCategory>
                            {
                                new TqmsOutstandingLoansByCategory
                                    {
                                        TotalStoresValue = 8, Category = "c"
                                    },
                                new TqmsOutstandingLoansByCategory
                                    {
                                        TotalStoresValue = 19, Category = "d"
                                    }
                            };
            this.TqmsSummaryByCategoryRepository
                .FilterBy(Arg.Any<Expression<Func<TqmsSummaryByCategory, bool>>>())
                .Returns(stock.AsQueryable());
            this.TqmsOutstandingLoansByCategoryRepository
                .FilterBy(Arg.Any<Expression<Func<TqmsOutstandingLoansByCategory, bool>>>())
                .Returns(loans.AsQueryable());
            this.results = this.Sut.TqmsSummaryByCategoryReport(this.JobRef, true, true);
        }

        [Test]
        public void ShouldReturnReportValues()
        {
            var totalStockSummary = this.results.First();
            totalStockSummary.GetRowValues()
                .First(a => a.RowIndex == totalStockSummary.RowIndex("Total Stock")).Values
                .First(a => a.Key == totalStockSummary.ColumnIndex("Value")).Value.DecimalPlaces.Should().Be(2);
        }
    }
}
