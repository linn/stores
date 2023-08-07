namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartLibraryResponseProcessor : JsonResponseProcessor<PartLibrary>
    {
        public PartLibraryResponseProcessor(IResourceBuilder<PartLibrary> resourceBuilder)
            : base(resourceBuilder, "partLibrary", 1)
        {
        }
    }
}
