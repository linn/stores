namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Resources.RequestResources;
    using System.Collections.Generic;

    public interface IConsignmentFacadeService : IFacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource>
    {
        IResult<IEnumerable<Consignment>> GetByRequestResource(ConsignmentsRequestResource resource);
    }
}
