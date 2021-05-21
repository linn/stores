namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private readonly int impbookId = 12007;
        private ImportBook impbook;
        private ImportBook newImpBook;
        private readonly DateTime now = DateTime.Now;


        [SetUp]
        public void SetUp()
        {
            this.impbook = new ImportBook
            {
                Id = this.impbookId,
                DateCreated = now.AddDays(-5),
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
                TotalImportValue = (decimal)123.4,
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

            this.newImpBook = new ImportBook
            {
                Id = this.impbookId,
                DateCreated = now.AddDays(2),
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
                ArrivalDate = this.now.AddDays(3),
                FreightCharges = (decimal) 11.1,
                HandlingCharge = (decimal)11.1,
                ClearanceCharge = (decimal)11.1,
                Cartage = (decimal)11.1,
                Duty = (decimal)11.1,
                Vat = (decimal)11.1,
                Misc = (decimal)11.1,
                CarriersInvTotal = (decimal)11.1,
                CarriersVatTotal = (decimal)11.1,
                TotalImportValue = (decimal)133.4,
                Pieces = 1,
                Weight = (decimal)11.1,
                CustomsEntryCode = "code green",
                CustomsEntryCodeDate = this.now.AddDays(2),
                LinnDuty = (decimal) 12,
                LinnVat = (decimal)11.1,
                IprCpcNumber = 1,
                EecgNumber = 1,
                DateCancelled = this.now.AddDays(5),
                CancelledBy = 33105,
                CancelledReason = "cancel",
                CarrierInvNumber = "inv123",
                CarrierInvDate = this.now.AddDays(3),
                CountryOfOrigin = "DE",
                FcName = "FC1",
                VaxRef = "VAX123",
                Storage = (decimal)11.1,
                NumCartons = 1,
                NumPallets = 1,
                Comments = "now closed",
                ExchangeRate = (decimal)11.1,
                ExchangeCurrency = "BB",
                BaseCurrency = "AA",
                PeriodNumber = 47,
                CreatedBy = 33105,
                PortCode = "g74",
                CustomsEntryCodePrefix = "AA",
                InvoiceDetails = new List<ImportBookInvoiceDetail>(),
                OrderDetails = new List<ImportBookOrderDetail>(),
                PostEntries = new List<ImportBookPostEntry>()
            };


            this.Sut.Update(this.impbook, this.newImpBook);
        }

        [Test]
        public void ShouldHaveUpdatedAllFieldsOnOriginalImportBook()
        {
            this.impbook.Id.Equals(this.impbookId).Should().Be(true);
            this.impbook.ParcelNumber.Equals(this.newImpBook.ParcelNumber).Should().Be(true);
            this.impbook.DateCreated.Equals(this.newImpBook.DateCreated).Should().Be(true);
            this.impbook.SupplierId.Equals(this.newImpBook.SupplierId).Should().Be(true);
            this.impbook.ForeignCurrency.Equals(this.newImpBook.ForeignCurrency).Should().Be(true);
            this.impbook.Currency.Equals(this.newImpBook.Currency).Should().Be(true);
            this.impbook.CarrierId.Equals(this.newImpBook.CarrierId).Should().Be(true);
            this.impbook.OldArrivalPort.Equals(this.newImpBook.OldArrivalPort).Should().Be(true);
            this.impbook.FlightNumber.Equals(this.newImpBook.FlightNumber).Should().Be(true);
            this.impbook.TransportId.Equals(this.newImpBook.TransportId).Should().Be(true);
            this.impbook.TransportBillNumber.Equals(this.newImpBook.TransportBillNumber).Should().Be(true);
            this.impbook.TransactionId.Equals(this.newImpBook.TransactionId).Should().Be(true);
            this.impbook.DeliveryTermCode.Equals(this.newImpBook.DeliveryTermCode).Should().Be(true);
            this.impbook.ArrivalPort.Equals(this.newImpBook.ArrivalPort).Should().Be(true);
            this.impbook.LineVatTotal.Equals(this.newImpBook.LineVatTotal).Should().Be(true);
            this.impbook.Hwb.Equals(this.newImpBook.Hwb).Should().Be(true);
            this.impbook.SupplierCostCurrency.Equals(this.newImpBook.SupplierCostCurrency).Should().Be(true);
            this.impbook.TransNature.Equals(this.newImpBook.TransNature).Should().Be(true);
            this.impbook.ArrivalDate.Equals(this.newImpBook.ArrivalDate).Should().Be(true);
            this.impbook.FreightCharges.Equals(this.newImpBook.FreightCharges).Should().Be(true);
            this.impbook.HandlingCharge.Equals(this.newImpBook.HandlingCharge).Should().Be(true);
            this.impbook.ClearanceCharge.Equals(this.newImpBook.ClearanceCharge).Should().Be(true);
            this.impbook.Cartage.Equals(this.newImpBook.Cartage).Should().Be(true);
            this.impbook.Duty.Equals(this.newImpBook.Duty).Should().Be(true);
            this.impbook.Vat.Equals(this.newImpBook.Vat).Should().Be(true);
            this.impbook.Misc.Equals(this.newImpBook.Misc).Should().Be(true);
            this.impbook.CarriersInvTotal.Equals(this.newImpBook.CarriersInvTotal).Should().Be(true);
            this.impbook.CarriersVatTotal.Equals(this.newImpBook.CarriersVatTotal).Should().Be(true);
            this.impbook.TotalImportValue.Equals(this.newImpBook.TotalImportValue).Should().Be(true);
            this.impbook.Pieces.Equals(this.newImpBook.Pieces).Should().Be(true);
            this.impbook.Weight.Equals(this.newImpBook.Weight).Should().Be(true);
            this.impbook.CustomsEntryCode.Equals(this.newImpBook.CustomsEntryCode).Should().Be(true);
            this.impbook.CustomsEntryCodeDate.Equals(this.newImpBook.CustomsEntryCodeDate).Should().Be(true);
            this.impbook.LinnDuty.Equals(this.newImpBook.LinnDuty).Should().Be(true);
            this.impbook.LinnVat.Equals(this.newImpBook.LinnVat).Should().Be(true);
            this.impbook.IprCpcNumber.Equals(this.newImpBook.IprCpcNumber).Should().Be(true);
            this.impbook.EecgNumber.Equals(this.newImpBook.EecgNumber).Should().Be(true);
            this.impbook.DateCancelled.Equals(this.newImpBook.DateCancelled).Should().Be(true);
            this.impbook.CancelledBy.Equals(this.newImpBook.CancelledBy).Should().Be(true);
            this.impbook.CancelledReason.Equals(this.newImpBook.CancelledReason).Should().Be(true);
            this.impbook.CarrierInvNumber.Equals(this.newImpBook.CarrierInvNumber).Should().Be(true);
            this.impbook.CarrierInvDate.Equals(this.newImpBook.CarrierInvDate).Should().Be(true);
            this.impbook.CountryOfOrigin.Equals(this.newImpBook.CountryOfOrigin).Should().Be(true);
            this.impbook.FcName.Equals(this.newImpBook.FcName).Should().Be(true);
            this.impbook.VaxRef.Equals(this.newImpBook.VaxRef).Should().Be(true);
            this.impbook.Storage.Equals(this.newImpBook.Storage).Should().Be(true);
            this.impbook.NumCartons.Equals(this.newImpBook.NumCartons).Should().Be(true);
            this.impbook.NumPallets.Equals(this.newImpBook.NumPallets).Should().Be(true);
            this.impbook.Comments.Equals(this.newImpBook.Comments).Should().Be(true);
            this.impbook.ExchangeRate.Equals(this.newImpBook.ExchangeRate).Should().Be(true);
            this.impbook.ExchangeCurrency.Equals(this.newImpBook.ExchangeCurrency).Should().Be(true);
            this.impbook.BaseCurrency.Equals(this.newImpBook.BaseCurrency).Should().Be(true);
            this.impbook.PeriodNumber.Equals(this.newImpBook.PeriodNumber).Should().Be(true);
            this.impbook.CreatedBy.Equals(this.newImpBook.CreatedBy).Should().Be(true);
            this.impbook.PortCode.Equals(this.newImpBook.PortCode).Should().Be(true);
            this.impbook.CustomsEntryCodePrefix.Equals(this.newImpBook.CustomsEntryCodePrefix).Should().Be(true);
        }
    }
}
