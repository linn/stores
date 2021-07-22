namespace Linn.Stores.Domain.LinnApps.Tests.ImportBooksReportsServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingIprReport : ContextBase
    {
        private readonly int iprCpcNumberId = 13;

        private ResultsModel result;

        private List<ImportBook> iprImpBooks;

        [SetUp]
        public void SetUp()
        {
            iprImpBooks = new List<ImportBook>
                              {
                                  new ImportBook
                                      {
                                          Id = 123,
                                          Currency = "EUR",
                                          TotalImportValue = 5555,
                                          CarrierId = 99,
                                          CustomsEntryCodeDate = 1.February(2021),
                                          CustomsEntryCodePrefix = "PR",
                                          CustomsEntryCode = "01312u1891",
                                          TransportBillNumber = "EdStob555",
                                          OrderDetails = new List<ImportBookOrderDetail>
                                                             {
                                                                 new ImportBookOrderDetail
                                                                     {
                                                                         CpcNumber = this.iprCpcNumberId,
                                                                         RsnNumber = 140111,
                                                                         TariffCode = "849500001111",
                                                                         LineNumber = 3
                                                                     },
                                                                 new ImportBookOrderDetail
                                                                     {
                                                                         CpcNumber = this.iprCpcNumberId,
                                                                         RsnNumber = 140234,
                                                                         TariffCode = "849500002222",
                                                                         LineNumber = 2
                                                                     },
                                                                 new ImportBookOrderDetail
                                                                     {
                                                                         CpcNumber = 999999,
                                                                         RsnNumber = 140234,
                                                                         TariffCode = "849500002222",
                                                                         LineNumber = 1
                                                                     }
                                                             },
                                          InvoiceDetails = new List<ImportBookInvoiceDetail>
                                                               {
                                                                   new ImportBookInvoiceDetail
                                                                       {
                                                                           InvoiceValue = 3333
                                                                       },
                                                                   new ImportBookInvoiceDetail
                                                                       {
                                                                           InvoiceValue = 4455
                                                                       }
                                                               }
                                      },
                                  new ImportBook
                                      {
                                          Id = 678,
                                          Currency = "DKK",
                                          TotalImportValue = 340,
                                          CarrierId = 101,
                                          CustomsEntryCodeDate = 2.March(2021),
                                          CustomsEntryCodePrefix = "PR",
                                          CustomsEntryCode = "UrAllowedInM8",
                                          TransportBillNumber = "EdStob123",
                                          OrderDetails =
                                              new List<ImportBookOrderDetail>
                                                  {
                                                      new ImportBookOrderDetail
                                                          {
                                                              CpcNumber = this.iprCpcNumberId,
                                                              RsnNumber = 140333,
                                                              TariffCode = "8495abc",
                                                              LineNumber = 1
                                                          },
                                                  },
                                          InvoiceDetails = new List<ImportBookInvoiceDetail>
                                                               {
                                                                   new ImportBookInvoiceDetail
                                                                       {
                                                                           InvoiceValue = 4628
                                                                       },
                                                                   new ImportBookInvoiceDetail
                                                                       {
                                                                           InvoiceValue = 2111
                                                                       }
                                                               }
                                      },
                              };
            this.ImpbookRepository.FilterBy(Arg.Any<Expression<Func<ImportBook, bool>>>())
                .Returns(iprImpBooks.AsQueryable());
            this.result = this.Sut.GetIPRReport(1.January(2021), 1.June(2021), true);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.ImpbookRepository.Received().FilterBy(Arg.Any<Expression<Func<ImportBook, bool>>>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.ReportTitle.DisplayValue.Should().Be("IPR Impbooks between 01-Jan-2021 and 01-Jun-2021");
            this.result.Rows.Count().Should().Be(3);
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("RsnNo")).Should()
                .Be("140111");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("Currency")).Should()
                .Be("EUR");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("ForeignValue")).Should()
                .Be("7788");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("GBPValue")).Should()
                .Be("5555");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("CarrierId")).Should()
                .Be("99");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("CustomsEntryCodeDate")).Should()
                .Be("2021/02/01");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("CustomsEntryCode")).Should()
                .Be("PR - 01312u1891");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("ShippingRef")).Should()
                .Be("EdStob555");
            this.result.GetGridTextValue(this.result.RowIndex("123/3"), this.result.ColumnIndex("TariffCode")).Should()
                .Be("849500001111");
        }
    }
}
