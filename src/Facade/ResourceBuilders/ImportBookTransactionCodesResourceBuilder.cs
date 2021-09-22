namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookTransactionCodesResourceBuilder : IResourceBuilder<IEnumerable<ImportBookTransactionCode>>
    {
        private readonly ImportBookTransactionCodeResourceBuilder importBookTransactionCodeResourceBuilder =
            new ImportBookTransactionCodeResourceBuilder();

        public IEnumerable<ImportBookTransactionCodeResource> Build(
            IEnumerable<ImportBookTransactionCode> transportCodes)
        {
            return transportCodes.OrderBy(b => b.TransactionId)
                .Select(a => this.importBookTransactionCodeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookTransactionCode>>.Build(
            IEnumerable<ImportBookTransactionCode> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookTransactionCode> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
