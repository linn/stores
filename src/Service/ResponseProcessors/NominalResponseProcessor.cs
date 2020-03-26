namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class NominalResponseProcessor : JsonResponseProcessor<Nominal>
    {
        public NominalResponseProcessor(IResourceBuilder<Nominal> resourceBuilder)
            : base(resourceBuilder, "nominal", 1)
        {
        }
    }
}