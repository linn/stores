namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Resources.RequestResources;

    public interface IConsignmentFacadeService : IFacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource>
    {
        IResult<IEnumerable<Consignment>> GetByRequestResource(ConsignmentsRequestResource resource);

        IResult<IEnumerable<Consignment>> GetByInvoiceNumber(int invoiceNumber);
    }
}
