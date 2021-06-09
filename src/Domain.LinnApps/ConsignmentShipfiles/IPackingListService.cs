namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using System.Collections.Generic;

    public interface IPackingListService
    {
        IEnumerable<PackingListItem> BuildPackingList(IEnumerable<PackingListItem> dataResult);
    }
}
