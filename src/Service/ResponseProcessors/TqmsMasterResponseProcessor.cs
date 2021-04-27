namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Tqms;

    public class TqmsMasterResponseProcessor 
        : JsonResponseProcessor<TqmsMaster>
    {
        public TqmsMasterResponseProcessor(
            IResourceBuilder<TqmsMaster> resourceBuilder)
            : base(resourceBuilder, "tqms-master", 1)
        {
        }
    }
}
