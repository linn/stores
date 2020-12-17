namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookInvoiceDetailResourceBuilder : IResourceBuilder<ImpBookInvoiceDetail>
    {
        public ImportBookInvoiceDetailResource Build(ImpBookInvoiceDetail model)
        {
            return new ImportBookInvoiceDetailResource
                       {
                           ImportBookId = model.ImpBookId,
                           LineNumber = model.LineNumber,
                           InvoiceNumber = model.InvoiceNumber,
                           InvoiceValue = model.InvoiceValue
                       };
        }

        object IResourceBuilder<ImpBookInvoiceDetail>.Build(ImpBookInvoiceDetail model) => this.Build(model);

        public string GetLocation(ImpBookInvoiceDetail model)
        {
            throw new System.NotImplementedException();
        }
    }
}