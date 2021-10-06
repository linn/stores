namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;
    using Linn.Stores.Resources;

    public class InterCompanyInvoiceResourceBuilder : IResourceBuilder<InterCompanyInvoice>
    {
        public IntercompanyInvoiceResource Build(InterCompanyInvoice invoice)
        {
            var addressBuilder = new AddressResourceBuilder();
            return new IntercompanyInvoiceResource
            {
                DeliveryAddress = invoice.DeliveryAddress == null ? null : addressBuilder.Build(invoice.DeliveryAddress),
                DocumentType = invoice.DocumentType,
                DocumentNumber = invoice.DocumentNumber,
                DocumentDate = invoice.DocumentDate.ToString("o"),
                ExportReturnId = invoice.ExportReturnId,
                AccountId = invoice.SalesAccountId,
                NetTotal = invoice.NetTotal,
                VATTotal = invoice.VATTotal,
                DocumentTotal = invoice.DocumentTotal,
                Currency = invoice.CurrencyCode,
                GrossWeightKg = invoice.GrossWeightKG,
                GrossDimsM3 = invoice.GrossDimsM3,
                Terms = invoice.Terms,
                ConsignmentId = invoice.ConsignmentId,
                InvoiceAddress = invoice.InvoiceAddress == null ? null : addressBuilder.Build(invoice.InvoiceAddress),
            };
        }

        public string GetLocation(InterCompanyInvoice model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<InterCompanyInvoice>.Build(InterCompanyInvoice invoice) => this.Build(invoice);
    }
}