namespace Linn.Stores.Domain.LinnApps.Tests.StoresMoveLogReportTests
{
    using Linn.Common.Reporting.Models;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;

    using FluentAssertions;

    public class WhenGettingReportWithExtraFilters : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void Setup()
        {
            var results = new List<StoresMoveLog>
                              {
                                  new StoresMoveLog { Id = 1, BudgetId = 1, PartNumber = "CAP 003", FromLocation = "P456", ToLocation = "P123", FromStockPool = "LINN", ToStockPool = "LINN", TransactionCode = "STSTM" },
                                  new StoresMoveLog { Id = 2, BudgetId = 2, PartNumber = "CAP 003", FromLocation = "P123", ToLocation = "E-FA-STORES", FromStockPool = "LINN", ToStockPool = "LINN", TransactionCode = "STSTM" },
                                  new StoresMoveLog { Id = 3, BudgetId = 2, PartNumber = "CAP 003", FromLocation = "E-FA-STORES", ToLocation = string.Empty, FromStockPool = "LINN", ToStockPool = string.Empty, TransactionCode = "STLDI" },
                                  new StoresMoveLog { Id = 4, BudgetId = 1, PartNumber = "CAP 003", FromLocation = string.Empty, ToLocation = "P123", FromStockPool = "LINN", ToStockPool = "LINN", TransactionCode = "LDSTI" },
                                  new StoresMoveLog { Id = 5, BudgetId = 1, PartNumber = "CAP 003", FromLocation = "P123", ToLocation = "E-FA-STORES", FromStockPool = "LINN", ToStockPool = "LINN", TransactionCode = "STSTM" },
                                  new StoresMoveLog { Id = 6, BudgetId = 1, PartNumber = "CAP 003", FromLocation = "E-SECRET-BASE", ToLocation = "E-EVIL-LAIR", FromStockPool = "RESERVED", ToStockPool = "RESERVED", TransactionCode = "STSTM" }
                              };
            var date = new DateTime(2023, 3, 1);

            this.StoresMoveLogRepository.FilterBy(Arg.Any<Expression<Func<StoresMoveLog, bool>>>()).Returns(results.AsQueryable());

            this.result = this.Sut.GetReport(
                date,
                date,
                "CAP 003",
                "STSTM",
                "123",
                "LINN");
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("01Mar23 - 01Mar23 CAP 003 trans STSTM location 123 stock pool LINN");
        }

        [Test]
        public void ShouldSetRows()
        {
            this.result.Rows.Should().HaveCount(3);
        }

        [Test]
        public void ShouldSetColumns()
        {
            this.result.Columns.Should().HaveCount(12);
        }
    }
}
