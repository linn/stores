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

    public class WhenGettingTqmsSummaryReportWithCategories : ContextBase
    {
        private IEnumerable<ResultsModel> results;

        [SetUp]
        public void SetUp()
        {
            var stock = new List<TqmsSummaryByCategory>
                              {
                                  new TqmsSummaryByCategory
                                      {
                                          TotalValue = 12, CategoryDescription = "c1", HeadingDescription = "c"
                                      },
                                  new TqmsSummaryByCategory
                                      {
                                          TotalValue = 10, CategoryDescription = "c2", HeadingDescription = "c"
                                      },
                                  new TqmsSummaryByCategory
                                      {
                                          TotalValue = 20, CategoryDescription = "d", HeadingDescription = "d"
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
            this.results = this.Sut.TqmsSummaryByCategoryReport(this.JobRef, false);
        }

        [Test]
        public void ShouldCallRepositories()
        {
            this.TqmsSummaryByCategoryRepository.Received()
                .FilterBy(Arg.Any<Expression<Func<TqmsSummaryByCategory, bool>>>());
            this.TqmsOutstandingLoansByCategoryRepository.Received()
                .FilterBy(Arg.Any<Expression<Func<TqmsOutstandingLoansByCategory, bool>>>());
        }

        [Test]
        public void ShouldReturnReportValues()
        {
            var totalStockSummary = this.results.First();
            var tqmsSummary = this.results.Last();
            totalStockSummary.RowCount().Should().Be(2);
            totalStockSummary.GetGridTextValue(totalStockSummary.RowIndex("Total Stock"), totalStockSummary.ColumnIndex("StockType"))
                .Should().Be("Stock Value");
            totalStockSummary.GetGridValue(totalStockSummary.RowIndex("Total Stock"), totalStockSummary.ColumnIndex("Value"))
                .Should().Be(42);
            totalStockSummary.GetGridTextValue(totalStockSummary.RowIndex("Loan Stock Value"), totalStockSummary.ColumnIndex("StockType"))
                .Should().Be("Loan Stock Value");
            totalStockSummary.GetGridValue(totalStockSummary.RowIndex("Loan Stock Value"), totalStockSummary.ColumnIndex("Value"))
                .Should().Be(27);
            totalStockSummary.GetTotalValue(totalStockSummary.ColumnIndex("Value")).Should().Be(69);

            tqmsSummary.RowCount().Should().Be(5);
            tqmsSummary.GetGridTextValue(tqmsSummary.RowIndex("001"), tqmsSummary.ColumnIndex("Heading"))
                .Should().Be("c");
            tqmsSummary.GetGridTextValue(tqmsSummary.RowIndex("001"), tqmsSummary.ColumnIndex("Category"))
                .Should().Be("c1");
            tqmsSummary.GetGridValue(tqmsSummary.RowIndex("001"), tqmsSummary.ColumnIndex("Value"))
                .Should().Be(12);
            tqmsSummary.GetGridTextValue(tqmsSummary.RowIndex("002"), tqmsSummary.ColumnIndex("Heading"))
                .Should().Be(string.Empty);
            tqmsSummary.GetGridTextValue(tqmsSummary.RowIndex("002"), tqmsSummary.ColumnIndex("Category"))
                .Should().Be("c2");
            tqmsSummary.GetGridValue(tqmsSummary.RowIndex("002"), tqmsSummary.ColumnIndex("Value"))
                .Should().Be(10);
            tqmsSummary.GetGridValue(tqmsSummary.RowIndex("subtotalc"), tqmsSummary.ColumnIndex("Value"))
                .Should().Be(22);
            tqmsSummary.GetGridTextValue(tqmsSummary.RowIndex("003"), tqmsSummary.ColumnIndex("Heading"))
                .Should().Be("d");
            tqmsSummary.GetGridTextValue(tqmsSummary.RowIndex("003"), tqmsSummary.ColumnIndex("Category"))
                .Should().Be("d");
            tqmsSummary.GetGridValue(tqmsSummary.RowIndex("003"), tqmsSummary.ColumnIndex("Value"))
                .Should().Be(20);
            tqmsSummary.GetGridValue(tqmsSummary.RowIndex("subtotald"), tqmsSummary.ColumnIndex("Value"))
                .Should().Be(20);
            tqmsSummary.GetTotalValue(tqmsSummary.ColumnIndex("Value")).Should().Be(42);
        }
    }
}
