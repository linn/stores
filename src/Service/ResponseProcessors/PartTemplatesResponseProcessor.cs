namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartTemplatesResponseProcessor : JsonResponseProcessor<IEnumerable<ResponseModel<PartTemplate>>>
    {
        public PartTemplatesResponseProcessor(IResourceBuilder<IEnumerable<ResponseModel<PartTemplate>>> resourceBuilder)
            : base(resourceBuilder, "linnapps-part-templates", 1)
        {
        }
    }
}
