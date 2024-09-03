namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class LibraryRefsResponseProcessor : JsonResponseProcessor<IEnumerable<LibraryRef>>
    {
        public LibraryRefsResponseProcessor(IResourceBuilder<IEnumerable<LibraryRef>> resourceBuilder)
            : base(resourceBuilder, "library-refs", 1)
        {
        }
    }
}
