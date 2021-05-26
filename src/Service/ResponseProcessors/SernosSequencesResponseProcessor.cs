namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class SernosSequencesResponseProcessor : JsonResponseProcessor<IEnumerable<SernosSequence>>
    {
        public SernosSequencesResponseProcessor(IResourceBuilder<IEnumerable<SernosSequence>> resourceBuilder)
            : base(resourceBuilder, "sernos-sequences", 1)
        {
        }
    }
}
