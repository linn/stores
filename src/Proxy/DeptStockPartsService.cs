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
                @"SELECT PART.PART_NUMBER PART_NUMBER,
                    PART.DESCRIPTION DESCRIPTION,
                    PART.BRIDGE_ID BRIDGE_ID
                    FROM PARTS PART 
                    WHERE PART.PART_NUMBER IN
                        (SELECT P.PART_NUMBER 
                        FROM STOCK_LOCATORS SL,     
                        PARTS P WHERE SL.PART_NUMBER = P.PART_NUMBER 
                        and stock_pool_code in ('LINN DEPT','LS DEPT')) OR(PART.STOCK_CONTROLLED = 'N' 
                        AND NVL(PART.BASE_UNIT_PRICE, 0) = 0 AND NVL(PART.LINN_PRODUCED, 'N') = 'N' 
                        AND PART.PART_NUMBER NOT IN(SELECT P.PART_NUMBER FROM STOCK_LOCATORS SL, PARTS P WHERE SL.PART_NUMBER = P.PART_NUMBER))
                    OR
                        (exists(
                            select 1 from stock_locators sl 
                            where sl.part_number = part.part_number and sl.budget_id is null))";

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
