﻿namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookFacadeService : FacadeService<ImportBook, int, ImportBookResource, ImportBookResource>,
                                           IImportBookFacadeService
    {
        private readonly IDatabaseService databaseService;

        private readonly IImportBookService importBookService;

        public ImportBookFacadeService(
            IRepository<ImportBook, int> repository,
            ITransactionManager transactionManager,
            IImportBookService importBookService,
            IDatabaseService databaseService)
            : base(repository, transactionManager)
        {
            this.importBookService = importBookService;
            this.databaseService = databaseService;
        }

        public IResult<ProcessResult> PostDuty(PostDutyResource resource)
        {
            var orderDetails = new List<ImportBookOrderDetail>();
            foreach (var detail in resource.OrderDetails)
            {
                orderDetails.Add(
                    new ImportBookOrderDetail
                        {
                            ImportBookId = resource.ImpbookId,
                            LineNumber = detail.LineNumber,
                            OrderNumber = detail.OrderNumber,
                            RsnNumber = detail.RsnNumber,
                            OrderDescription = detail.OrderDescription,
                            Qty = detail.Qty,
                            DutyValue = detail.DutyValue,
                            FreightValue = detail.FreightValue,
                            VatValue = detail.VatValue,
                            OrderValue = detail.OrderValue,
                            Weight = detail.Weight,
                            LoanNumber = detail.LoanNumber,
                            LineType = detail.LineType,
                            CpcNumber = detail.CpcNumber,
                            TariffCode = detail.TariffCode,
                            InsNumber = detail.InsNumber,
                            VatRate = detail.VatRate,
                            PostDuty = detail.PostDuty
                        });
            }

            try
            {
                var result = this.importBookService.PostDutyForOrderDetails(
                    orderDetails,
                    resource.SupplierId,
                    resource.CurrentUserNumber,
                    DateTime.Parse(resource.DatePosted));

                return new SuccessResult<ProcessResult>(result);
            }
            catch (PostDutyException e)
            {
                return new BadRequestResult<ProcessResult>(e.Message);
            }
            catch (Exception e)
            {
                return new BadRequestResult<ProcessResult>(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

        protected override ImportBook CreateFromResource(ImportBookResource resource)
        {
            var newImportBook = new ImportBook
                                    {
                                        Id = this.databaseService.GetNextVal("IMPBOOK_SEQ"),
                                        DateCreated = DateTime.Parse(resource.DateCreated),
                                        ParcelNumber = resource.ParcelNumber,
                                        SupplierId = resource.SupplierId,
                                        ForeignCurrency = resource.ForeignCurrency,
                                        Currency = resource.Currency,
                                        CarrierId = resource.CarrierId,
                                        TransportId = resource.TransportId,
                                        TransportBillNumber = resource.TransportBillNumber,
                                        TransactionId = resource.TransactionId,
                                        DeliveryTermCode = resource.DeliveryTermCode,
                                        ArrivalPort = resource.ArrivalPort,
                                        ArrivalDate =
                                            string.IsNullOrWhiteSpace(resource.ArrivalDate)
                                                ? (DateTime?) null
                                                : DateTime.Parse(resource.ArrivalDate),
                                        TotalImportValue = resource.TotalImportValue,
                                        Weight = resource.Weight,
                                        CustomsEntryCode = resource.CustomsEntryCode,
                                        CustomsEntryCodeDate =
                                            string.IsNullOrWhiteSpace(resource.CustomsEntryCodeDate)
                                                ? (DateTime?)null
                                                : DateTime.Parse(resource.CustomsEntryCodeDate),
                                        LinnDuty = resource.LinnDuty,
                                        LinnVat = resource.LinnVat,
                                        DateCancelled =
                                            string.IsNullOrWhiteSpace(resource.DateCancelled)
                                                ? (DateTime?) null
                                                : DateTime.Parse(resource.DateCancelled),
                                        CancelledBy = resource.CancelledBy,
                                        CancelledReason = resource.CancelledReason,
                                        NumCartons = resource.NumCartons,
                                        NumPallets = resource.NumPallets,
                                        Comments = resource.Comments,
                                        CreatedBy = resource.CreatedBy,
                                        CustomsEntryCodePrefix = resource.CustomsEntryCodePrefix,
                                        Pva = resource.Pva
                                    };

            var invoiceDetails = new List<ImportBookInvoiceDetail>();
            foreach (var detail in resource.ImportBookInvoiceDetails)
            {
                invoiceDetails.Add(
                    new ImportBookInvoiceDetail
                        {
                            ImportBookId = resource.Id,
                            LineNumber = detail.LineNumber,
                            InvoiceNumber = detail.InvoiceNumber,
                            InvoiceValue = detail.InvoiceValue
                        });
            }

            newImportBook.InvoiceDetails = invoiceDetails;

            var orderDetails = new List<ImportBookOrderDetail>();
            foreach (var detail in resource.ImportBookOrderDetails)
            {
                orderDetails.Add(
                    new ImportBookOrderDetail
                        {
                            ImportBookId = resource.Id,
                            LineNumber = detail.LineNumber,
                            OrderNumber = detail.OrderNumber,
                            RsnNumber = detail.RsnNumber,
                            OrderDescription = detail.OrderDescription,
                            Qty = detail.Qty,
                            DutyValue = detail.DutyValue,
                            FreightValue = detail.FreightValue,
                            VatValue = detail.VatValue,
                            OrderValue = detail.OrderValue,
                            Weight = detail.Weight,
                            LoanNumber = detail.LoanNumber,
                            LineType = detail.LineType,
                            CpcNumber = detail.CpcNumber,
                            TariffCode = detail.TariffCode,
                            InsNumber = detail.InsNumber,
                            VatRate = detail.VatRate
                        });
            }

            newImportBook.OrderDetails = orderDetails;

            var postEntries = new List<ImportBookPostEntry>();
            foreach (var entry in resource.ImportBookPostEntries)
            {
                postEntries.Add(
                    new ImportBookPostEntry
                        {
                            ImportBookId = resource.Id,
                            LineNumber = entry.LineNumber,
                            EntryCodePrefix = entry.EntryCodePrefix,
                            EntryCode = entry.EntryCode,
                            EntryDate = string.IsNullOrWhiteSpace(entry.EntryDate)
                                            ? (DateTime?) null
                                            : DateTime.Parse(entry.EntryDate),
                            Reference = entry.Reference,
                            Duty = entry.Duty,
                            Vat = entry.Vat
                        });
            }

            newImportBook.PostEntries = postEntries;

            return newImportBook;
        }

        protected override Expression<Func<ImportBook, bool>> SearchExpression(string searchTerm)
        {
            return imps => imps.Id.ToString().Contains(searchTerm);
        }

        protected override void UpdateFromResource(ImportBook entity, ImportBookResource updateResource)
        {
            var newImportBook = new ImportBook
                                    {
                                        ParcelNumber = updateResource.ParcelNumber,
                                        SupplierId = updateResource.SupplierId,
                                        ForeignCurrency = updateResource.ForeignCurrency,
                                        Currency = updateResource.Currency,
                                        CarrierId = updateResource.CarrierId,
                                        TransportId = updateResource.TransportId,
                                        TransportBillNumber = updateResource.TransportBillNumber,
                                        TransactionId = updateResource.TransactionId,
                                        DeliveryTermCode = updateResource.DeliveryTermCode,
                                        ArrivalPort = updateResource.ArrivalPort,
                                        ArrivalDate =
                                            string.IsNullOrWhiteSpace(updateResource.ArrivalDate)
                                                ? (DateTime?) null
                                                : DateTime.Parse(updateResource.ArrivalDate),
                                        TotalImportValue = updateResource.TotalImportValue,
                                        Weight = updateResource.Weight,
                                        CustomsEntryCode = updateResource.CustomsEntryCode,
                                        CustomsEntryCodeDate =
                                            string.IsNullOrWhiteSpace(updateResource.CustomsEntryCodeDate)
                                                ? (DateTime?) null
                                                : DateTime.Parse(updateResource.CustomsEntryCodeDate),
                                        LinnDuty = updateResource.LinnDuty,
                                        LinnVat = updateResource.LinnVat,
                                        DateCancelled =
                                            string.IsNullOrWhiteSpace(updateResource.DateCancelled)
                                                ? (DateTime?) null
                                                : DateTime.Parse(updateResource.DateCancelled),
                                        CancelledBy = updateResource.CancelledBy,
                                        CancelledReason = updateResource.CancelledReason,
                                        NumCartons = updateResource.NumCartons,
                                        NumPallets = updateResource.NumPallets,
                                        Comments = updateResource.Comments,
                                        CreatedBy = updateResource.CreatedBy,
                                        CustomsEntryCodePrefix = updateResource.CustomsEntryCodePrefix,
                                        Pva = updateResource.Pva
                                    };

            var invoiceDetails = new List<ImportBookInvoiceDetail>();
            foreach (var detail in updateResource.ImportBookInvoiceDetails)
            {
                invoiceDetails.Add(
                    new ImportBookInvoiceDetail
                        {
                            ImportBookId = updateResource.Id,
                            LineNumber = detail.LineNumber,
                            InvoiceNumber = detail.InvoiceNumber,
                            InvoiceValue = detail.InvoiceValue
                        });
            }

            newImportBook.InvoiceDetails = invoiceDetails;

            var orderDetails = new List<ImportBookOrderDetail>();
            foreach (var detail in updateResource.ImportBookOrderDetails)
            {
                orderDetails.Add(
                    new ImportBookOrderDetail
                        {
                            ImportBookId = updateResource.Id,
                            LineNumber = detail.LineNumber,
                            OrderNumber = detail.OrderNumber,
                            RsnNumber = detail.RsnNumber,
                            OrderDescription = detail.OrderDescription,
                            Qty = detail.Qty,
                            DutyValue = detail.DutyValue,
                            FreightValue = detail.FreightValue,
                            VatValue = detail.VatValue,
                            OrderValue = detail.OrderValue,
                            Weight = detail.Weight,
                            LoanNumber = detail.LoanNumber,
                            LineType = detail.LineType,
                            CpcNumber = detail.CpcNumber,
                            TariffCode = detail.TariffCode,
                            InsNumber = detail.InsNumber,
                            VatRate = detail.VatRate
                        });
            }

            newImportBook.OrderDetails = orderDetails;

            var postEntries = new List<ImportBookPostEntry>();
            foreach (var entry in updateResource.ImportBookPostEntries)
            {
                postEntries.Add(
                    new ImportBookPostEntry
                        {
                            ImportBookId = updateResource.Id,
                            LineNumber = entry.LineNumber,
                            EntryCodePrefix = entry.EntryCodePrefix,
                            EntryCode = entry.EntryCode,
                            EntryDate = string.IsNullOrWhiteSpace(entry.EntryDate)
                                            ? (DateTime?) null
                                            : DateTime.Parse(entry.EntryDate),
                            Reference = entry.Reference,
                            Duty = entry.Duty,
                            Vat = entry.Vat
                        });
            }

            newImportBook.PostEntries = postEntries;

            this.importBookService.Update(entity, newImportBook);
        }
    }
}
