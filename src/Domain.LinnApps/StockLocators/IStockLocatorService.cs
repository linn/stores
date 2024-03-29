﻿namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;
    using System.Collections.Generic;

    public interface IStockLocatorService
    {
        void UpdateStockLocator(StockLocator from, StockLocator to, IEnumerable<string> privileges);

        StockLocator CreateStockLocator(
            StockLocator toCreate, 
            string auditDepartmentCode, 
            IEnumerable<string> privileges);

        void DeleteStockLocator(StockLocator toDelete, IEnumerable<string> privileges);

        IEnumerable<StockLocatorWithStoragePlaceInfo> 
            GetStockLocatorsWithStoragePlaceInfoForPart(int partId);

        IEnumerable<StockLocator> GetBatches(string batches);

        IEnumerable<StockLocator> SearchStockLocators(
            string partNumber,
            int? locationId,
            int? palletNumber,
            string stockPool,
            string stockState,
            string category,
            string locationName,
            string partDescription);

        IEnumerable<StockLocator> SearchStockLocatorBatchView(
            string partNumber,
            int? locationId,
            int? palletNumber,
            string stockPool,
            string stockState,
            string category);

        IEnumerable<StockLocatorPrices> GetPrices(
            int? palletNumber,
            string partNumber,
            string locationCode,
            string state,
            string category,
            string stockPool,
            string batchRef,
            DateTime? batchDate);

        IEnumerable<StockMove> GetMoves(string partNumber, int? palletNumber, int? locationId);

        IEnumerable<StockLocator> GetBatchesInRotationOrderByPart(string partSearch);
    }
}
