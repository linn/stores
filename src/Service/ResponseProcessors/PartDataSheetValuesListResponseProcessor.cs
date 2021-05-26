namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartDataSheetValuesResponseProcessor : JsonResponseProcessor<IEnumerable<PartDataSheetValues>>
    {
        public PartDataSheetValuesResponseProcessor(IResourceBuilder<IEnumerable<PartDataSheetValues>> resourceBuilder)
            : base(resourceBuilder, "part-data-sheet-values-list", 1)
        {
        }
    }
}
