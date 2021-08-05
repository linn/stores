namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookPostEntriesResourceBuilder : IResourceBuilder<IEnumerable<ImportBookPostEntry>>
    {
        private readonly ImportBookPostEntryResourceBuilder importBookPostEntryResourceBuilder =
            new ImportBookPostEntryResourceBuilder();

        public IEnumerable<ImportBookPostEntryResource> Build(IEnumerable<ImportBookPostEntry> entries)
        {
            return entries.OrderBy(b => b.LineNumber).Select(a => this.importBookPostEntryResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookPostEntry>>.Build(IEnumerable<ImportBookPostEntry> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookPostEntry> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
