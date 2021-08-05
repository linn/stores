namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookInvoiceDetailsResourceBuilder : IResourceBuilder<IEnumerable<ImportBookInvoiceDetail>>
    {
        private readonly ImportBookInvoiceDetailResourceBuilder importBookInvoiceDetailResourceBuilder =
            new ImportBookInvoiceDetailResourceBuilder();

        public IEnumerable<ImportBookInvoiceDetailResource> Build(IEnumerable<ImportBookInvoiceDetail> details)
        {
            return details.OrderBy(b => b.LineNumber).Select(a => this.importBookInvoiceDetailResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookInvoiceDetail>>.Build(IEnumerable<ImportBookInvoiceDetail> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookInvoiceDetail> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
