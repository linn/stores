namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;


    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class TqmsCategoriesResponseProcessor 
        : JsonResponseProcessor<IEnumerable<TqmsCategory>>
    {
        public TqmsCategoriesResponseProcessor(
            IResourceBuilder<IEnumerable<TqmsCategory>> resourceBuilder)
            : base(resourceBuilder, "tqms-categories", 1)
        {
        }
    }
}
