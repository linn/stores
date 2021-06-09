namespace Linn.Stores.Facade.Tests.ImportBookFacadeTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Common;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreating : ContextBase
    {
        private readonly int impbookId = 54321;

        private readonly DateTime now = DateTime.Now;

        private ImportBookResource resource;

        private ImportBook from;

        private IResult<ImportBook> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ImportBookResource
                                {
                                    Id = this.impbookId,
                                    DateCreated = this.now.AddDays(2).ToString("o"),
                                    ParcelNumber = 1,
                                    SupplierId = 556,
                                    ForeignCurrency = "YN",
                                    Currency = "GBD",
                                    CarrierId = 678,
                                    OldArrivalPort = "Glesga",
                                    FlightNumber = "sk123",
                                    TransportId = 2,
                                    TransportBillNumber = "1212",
                                    TransactionId = 45,
                                    DeliveryTermCode = "dli",
                                    ArrivalPort = "LAZ",
                                    LineVatTotal = 12,
                                    Hwb = "hwbbb",
                                    SupplierCostCurrency = "egg",
                                    TransNature = "sea",
                                    ArrivalDate = this.now.AddDays(3).ToString("o"),
                                    FreightCharges = 11.1m,
                                    HandlingCharge = 11.1m,
                                    ClearanceCharge = 11.1m,
                                    Cartage = 11.1m,
                                    Duty = 11.1m,
                                    Vat = 11.1m,
                                    Misc = 11.1m,
                                    CarriersInvTotal = 11.1m,
                                    CarriersVatTotal = 11.1m,
                                    TotalImportValue = 133.4m,
                                    Pieces = 1,
                                    Weight = 11.1m,
                                    CustomsEntryCode = "code green",
                                    CustomsEntryCodeDate = this.now.AddDays(2).ToString("o"),
                                    LinnDuty = 12,
                                    LinnVat = 11.1m,
                                    IprCpcNumber = 1,
                                    EecgNumber = 1,
                                    DateCancelled = this.now.AddDays(5).ToString("o"),
                                    CancelledBy = 33105,
                                    CancelledReason = "cancel",
                                    CarrierInvNumber = "inv123",
                                    CarrierInvDate = this.now.AddDays(3).ToString("o"),
                                    CountryOfOrigin = "DE",
                                    FcName = "FC1",
                                    VaxRef = "VAX123",
                                    Storage = 11.1m,
                                    NumCartons = 1,
                                    NumPallets = 1,
                                    Comments = "now closed",
                                    ExchangeRate = 11.1m,
                                    ExchangeCurrency = "BB",
                                    BaseCurrency = "AA",
                                    PeriodNumber = 47,
                                    CreatedBy = 33105,
                                    PortCode = "g74",
                                    CustomsEntryCodePrefix = "AA",
                                    ImportBookInvoiceDetails =
                                        new List<ImportBookInvoiceDetailResource>
                                            {
                                                new ImportBookInvoiceDetailResource
                                                    {
                                                        ImportBookId = this.impbookId,
                                                        InvoiceNumber = "123",
                                                        LineNumber = 1,
                                                        InvoiceValue = 12.5m
                                                    },
                                                new ImportBookInvoiceDetailResource
                                                    {
                                                        ImportBookId = this.impbookId,
                                                        InvoiceNumber = "1234",
                                                        LineNumber = 2,
                                                        InvoiceValue = 155.2m
                                                    }
                                            },
                                    ImportBookOrderDetails =
                                        new List<ImportBookOrderDetailResource>
                                            {
                                                new ImportBookOrderDetailResource
                                                    {
                                                        ImportBookId = this.impbookId,
                                                        LineNumber = 2,
                                                        OrderNumber = 13,
                                                        RsnNumber = 2,
                                                        OrderDescription = "palpatine final order",
                                                        Qty = 1,
                                                        DutyValue = 21.12m,
                                                        FreightValue = 22.12m,
                                                        VatValue = 3.12m,
                                                        OrderValue = 44.1m,
                                                        Weight = 55.2m,
                                                        LoanNumber = null,
                                                        LineType = "TYpe B",
                                                        CpcNumber = null,
                                                        TariffCode = "121213",
                                                        InsNumber = null,
                                                        VatRate = null
                                                    },
                                                new ImportBookOrderDetailResource
                                                    {
                                                        ImportBookId = this.impbookId,
                                                        LineNumber = 1,
                                                        OrderNumber = 111,
                                                        RsnNumber = 222,
                                                        OrderDescription = "kylo ren first order",
                                                        Qty = 3,
                                                        DutyValue = 91.12m,
                                                        FreightValue = 92.12m,
                                                        VatValue = 93.12m,
                                                        OrderValue = 944.1m,
                                                        Weight = 955.2m,
                                                        LoanNumber = 999,
                                                        LineType = "Type C",
                                                        CpcNumber = 91,
                                                        TariffCode = "121213",
                                                        InsNumber = 92,
                                                        VatRate = 93
                                                    }
                                            },
                                    ImportBookPostEntries = new List<ImportBookPostEntryResource>
                                                                {
                                                                    new ImportBookPostEntryResource
                                                                        {
                                                                            ImportBookId = this.impbookId,
                                                                            LineNumber = 1,
                                                                            EntryCodePrefix = "PR",
                                                                            EntryCode = "code blu",
                                                                            EntryDate = null,
                                                                            Reference = "refer fence",
                                                                            Duty = null,
                                                                            Vat = null
                                                                        },
                                                                    new ImportBookPostEntryResource
                                                                        {
                                                                            ImportBookId = this.impbookId,
                                                                            LineNumber = 2,
                                                                            EntryCodePrefix = "DL",
                                                                            EntryCode = "code blanc",
                                                                            EntryDate = this.now.AddDays(-6)
                                                                                .ToString("o"),
                                                                            Reference = "hocus pocus",
                                                                            Duty = 33,
                                                                            Vat = 44
                                                                        }
                                                                }
                                };
            
            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldCallReturnCreatedResult()
        {
            this.result.Should().BeOfType<CreatedResult<ImportBook>>();
        }

        [Test]
        public void ShouldCallDomainWithRightData()
        {
            var dataResult = ((CreatedResult<ImportBook>)this.result).Data;
            dataResult.DateCreated.IsSameOrEqualTo(this.now.AddDays(-10));
            dataResult.ParcelNumber.Should().Be(this.resource.ParcelNumber);
            dataResult.SupplierId.Should().Be(this.resource.SupplierId);
            dataResult.ForeignCurrency.Should().Be(this.resource.ForeignCurrency);
            dataResult.Currency.Should().Be(this.resource.Currency);
            dataResult.CarrierId.Should().Be(this.resource.CarrierId);
            dataResult.OldArrivalPort.Should().Be(this.resource.OldArrivalPort);
            dataResult.FlightNumber.Should().Be(this.resource.FlightNumber);
            dataResult.TransportId.Should().Be(this.resource.TransportId);
            dataResult.TransportBillNumber.Should().Be(this.resource.TransportBillNumber);
            dataResult.TransactionId.Should().Be(this.resource.TransactionId);
            dataResult.DeliveryTermCode.Should().Be(this.resource.DeliveryTermCode);
            dataResult.ArrivalPort.Should().Be("LAZ");
            dataResult.LineVatTotal.Should().Be(12);
            dataResult.DeliveryTermCode.Should().Be(this.resource.DeliveryTermCode);
            dataResult.ArrivalPort.Should().Be(this.resource.ArrivalPort);
            dataResult.LineVatTotal.Should().Be(this.resource.LineVatTotal);
            dataResult.Hwb.Should().Be(this.resource.Hwb);
            dataResult.SupplierCostCurrency.Should().Be(this.resource.SupplierCostCurrency);
            dataResult.TransNature.Should().Be(this.resource.TransNature);
            dataResult.ArrivalDate.Should().Be(this.now.AddDays(3));
            dataResult.FreightCharges.Should().Be(this.resource.FreightCharges);
            dataResult.HandlingCharge.Should().Be(this.resource.HandlingCharge);
            dataResult.ClearanceCharge.Should().Be(this.resource.ClearanceCharge);
            dataResult.Cartage.Should().Be(this.resource.Cartage);
            dataResult.Duty.Should().Be(this.resource.Duty);
            dataResult.Vat.Should().Be(this.resource.Vat);
            dataResult.Misc.Should().Be(this.resource.Misc);
            dataResult.CarriersInvTotal.Should().Be(this.resource.CarriersInvTotal);
            dataResult.CarriersVatTotal.Should().Be(this.resource.CarriersVatTotal);
            dataResult.TotalImportValue.Should().Be(this.resource.TotalImportValue);
            dataResult.Pieces.Should().Be(this.resource.Pieces);
            dataResult.Weight.Should().Be(this.resource.Weight);
            dataResult.CustomsEntryCode.Should().Be(this.resource.CustomsEntryCode);
            dataResult.CustomsEntryCodeDate.Should().Be(this.now.AddDays(2));
            dataResult.LinnDuty.Should().Be(this.resource.LinnDuty);
            dataResult.LinnVat.Should().Be(this.resource.LinnVat);
            dataResult.IprCpcNumber.Should().Be(this.resource.IprCpcNumber);
            dataResult.EecgNumber.Should().Be(this.resource.EecgNumber);
            dataResult.DateCancelled.Should().Be(this.now.AddDays(5));
            dataResult.CancelledBy.Should().Be(33105);
            dataResult.CancelledReason.Should().Be("cancel");
            dataResult.CarrierInvNumber.Should().Be("inv123");
            dataResult.CarrierInvDate.Should().Be(this.now.AddDays(3));
            dataResult.CountryOfOrigin.Should().Be(this.resource.CountryOfOrigin);
            dataResult.FcName.Should().Be(this.resource.FcName);
            dataResult.VaxRef.Should().Be(this.resource.VaxRef);
            dataResult.Storage.Should().Be(this.resource.Storage);
            dataResult.NumCartons.Should().Be(this.resource.NumCartons);
            dataResult.NumPallets.Should().Be(this.resource.NumPallets);
            dataResult.Comments.Should().Be(this.resource.Comments);
            dataResult.ExchangeRate.Should().Be(this.resource.ExchangeRate);
            dataResult.ExchangeCurrency.Should().Be(this.resource.ExchangeCurrency);
            dataResult.BaseCurrency.Should().Be(this.resource.BaseCurrency);
            dataResult.PeriodNumber.Should().Be(this.resource.PeriodNumber);
            dataResult.CreatedBy.Should().Be(this.resource.CreatedBy);
            dataResult.PortCode.Should().Be(this.resource.PortCode);
            dataResult.CustomsEntryCodePrefix.Should().Be(this.resource.CustomsEntryCodePrefix);

            var firstResourceInvDetail = this.resource.ImportBookInvoiceDetails.FirstOrDefault(x => x.LineNumber == 1);
            var secondResourceInvDetail = this.resource.ImportBookInvoiceDetails.FirstOrDefault(x => x.LineNumber == 2);

            var firstInvDetail = dataResult.InvoiceDetails.FirstOrDefault(x => x.LineNumber == 1);
            var secondInvDetail = dataResult.InvoiceDetails.FirstOrDefault(x => x.LineNumber == 2);

            firstInvDetail.ImportBookId.Should().Be(this.impbookId);
            firstInvDetail.InvoiceNumber.Should().Be(firstResourceInvDetail.InvoiceNumber);
            firstInvDetail.LineNumber.Should().Be(firstResourceInvDetail.LineNumber);
            firstInvDetail.InvoiceValue.Should().Be(firstInvDetail.InvoiceValue);

            secondInvDetail.ImportBookId.Should().Be(this.impbookId);
            secondInvDetail.InvoiceNumber.Should().Be(secondResourceInvDetail.InvoiceNumber);
            secondInvDetail.LineNumber.Should().Be(secondResourceInvDetail.LineNumber);
            secondInvDetail.InvoiceValue.Should().Be(secondResourceInvDetail.InvoiceValue);

            var firstResourceOrdDetail = this.resource.ImportBookOrderDetails.FirstOrDefault(x => x.LineNumber == 1);
            var secondResourceOrdDetail = this.resource.ImportBookOrderDetails.FirstOrDefault(x => x.LineNumber == 2);

            var firstOrdDetail = dataResult.OrderDetails.FirstOrDefault(x => x.LineNumber == 1);
            var secondOrdDetail = dataResult.OrderDetails.FirstOrDefault(x => x.LineNumber == 2);

            firstOrdDetail.ImportBookId.Should().Be(this.impbookId);
            firstOrdDetail.LineNumber.Should().Be(firstResourceOrdDetail.LineNumber);
            firstOrdDetail.OrderNumber.Should().Be(firstResourceOrdDetail.OrderNumber);
            firstOrdDetail.RsnNumber.Should().Be(firstResourceOrdDetail.RsnNumber);
            firstOrdDetail.OrderDescription.Should().Be(firstResourceOrdDetail.OrderDescription);
            firstOrdDetail.Qty.Should().Be(firstResourceOrdDetail.Qty);
            firstOrdDetail.DutyValue.Should().Be(firstResourceOrdDetail.DutyValue);
            firstOrdDetail.FreightValue.Should().Be(firstResourceOrdDetail.FreightValue);
            firstOrdDetail.VatValue.Should().Be(firstResourceOrdDetail.VatValue);
            firstOrdDetail.OrderValue.Should().Be(firstResourceOrdDetail.OrderValue);
            firstOrdDetail.Weight.Should().Be(firstResourceOrdDetail.Weight);
            firstOrdDetail.LoanNumber.Should().Be(firstResourceOrdDetail.LoanNumber);
            firstOrdDetail.LineType.Should().Be(firstResourceOrdDetail.LineType);
            firstOrdDetail.CpcNumber.Should().Be(firstResourceOrdDetail.CpcNumber);
            firstOrdDetail.TariffCode.Should().Be(firstResourceOrdDetail.TariffCode);
            firstOrdDetail.InsNumber.Should().Be(firstResourceOrdDetail.InsNumber);
            firstOrdDetail.VatRate.Should().Be(firstResourceOrdDetail.VatRate);

            secondOrdDetail.ImportBookId.Should().Be(this.impbookId);
            secondOrdDetail.LineNumber.Should().Be(secondResourceOrdDetail.LineNumber);
            secondOrdDetail.OrderNumber.Should().Be(secondResourceOrdDetail.OrderNumber);
            secondOrdDetail.RsnNumber.Should().Be(secondResourceOrdDetail.RsnNumber);
            secondOrdDetail.OrderDescription.Should().Be(secondResourceOrdDetail.OrderDescription);
            secondOrdDetail.Qty.Should().Be(secondResourceOrdDetail.Qty);
            secondOrdDetail.DutyValue.Should().Be(secondResourceOrdDetail.DutyValue);
            secondOrdDetail.FreightValue.Should().Be(secondResourceOrdDetail.FreightValue);
            secondOrdDetail.VatValue.Should().Be(secondResourceOrdDetail.VatValue);
            secondOrdDetail.OrderValue.Should().Be(secondResourceOrdDetail.OrderValue);
            secondOrdDetail.Weight.Should().Be(secondResourceOrdDetail.Weight);
            secondOrdDetail.LoanNumber.Should().Be(secondResourceOrdDetail.LoanNumber);
            secondOrdDetail.LineType.Should().Be(secondResourceOrdDetail.LineType);
            secondOrdDetail.CpcNumber.Should().Be(secondResourceOrdDetail.CpcNumber);
            secondOrdDetail.TariffCode.Should().Be(secondResourceOrdDetail.TariffCode);
            secondOrdDetail.InsNumber.Should().Be(secondResourceOrdDetail.InsNumber);
            secondOrdDetail.VatRate.Should().Be(secondResourceOrdDetail.VatRate);
     
            var firstResourcePostEntry = this.resource.ImportBookPostEntries.FirstOrDefault(x => x.LineNumber == 1);
            var secondResourcePostEntry = this.resource.ImportBookPostEntries.FirstOrDefault(x => x.LineNumber == 2);

            var firstPostEntry = dataResult.PostEntries.FirstOrDefault(x => x.LineNumber == 1);
            var secondPostEntry = dataResult.PostEntries.FirstOrDefault(x => x.LineNumber == 2);

            firstPostEntry.ImportBookId.Should().Be(this.impbookId);
            firstPostEntry.LineNumber.Should().Be(firstResourcePostEntry.LineNumber);
            firstPostEntry.EntryCodePrefix.Should().Be(firstResourcePostEntry.EntryCodePrefix);
            firstPostEntry.EntryCode.Should().Be(firstResourcePostEntry.EntryCode);
            firstPostEntry.EntryDate.Should().Be((DateTime?)null);
            firstPostEntry.Reference.Should().Be(firstResourcePostEntry.Reference);
            firstPostEntry.Duty.Should().Be(firstResourcePostEntry.Duty);
            firstPostEntry.Vat.Should().Be(firstResourcePostEntry.Vat);

            secondPostEntry.ImportBookId.Should().Be(this.impbookId);
            secondPostEntry.LineNumber.Should().Be(secondResourcePostEntry.LineNumber);
            secondPostEntry.EntryCodePrefix.Should().Be(secondResourcePostEntry.EntryCodePrefix);
            secondPostEntry.EntryCode.Should().Be(secondResourcePostEntry.EntryCode);
            secondPostEntry.EntryDate.Should().Be(this.now.AddDays(-6));
            secondPostEntry.Reference.Should().Be(secondResourcePostEntry.Reference);
            secondPostEntry.Duty.Should().Be(secondResourcePostEntry.Duty);
            secondPostEntry.Vat.Should().Be(secondResourcePostEntry.Vat);
        }
    }
}
