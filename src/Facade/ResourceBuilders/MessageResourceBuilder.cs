namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;

    public class MessageResourceBuilder : IResourceBuilder<MessageResult>
    {
        public MessageResource Build(MessageResult model)
        {
            return new MessageResource { Message = model.Message };
        }

        public string GetLocation(MessageResult model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<MessageResult>.Build(MessageResult message) => this.Build(message);
    }
}
