namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookDeliveryTermsResourceBuilder : IResourceBuilder<IEnumerable<ImportBookDeliveryTerm>>
    {
        private readonly ImportBookDeliveryTermResourceBuilder importBookDeliveryTermResourceBuilder =
            new ImportBookDeliveryTermResourceBuilder();

        public IEnumerable<ImportBookDeliveryTermResource> Build(IEnumerable<ImportBookDeliveryTerm> deliveryTerms)
        {
            return deliveryTerms.OrderBy(b => b.DeliveryTermCode).Select(a => this.importBookDeliveryTermResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ImportBookDeliveryTerm>>.Build(IEnumerable<ImportBookDeliveryTerm> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<ImportBookDeliveryTerm> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
