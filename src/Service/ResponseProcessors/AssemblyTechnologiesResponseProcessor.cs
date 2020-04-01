namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class AssemblyTechnologiesResponseProcessor : JsonResponseProcessor<IEnumerable<AssemblyTechnology>>
    {
        public AssemblyTechnologiesResponseProcessor(IResourceBuilder<IEnumerable<AssemblyTechnology>> resourceBuilder)
            : base(resourceBuilder, "assembly-technology", 1)
        {
        }
    }
}