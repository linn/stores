namespace Linn.Stores.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
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
            string category,
            string locationName)
        {
            var whereClause = "WHERE ";

            var whereClauseParts = new List<string>();

            if (partNumber != null)
            {
                whereClauseParts.Add(
                    partNumber.Contains("*")
                        ? $"PART_NUMBER LIKE '{partNumber.Replace("*", "%").ToUpper()}'"
                        : $"PART_NUMBER = '{partNumber.ToUpper()}'");
            }

            if (!string.IsNullOrEmpty(locationName))
            {
                whereClauseParts.Add(
                    locationName.Contains("*")
                        ? $"LOCATION_CODE LIKE '{locationName.Replace("*", "%").ToUpper()}'"
                        : $"LOCATION_CODE = '{locationName.ToUpper()}'");
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

            var sql = $@"SELECT * FROM STOCK_LOCATOR_PARTS_VIEW {whereClause}";

            var res = this.databaseService.ExecuteQuery(sql);

            return (from DataRow row in res.Tables[0].Rows
                    select row.ItemArray
                    into values
                    select new StockLocatorLocation
                               {
                                   Quantity = Convert.ToDecimal(values[0]),
                                   StorageLocationId = Convert.ToInt32(values[1]),
                                   StorageLocation = new StorageLocation
                                                         {
                                                             LocationId = Convert.ToInt32(values[1]),
                                                             LocationCode = values[2].ToString(), 
                                                             Description = values[3].ToString()
                                                         },
                                   PartNumber = values[4].ToString(),
                                   PartDescription = values[5].ToString(),
                                   Part = new Part { Id = Convert.ToInt32(values[13]) },
                                   PalletNumber = values[6] == DBNull.Value ? (int?)null : Convert.ToInt32(values[6]),
                                   LocationType = values[7].ToString(),
                                   State = values[8].ToString(),
                                   Category = values[9].ToString(),
                                   StockPoolCode = values[10].ToString(),
                                   OurUnitOfMeasure = values[11].ToString(),
                                   QuantityAllocated = values[12] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(values[12]),
                               }).ToList();
        }
    }
}
