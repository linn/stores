namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    public interface IConsignmentShipfileService
    {
        IEnumerable<ConsignmentShipfile> GetEmailDetails(int consignmentId);
    }
}
