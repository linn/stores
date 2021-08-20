namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookOrderDetailsResourceBuilder : IResourceBuilder<IEnumerable<ImportBookOrderDetail>>
    {
        private readonly ImportBookOrderDetailResourceBuilder importBookOrderDetailResourceBuilder =
            new ImportBookOrderDetailResourceBuilder();

        public IEnumerable<ImportBookOrderDetailResource> Build(IEnumerable<ImportBookOrderDetail> details)
        {
            return details.OrderBy(b => b.LineNumber).Select(a => this.importBookOrderDetailResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookOrderDetail>>.Build(IEnumerable<ImportBookOrderDetail> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookOrderDetail> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
