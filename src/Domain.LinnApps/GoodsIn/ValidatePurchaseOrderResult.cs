namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    public class ValidatePurchaseOrderResult
    {
        public int? OrderNumber { get; set; }

        public int? OrderLine { get; set; }

        public int? QtyBookedIn { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string OrderUnitOfMeasure { get; set; }

        public int OrderQty { get; set; }

        public string QcPart { get; set; }

        public string ManufacturersPartNumber { get; set; }

        public string DocumentType { get; set; }

        public string BookInMessage { get; set; }

        public string PartQcWarning { get; set; }

        public string Storage { get; set; }

        public string TransactionType { get; set; }

        public string State { get; set; }
    }
}
