namespace Linn.Stores.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorLocationsViewService : IStockLocatorLocationsViewService
    {
        private readonly IDatabaseService databaseService;

        public StockLocatorLocationsViewService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public IEnumerable<StockLocatorLocation> QueryView(
            string partNumber,
            int? locationId,
            int? palletNumber,
            string stockPool,
            string stockState,
            string category)
        {
            var whereClause = "WHERE ";

            var whereClauseParts = new List<string>();

            if (partNumber != null)
            {
                if (partNumber.Contains("*"))
                {
                    whereClauseParts.Add($"PART_NUMBER LIKE '{partNumber.Replace("*", "%").ToUpper()}'");
                }
                else
                {
                    whereClauseParts.Add($"PART_NUMBER = '{partNumber.ToUpper()}'");
                }
            }

            if (locationId != null)
            {
                whereClauseParts.Add($"LOCATION_ID = {locationId}");
            }

            if (palletNumber != null)
            {
                whereClauseParts.Add($"PALLET_NUMBER = {palletNumber}");
            }

            if (stockPool != null)
            {
                whereClauseParts.Add($"STOCK_POOL_CODE = '{stockPool}'");
            }

            if (stockState != null)
            {
                whereClauseParts.Add($"STATE = '{stockState}'");
            }

            if (category != null)
            {
                whereClauseParts.Add($"CATEGORY = '{category}'");
            }

            for (var i = 0; i < whereClauseParts.Count;)
            {
                whereClause += whereClauseParts[i];
                i++;
                if (i != whereClauseParts.Count)
                {
                    whereClause += " AND ";
                }
            }

            var sql = $@"select * from stock_locator_loc_view {whereClause}";

            var res = this.databaseService.ExecuteQuery(sql);

            return (from DataRow row in res.Tables[0].Rows
                    select row.ItemArray
                    into values
                    select new StockLocatorLocation
                               {
                                   Quantity = int.Parse(values[0].ToString()),
                                   StorageLocationId = int.Parse(values[1].ToString()),
                                   StorageLocation = new StorageLocation
                                                         {
                                                             LocationId = int.Parse(values[1].ToString()),
                                                             LocationCode = values[2].ToString(), 
                                                             Description = values[3].ToString()
                                                         },
                                   PartNumber = values[4].ToString(),
                                   PalletNumber = values[5] == DBNull.Value ? (int?)null : int.Parse(values[5].ToString()),
                                   LocationType = values[6].ToString(),
                                   State = values[7].ToString(),
                                   Category = values[8].ToString(),
                                   StockPoolCode = values[9].ToString(),
                                   OurUnitOfMeasure = values[10].ToString(),
                                   QuantityAllocated = values[11] == DBNull.Value ? (int?)null : int.Parse(values[11].ToString()),
                               }).ToList();
        }
    }
}
