namespace Linn.Stores.Facade.Tests.ImportBookFacadeTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private readonly int impbookId = 54321;

        private readonly DateTime now = DateTime.Now;

        private ImportBookResource resource;

        private ImportBook from;

        private IResult<ImportBook> result;

        [SetUp]
        public void SetUp()
        {
            this.from = new ImportBook
                            {
                                Id = this.impbookId,
                                DateCreated = this.now.AddDays(-5),
                                ParcelNumber = null,
                                SupplierId = 555,
                                ForeignCurrency = string.Empty,
                                Currency = "GBP",
                                CarrierId = 678,
                                OldArrivalPort = "Glasscow",
                                FlightNumber = string.Empty,
                                TransportId = 1,
                                TransportBillNumber = string.Empty,
                                TransactionId = 44,
                                DeliveryTermCode = string.Empty,
                                ArrivalPort = "LAX",
                                LineVatTotal = null,
                                Hwb = "what is hwb",
                                SupplierCostCurrency = "bacon",
                                TransNature = "air",
                                ArrivalDate = null,
                                FreightCharges = null,
                                HandlingCharge = null,
                                ClearanceCharge = null,
                                Cartage = null,
                                Duty = null,
                                Vat = null,
                                Misc = null,
                                CarriersInvTotal = null,
                                CarriersVatTotal = null,
                                TotalImportValue = 123.4m,
                                Pieces = null,
                                Weight = null,
                                CustomsEntryCode = "code RED",
                                CustomsEntryCodeDate = null,
                                LinnDuty = null,
                                LinnVat = null,
                                IprCpcNumber = null,
                                EecgNumber = null,
                                DateCancelled = null,
                                CancelledBy = null,
                                CancelledReason = null,
                                CarrierInvNumber = null,
                                CarrierInvDate = null,
                                CountryOfOrigin = string.Empty,
                                FcName = string.Empty,
                                VaxRef = string.Empty,
                                Storage = null,
                                NumCartons = null,
                                NumPallets = null,
                                Comments = string.Empty,
                                ExchangeRate = null,
                                ExchangeCurrency = string.Empty,
                                BaseCurrency = string.Empty,
                                PeriodNumber = null,
                                CreatedBy = null,
                                PortCode = string.Empty,
                                CustomsEntryCodePrefix = "AA",
                                InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                                OrderDetails = new List<ImportBookOrderDetail>(),
                                PostEntries = new List<ImportBookPostEntry>()
                            };

            this.resource = new ImportBookResource()
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

            this.ImportBookRepository.FindById(Arg.Any<int>()).Returns(this.from);
            this.result = this.Sut.Update(this.impbookId, this.resource);
        }

        [Test]
        public void ShouldCallReturnSuccessResult()
        {
            this.result.Should().BeOfType<SuccessResult<ImportBook>>();
        }

        [Test]
        public void ShouldCallDomainWithRightData()
        {
            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    x => x.ParcelNumber == this.resource.ParcelNumber && x.SupplierId == this.resource.SupplierId
                                                                      && x.ForeignCurrency
                                                                      == this.resource.ForeignCurrency
                                                                      && x.Currency == this.resource.Currency
                                                                      && x.CarrierId == this.resource.CarrierId
                                                                      && x.OldArrivalPort
                                                                      == this.resource.OldArrivalPort
                                                                      && x.FlightNumber == this.resource.FlightNumber
                                                                      && x.TransportId == this.resource.TransportId
                                                                      && x.TransportBillNumber
                                                                      == this.resource.TransportBillNumber
                                                                      && x.TransactionId == this.resource.TransactionId
                                                                      && x.DeliveryTermCode
                                                                      == this.resource.DeliveryTermCode
                                                                      && x.ArrivalPort == "LAZ" && x.LineVatTotal == 12
                                                                      && x.DeliveryTermCode
                                                                      == this.resource.DeliveryTermCode
                                                                      && x.ArrivalPort == this.resource.ArrivalPort
                                                                      && x.LineVatTotal == this.resource.LineVatTotal
                                                                      && x.Hwb == this.resource.Hwb
                                                                      && x.SupplierCostCurrency
                                                                      == this.resource.SupplierCostCurrency
                                                                      && x.TransNature == this.resource.TransNature
                                                                      && x.ArrivalDate == this.now.AddDays(3)
                                                                      && x.FreightCharges
                                                                      == this.resource.FreightCharges
                                                                      && x.HandlingCharge
                                                                      == this.resource.HandlingCharge
                                                                      && x.ClearanceCharge
                                                                      == this.resource.ClearanceCharge
                                                                      && x.Cartage == this.resource.Cartage
                                                                      && x.Duty == this.resource.Duty
                                                                      && x.Vat == this.resource.Vat
                                                                      && x.Misc == this.resource.Misc
                                                                      && x.CarriersInvTotal
                                                                      == this.resource.CarriersInvTotal
                                                                      && x.CarriersVatTotal
                                                                      == this.resource.CarriersVatTotal
                                                                      && x.TotalImportValue
                                                                      == this.resource.TotalImportValue
                                                                      && x.Pieces == this.resource.Pieces
                                                                      && x.Weight == this.resource.Weight
                                                                      && x.CustomsEntryCode
                                                                      == this.resource.CustomsEntryCode
                                                                      && x.CustomsEntryCodeDate == this.now.AddDays(2)
                                                                      && x.LinnDuty == this.resource.LinnDuty
                                                                      && x.LinnVat == this.resource.LinnVat
                                                                      && x.IprCpcNumber == this.resource.IprCpcNumber
                                                                      && x.EecgNumber == this.resource.EecgNumber
                                                                      && x.DateCancelled == this.now.AddDays(5)
                                                                      && x.CancelledBy == 33105
                                                                      && x.CancelledReason == "cancel"
                                                                      && x.CarrierInvNumber == "inv123"
                                                                      && x.CarrierInvDate == this.now.AddDays(3)
                                                                      && x.CountryOfOrigin
                                                                      == this.resource.CountryOfOrigin
                                                                      && x.FcName == this.resource.FcName
                                                                      && x.VaxRef == this.resource.VaxRef
                                                                      && x.Storage == this.resource.Storage
                                                                      && x.NumCartons == this.resource.NumCartons
                                                                      && x.NumPallets == this.resource.NumPallets
                                                                      && x.Comments == this.resource.Comments
                                                                      && x.ExchangeRate == this.resource.ExchangeRate
                                                                      && x.ExchangeCurrency
                                                                      == this.resource.ExchangeCurrency
                                                                      && x.BaseCurrency == this.resource.BaseCurrency
                                                                      && x.PeriodNumber == this.resource.PeriodNumber
                                                                      && x.CreatedBy == this.resource.CreatedBy
                                                                      && x.PortCode == this.resource.PortCode
                                                                      && x.CustomsEntryCodePrefix
                                                                      == this.resource.CustomsEntryCodePrefix));
        }

        [Test]
        public void ShouldCallDomainWithInvoiceDetails()
        {
            var firstResource = this.resource.ImportBookInvoiceDetails.FirstOrDefault(x => x.LineNumber == 1);
            var secondResource = this.resource.ImportBookInvoiceDetails.FirstOrDefault(x => x.LineNumber == 2);

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.InvoiceDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.InvoiceNumber == firstResource.InvoiceNumber
                                                              && x.LineNumber == firstResource.LineNumber
                                                              && x.InvoiceValue == firstResource.InvoiceValue)));

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.InvoiceDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.InvoiceNumber == secondResource.InvoiceNumber
                                                              && x.LineNumber == secondResource.LineNumber
                                                              && x.InvoiceValue == secondResource.InvoiceValue)));
        }

        [Test]
        public void ShouldCallDomainWithOrderDetails()
        {
            var firstResource = this.resource.ImportBookOrderDetails.FirstOrDefault(x => x.LineNumber == 1);
            var secondResource = this.resource.ImportBookOrderDetails.FirstOrDefault(x => x.LineNumber == 2);

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.OrderDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == firstResource.LineNumber
                                                              && x.OrderNumber == firstResource.OrderNumber
                                                              && x.RsnNumber == firstResource.RsnNumber
                                                              && x.OrderDescription == firstResource.OrderDescription
                                                              && x.Qty == firstResource.Qty
                                                              && x.DutyValue == firstResource.DutyValue
                                                              && x.FreightValue == firstResource.FreightValue
                                                              && x.VatValue == firstResource.VatValue
                                                              && x.OrderValue == firstResource.OrderValue
                                                              && x.Weight == firstResource.Weight
                                                              && x.LoanNumber == firstResource.LoanNumber
                                                              && x.LineType == firstResource.LineType
                                                              && x.CpcNumber == firstResource.CpcNumber
                                                              && x.TariffCode == firstResource.TariffCode
                                                              && x.InsNumber == firstResource.InsNumber
                                                              && x.VatRate == firstResource.VatRate)));

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.OrderDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == secondResource.LineNumber
                                                              && x.OrderNumber == secondResource.OrderNumber
                                                              && x.RsnNumber == secondResource.RsnNumber
                                                              && x.OrderDescription == secondResource.OrderDescription
                                                              && x.Qty == secondResource.Qty
                                                              && x.DutyValue == secondResource.DutyValue
                                                              && x.FreightValue == secondResource.FreightValue
                                                              && x.VatValue == secondResource.VatValue
                                                              && x.OrderValue == secondResource.OrderValue
                                                              && x.Weight == secondResource.Weight
                                                              && x.LoanNumber == secondResource.LoanNumber
                                                              && x.LineType == secondResource.LineType
                                                              && x.CpcNumber == secondResource.CpcNumber
                                                              && x.TariffCode == secondResource.TariffCode
                                                              && x.InsNumber == secondResource.InsNumber
                                                              && x.VatRate == secondResource.VatRate)));
        }

        [Test]
        public void ShouldCallDomainWithPostEntries()
        {
            var firstResource = this.resource.ImportBookPostEntries.FirstOrDefault(x => x.LineNumber == 1);
            var secondResource = this.resource.ImportBookPostEntries.FirstOrDefault(x => x.LineNumber == 2);

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.PostEntries.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == firstResource.LineNumber
                                                              && x.EntryCodePrefix == firstResource.EntryCodePrefix
                                                              && x.EntryCode == firstResource.EntryCode
                                                              && x.EntryDate == (DateTime?)null
                                                              && x.Reference == firstResource.Reference
                                                              && x.Duty == firstResource.Duty
                                                              && x.Vat == firstResource.Vat)));

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.PostEntries.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == secondResource.LineNumber
                                                              && x.EntryCodePrefix == secondResource.EntryCodePrefix
                                                              && x.EntryCode == secondResource.EntryCode
                                                              && x.EntryDate == this.now.AddDays(-6)
                                                              && x.Reference == secondResource.Reference
                                                              && x.Duty == secondResource.Duty
                                                              && x.Vat == secondResource.Vat)));
        }
    }
}
