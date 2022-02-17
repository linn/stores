namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;

    public class StockLocatorBatch
    {
        public decimal? Quantity { get; set; }
        
        public int LocationId { get; set; }
        
        public string LocationCode { get; set; }
        
        public int? PalletNumber { get; set; }
        
        public string LocationType { get; set; }
        
        public string PartNumber { get; set; }
        
        public string Description { get; set; }
        
        public string BatchRef { get; set; }
        
        public string State { get; set; }
        
        public string Category { get; set; }
        
        public DateTime? StockRotationDate { get; set; }
        
        public string StockPoolCode { get; set; }
        
        public decimal? QuantityAllocated { get; set; }
    }
}
