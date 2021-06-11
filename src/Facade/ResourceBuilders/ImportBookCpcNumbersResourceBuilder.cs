namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookCpcNumbersResourceBuilder : IResourceBuilder<IEnumerable<ImportBookCpcNumber>>
    {
        private readonly ImportBookCpcNumberResourceBuilder importBookCpcNumberResourceBuilder =
            new ImportBookCpcNumberResourceBuilder();

        public IEnumerable<ImportBookCpcNumberResource> Build(IEnumerable<ImportBookCpcNumber> cpcNumbers)
        {
            return cpcNumbers.OrderBy(b => b.CpcNumber).Select(a => this.importBookCpcNumberResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookCpcNumber>>.Build(IEnumerable<ImportBookCpcNumber> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookCpcNumber> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
