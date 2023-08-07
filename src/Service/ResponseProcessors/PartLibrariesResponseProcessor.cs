namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartLibrariesResponseProcessor : JsonResponseProcessor<IEnumerable<PartLibrary>>
    {
        public PartLibrariesResponseProcessor(IResourceBuilder<IEnumerable<PartLibrary>> resourceBuilder)
            : base(resourceBuilder, "partLibraries", 1)
        {
        }
    }
}
