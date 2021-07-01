namespace Linn.Stores.Resources
{
    public class ValidatePurchaseOrderResultResource
    {
        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string OrderUnitOfMeasure { get; set; }

        public int OrderQty { get; set; }

        public string QcPart { get; set; }

        public string ManufacturersPartNumber { get; set; }

        public string DocumentType { get; set; }

        public string BookInMessage { get; set; }
    }
}
