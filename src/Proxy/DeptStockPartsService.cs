namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class DeptStockPartsService : IDeptStockPartsService
    {
        private readonly IDatabaseService databaseService;

        public DeptStockPartsService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public IEnumerable<Part> GetDeptStockPalletParts()
        {
            var query =
                @"SELECT PART.PART_NUMBER, PART.DESCRIPTION, PART.BRIDGE_ID
                 FROM PARTS PART
                WHERE EXISTS (
                    SELECT 1
                    FROM STOCK_LOCATORS SL
                    WHERE SL.PART_NUMBER = PART.PART_NUMBER
                      AND SL.STOCK_POOL_CODE IN ('LINN DEPT', 'LS DEPT'))
                OR (PART.STOCK_CONTROLLED = 'N'
                    AND NVL(PART.BASE_UNIT_PRICE, 0) = 0
                    AND NVL(PART.LINN_PRODUCED, 'N') = 'N'
                    AND NOT EXISTS (
                        SELECT 1
                        FROM STOCK_LOCATORS SL
                        WHERE SL.PART_NUMBER = PART.PART_NUMBER))
                OR EXISTS (
                    SELECT 1
                    FROM STOCK_LOCATORS SL
                    WHERE SL.PART_NUMBER = PART.PART_NUMBER
                      AND SL.BUDGET_ID IS NULL)";

            var rows = this.databaseService.ExecuteQuery(query).Tables[0].Rows;
            var result = new List<Part>();

            foreach (DataRow row in rows)
            {
                result.Add(new Part
                               {
                                   PartNumber = row.ItemArray[0].ToString(),
                                   Description = row.ItemArray[1].ToString(),
                                   Id = int.Parse(row.ItemArray[2].ToString())
                               });
            }

            return result;
        }
    }
}
