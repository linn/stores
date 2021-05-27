namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using System.Collections.Generic;

    public class PackingListService : IPackingListService
    {
        public IEnumerable<PackingListItem> BuildPackingList(IEnumerable<PackingListItem> dataResult)
        {
            return dataResult;
        }
    }
}
