namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBooksResourceBuilder : IResourceBuilder<IEnumerable<ImportBook>>
    {
        private readonly ImportBookResourceBuilder importBookResourceBuilder = new ImportBookResourceBuilder();

        public IEnumerable<ImportBookResource> Build(IEnumerable<ImportBook> importBooks)
        {
            return importBooks
                .OrderBy(b => b.Id)
                .Select(a => this.importBookResourceBuilder.Build(a));
        }

        public string GetLocation(IEnumerable<ImportBook> model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<ImportBook>>.Build(IEnumerable<ImportBook> importBooks) => this.Build(importBooks);
    }
}
