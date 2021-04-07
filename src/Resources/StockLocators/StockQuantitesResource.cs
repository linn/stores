namespace Linn.Stores.Resources.StockLocators
{
    public class StockQuantitiesResource
    {
        public string PartNumber { get; set; }

        public int GoodStock { get; set; }

        public int GoodStockAllocated { get; set; }

        public int UninspectedStock { get; set; }

        public int UninspectedStockAllocated { get; set; }

        public int FaultyStock { get; set; }

        public int FaultyStockAllocated { get; set; }

        public int DistributorStock { get; set; }

        public int DistributorStockAllocated { get; set; }

        public int SupplierStock { get; set; }

        public int SupplierStockAllocated { get; set; }

        public int OtherStock { get; set; }

        public int OtherStockAllocated { get; set; }
    }
}
