namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models.Emails;

    public class ConsignmentShipfilePackingListDataProxy : IConsignmentShipfilePackingListService
    {
        private readonly IDatabaseService databaseService;

        public ConsignmentShipfilePackingListDataProxy(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public IEnumerable<PackingListItem> GetPackingList(int consignmentId)
        {
            var sql = $@"
            (SELECT CI.CONSIGNMENT_ID,
            PALLET_NO,
            NVL(CI.CONTAINER_NO,999998) CONTAINER_NO,
            NVL(consignment.container_description(CI.CONSIGNMENT_ID,CI.CONTAINER_NO),
            TO_CHAR(QTY)||' '||ITEM_DESCRIPTION) ITEM_DESCRIPTION,
            QTY
            FROM
            CONSIGNMENT_ITEMS CI
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND PALLET_NO IS NOT NULL
            UNION ALL
            SELECT CI.CONSIGNMENT_ID,
            PALLET.PALLET_NO,
            CI.CONTAINER_NO,
            NVL(consignment.container_description(CI.CONSIGNMENT_ID,CI.CONTAINER_NO),
            TO_CHAR(QTY)||' '||ITEM_DESCRIPTION) ITEM_DESCRIPTION,
            QTY
            FROM
            CONSIGNMENT_ITEMS CI,
            (SELECT DISTINCT PALLET_NO,CONTAINER_NO
            FROM CONSIGNMENT_ITEMS
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND PALLET_NO IS NOT NULL) PALLET
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND CI.CONTAINER_NO = PALLET.CONTAINER_NO
            AND CI.PALLET_NO IS NULL
            UNION ALL
            SELECT CI.CONSIGNMENT_ID,
            PALLET_NO,
            CI.CONTAINER_NO,
            NVL(consignment.container_description(CI.CONSIGNMENT_ID,CI.CONTAINER_NO),
            TO_CHAR(QTY)||' '||ITEM_DESCRIPTION) ITEM_DESCRIPTION,
            QTY
            FROM
            CONSIGNMENT_ITEMS CI
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND PALLET_NO IS NULL
            AND NOT EXISTS(SELECT PALLET_NO FROM CONSIGNMENT_ITEMS
            WHERE PALLET_NO IS NOT NULL AND CONTAINER_NO = CI.CONTAINER_NO
            AND CONSIGNMENT_ID = CI.CONSIGNMENT_ID))
            ORDER BY CONTAINER_NO";

            var rows = this.databaseService.ExecuteQuery(sql).Tables[0].Rows;
            var result = new List<PackingListItem>();
            for (var i = 0; i < rows.Count; i++)
            {
                var data = rows[i].ItemArray;
                result.Add(new PackingListItem
                               {
                                   Pallet = data[1].ToString(),
                                   Box = data[2].ToString(),
                                   ContentsDescription = data[3].ToString(),
                                   // what is count? Seems to come from a Program Unit on the report Count = 
                               });
            }

            return result;
        }
    }
}
