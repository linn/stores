namespace Linn.Stores.Domain.LinnApps.Tests.StockReportServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingReport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void Setup()
        {
            var results = new List<StockLocator>
                              {
                                  new StockLocator
                                      {
                                          Id = 1,
                                          StorageLocation = new StorageLocation { LocationCode = "SU-1234" },
                                          Quantity = 12,
                                          BatchRef = "B093",
                                          State = "STORES",
                                          PalletNumber = null,
                                          PartNumber = "CAP 050"
                                      },
                                  new StockLocator
                                      {
                                          Id = 234,
                                          StorageLocation = new StorageLocation { LocationCode = "SU-9874" },
                                          Quantity = 4,
                                          BatchRef = "B4587",
                                          State = "STORES",
                                          PalletNumber = null,
                                          PartNumber = "MISS 123"
                                      }
                              };

            this.StockLocatorRepository.FilterBy(Arg.Any<Expression<Func<StockLocator, bool>>>())
                .Returns(results.AsQueryable());

            this.result = this.Sut.GetStockLocatorReport("SUPPLIERS");
        }

        [Test]
        public void ShouldSetReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Stock at site SUPPLIERS");
        }

        [Test]
        public void ShouldSetRows()
        {
            this.result.Rows.Should().HaveCount(2);
        }

        [Test]
        public void ShouldReturnReportValues()
        {
            this.result.RowCount().Should().Be(2);
            this.result.GetGridTextValue(0, 0).Should().Be("SU-1234");
            this.result.GetGridTextValue(0, 1).Should().BeNullOrEmpty();
            this.result.GetGridTextValue(0, 2).Should().Be("CAP 050");
            this.result.GetGridValue(0, 3).Should().Be(12);
            this.result.GetGridTextValue(0, 4).Should().Be("STORES");
            this.result.GetGridTextValue(0, 5).Should().Be("B093");
            this.result.GetGridTextValue(1, 0).Should().Be("SU-9874");
            this.result.GetGridTextValue(1, 1).Should().BeNullOrEmpty();
            this.result.GetGridTextValue(1, 2).Should().Be("MISS 123");
            this.result.GetGridValue(1, 3).Should().Be(4);
            this.result.GetGridTextValue(1, 4).Should().Be("STORES");
            this.result.GetGridTextValue(1, 5).Should().Be("B4587");
        }
    }
}
