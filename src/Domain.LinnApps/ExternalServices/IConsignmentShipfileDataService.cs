namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Models.Emails;

    public interface IConsignmentShipfileDataService
    {
        IEnumerable<PackingListItem> GetPackingList(int consignmentId);

        IEnumerable<DespatchNote> GetDespatchNotes(int consignmentId);
    }
}
