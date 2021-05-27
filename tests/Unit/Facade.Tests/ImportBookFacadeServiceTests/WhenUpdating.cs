namespace Linn.Stores.Facade.Tests.ImportBookFacadeTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            this.Sut.Update(this.impbookId, this.resource);
        }

        [Test]
        public void ShouldCallDomainWithRightData()
        {
            var to = new ImportBook()
                         {
                             Id = this.impbookId,
                             DateCreated = this.now.AddDays(2),
                             OrderDetails =
                                 new List<ImportBookOrderDetail>
                                     {
                                         new ImportBookOrderDetail
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
                             PostEntries = new List<ImportBookPostEntry>
                                               {
                                                   new ImportBookPostEntry
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
                                                   new ImportBookPostEntry
                                                       {
                                                           ImportBookId = this.impbookId,
                                                           LineNumber = 2,
                                                           EntryCodePrefix = "DL",
                                                           EntryCode = "code blanc",
                                                           EntryDate = this.now.AddDays(-6),
                                                           Reference = "hocus pocus",
                                                           Duty = 33,
                                                           Vat = 44
                                                       }
                                               }
                         };

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    x => x.ParcelNumber == 1 && x.SupplierId == 556 && x.ForeignCurrency == "YN" && x.Currency == "GBD"
                         && x.CarrierId == 678 && x.OldArrivalPort == "Glesga" && x.FlightNumber == "sk123"
                         && x.TransportId == 2 && x.TransportBillNumber == "1212" && x.TransactionId == 45
                         && x.DeliveryTermCode == "dli" && x.ArrivalPort == "LAZ" && x.LineVatTotal == 12
                         && x.Hwb == "hwbbb" && x.SupplierCostCurrency == "egg" && x.TransNature == "sea"
                         && x.ArrivalDate == this.now.AddDays(3) && x.FreightCharges == 11.1m
                         && x.HandlingCharge == 11.1m && x.ClearanceCharge == 11.1m && x.Cartage == 11.1m
                         && x.Duty == 11.1m && x.Vat == 11.1m && x.Misc == 11.1m && x.CarriersInvTotal == 11.1m
                         && x.CarriersVatTotal == 11.1m && x.TotalImportValue == 133.4m && x.Pieces == 1
                         && x.Weight == 11.1m && x.CustomsEntryCode == "code green"
                         && x.CustomsEntryCodeDate == this.now.AddDays(2) && x.LinnDuty == 12 && x.LinnVat == 11.1m
                         && x.IprCpcNumber == 1 && x.EecgNumber == 1 && x.DateCancelled == this.now.AddDays(5)
                         && x.CancelledBy == 33105 && x.CancelledReason == "cancel" && x.CarrierInvNumber == "inv123"
                         && x.CarrierInvDate == this.now.AddDays(3) && x.CountryOfOrigin == "DE" && x.FcName == "FC1"
                         && x.VaxRef == "VAX123" && x.Storage == 11.1m && x.NumCartons == 1 && x.NumPallets == 1
                         && x.Comments == "now closed" && x.ExchangeRate == 11.1m && x.ExchangeCurrency == "BB"
                         && x.BaseCurrency == "AA" && x.PeriodNumber == 47 && x.CreatedBy == 33105
                         && x.PortCode == "g74" && x.CustomsEntryCodePrefix == "AA"));
        }

        [Test]
        public void ShouldCallDomainWithInvoiceDetails()
        {
            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.InvoiceDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.InvoiceNumber == "123" && x.LineNumber == 1
                             && x.InvoiceValue == 12.5m)));

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.InvoiceDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.InvoiceNumber == "1234" && x.LineNumber == 2
                             && x.InvoiceValue == 155.2m)));
        }

        [Test]
        public void ShouldCallDomainWithOrderDetails()
        {
            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.OrderDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == 2 && x.OrderNumber == 13
                             && x.RsnNumber == 2 && x.OrderDescription == "palpatine final order" && x.Qty == 1
                             && x.DutyValue == 21.12m && x.FreightValue == 22.12m && x.VatValue == 3.12m
                             && x.OrderValue == 44.1m && x.Weight == 55.2m && x.LoanNumber == null
                             && x.LineType == "TYpe B" && x.CpcNumber == null && x.TariffCode == "121213"
                             && x.InsNumber == null && x.VatRate == null)));

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.OrderDetails.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == 1 && x.OrderNumber == 111
                             && x.RsnNumber == 222 && x.OrderDescription == "kylo ren first order" && x.Qty == 3
                             && x.DutyValue == 91.12m && x.FreightValue == 92.12m && x.VatValue == 93.12m
                             && x.OrderValue == 944.1m && x.Weight == 955.2m && x.LoanNumber == 999
                             && x.LineType == "Type C" && x.CpcNumber == 91 && x.TariffCode == "121213"
                             && x.InsNumber == 92 && x.VatRate == 93)));
        }

        [Test]
        public void ShouldCallDomainWithPostEntries()
        {
            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.PostEntries.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == 1 && x.EntryCodePrefix == "PR"
                             && x.EntryCode == "code blu" && x.EntryDate == null && x.Reference == "refer fence"
                             && x.Duty == null && x.Vat == null)));

            this.DomainService.Received().Update(
                this.from,
                Arg.Is<ImportBook>(
                    z => z.PostEntries.Any(
                        x => x.ImportBookId == this.impbookId && x.LineNumber == 2 && x.EntryCodePrefix == "DL"
                             && x.EntryCode == "code blanc" && x.EntryDate == this.now.AddDays(-6)
                             && x.Reference == "hocus pocus" && x.Duty == 33 && x.Vat == 44)));
        }
    }
}
