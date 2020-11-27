namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class ErrorResponseProcessor : JsonResponseProcessor<Error>
    {
        public ErrorResponseProcessor(IResourceBuilder<Error> resourceBuilder)
            : base(resourceBuilder, "error", 1)
        {
        }
    }
}
