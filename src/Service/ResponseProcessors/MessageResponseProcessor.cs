namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Models;

    public class MessageResponseProcessor : JsonResponseProcessor<MessageResult>
    {
        public MessageResponseProcessor(IResourceBuilder<MessageResult> resourceBuilder)
            : base(resourceBuilder, "message", 1)
        {
        }
    }
}
