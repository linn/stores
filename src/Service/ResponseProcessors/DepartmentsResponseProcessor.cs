namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;


    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class DepartmentsResponseProcessor : JsonResponseProcessor<IEnumerable<Department>>
    {
        public DepartmentsResponseProcessor(IResourceBuilder<IEnumerable<Department>> resourceBuilder)
            : base(resourceBuilder, "departments", 1)
        {
        }
    }
}
