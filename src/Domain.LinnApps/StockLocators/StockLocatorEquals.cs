namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System.Collections.Generic;

    public class StockLocatorEquals : IEqualityComparer<StockLocator>
    {
        public bool Equals(StockLocator a, StockLocator b)
        {
            return a.BatchRef == b?.BatchRef && a.PartNumber == b?.PartNumber
                                             && a.StockRotationDate == b?.StockRotationDate && a.State == b?.State
                                             && a.StorageLocation?.LocationCode == b?.StorageLocation?.LocationCode
                                             && a.PalletNumber == b?.PalletNumber && a.Category == b?.Category;
        }

        public int GetHashCode(StockLocator stockLocator)
        {
            return (stockLocator.BatchRef
                    + stockLocator.PartNumber 
                    + stockLocator.StockRotationDate 
                    + stockLocator.State 
                    + stockLocator.PalletNumber
                    + stockLocator.Category
                    + stockLocator.StorageLocation?.LocationCode).GetHashCode();
        }
    }
}
