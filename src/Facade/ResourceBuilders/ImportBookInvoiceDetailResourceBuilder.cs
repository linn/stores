namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookInvoiceDetailResourceBuilder : IResourceBuilder<ImportBookInvoiceDetail>
    {
        public ImportBookInvoiceDetailResource Build(ImportBookInvoiceDetail model)
        {
            return new ImportBookInvoiceDetailResource
                       {
                           ImportBookId = model.ImpBookId,
                           LineNumber = model.LineNumber,
                           InvoiceNumber = model.InvoiceNumber,
                           InvoiceValue = model.InvoiceValue
                       };
        }

        object IResourceBuilder<ImportBookInvoiceDetail>.Build(ImportBookInvoiceDetail model) => this.Build(model);

        public string GetLocation(ImportBookInvoiceDetail model)
        {
            throw new System.NotImplementedException();
        }
    }
}