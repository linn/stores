namespace Linn.Stores.Domain.LinnApps.Tests.AllocationReportsServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDespatchPickingReport : ContextBase
    {
        private ResultsModel result;

        [SetUp]
        public void SetUp()
        {
            var summary = new List<DespatchPickingSummary>
                              {
                                  new DespatchPickingSummary
                                      {
                                          Addressee = "b",
                                          ArticleNumber = "an2",
                                          ConsignmentId = 1,
                                          FromPlace = "From2",
                                          Quantity = 34,
                                          QtyNeededFromLocation = 44,
                                          Location = "From2",
                                          LocationId = 23,
                                          QuantityOfItemsAtLocation = 44,
                                          InvoiceDescription = "Thing 2",
                                          PalletNumber = null
                                      },
                                  new DespatchPickingSummary
                                     {
                                         Addressee = "a",
                                         ArticleNumber = "an1",
                                         ConsignmentId = 1,
                                         FromPlace = "From1",
                                         Quantity = 2,
                                         QtyNeededFromLocation = 44,
                                         Location = "From1",
                                         LocationId = 2,
                                         QuantityOfItemsAtLocation = 723,
                                         InvoiceDescription = "Thing 1",
                                         PalletNumber = 4
                                     },
                                  new DespatchPickingSummary
                                     {
                                         Addressee = "c",
                                         ArticleNumber = "an3",
                                         ConsignmentId = 1,
                                         FromPlace = "From2",
                                         Quantity = 10,
                                         QtyNeededFromLocation = 44,
                                         Location = "From2",
                                         LocationId = 23,
                                         QuantityOfItemsAtLocation = 44,
                                         InvoiceDescription = "Thing 3",
                                         PalletNumber = null
                                     }
                              };
            this.DespatchPickingSummaryRepository.FindAll()
                .Returns(summary.AsQueryable());
            this.result = this.Sut.DespatchPickingSummary();
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.DespatchPickingSummaryRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnReportTitle()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("Despatch Picking Summary");
        }

        [Test]
        public void ShouldReturnReportValues()
        {
            this.result.RowCount().Should().Be(3);
            this.result.GetGridTextValue(this.result.RowIndex("001"), this.result.ColumnIndex("From"))
                .Should().Be("From1");
            this.result.GetGridTextValue(this.result.RowIndex("001"), this.result.ColumnIndex("Article Number"))
                .Should().Be("an1");
            this.result.GetGridTextValue(this.result.RowIndex("001"), this.result.ColumnIndex("Empty"))
                .Should().BeNullOrEmpty();
            this.result.GetGridTextValue(this.result.RowIndex("002"), this.result.ColumnIndex("From"))
                .Should().Be("From2");
            this.result.GetGridTextValue(this.result.RowIndex("002"), this.result.ColumnIndex("Article Number"))
                .Should().Be("an2");
            this.result.GetGridTextValue(this.result.RowIndex("002"), this.result.ColumnIndex("Empty"))
                .Should().Be("Empty");
            this.result.GetGridTextValue(this.result.RowIndex("003"), this.result.ColumnIndex("From"))
                .Should().Be("From2");
            this.result.GetGridTextValue(this.result.RowIndex("003"), this.result.ColumnIndex("Article Number"))
                .Should().Be("an3");
        }
    }
}
