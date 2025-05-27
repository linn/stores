namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public class ImportBookDeliveryTerm
    {
        public string DeliveryTermCode { get; set; }
        
        public string Description { get; set; }
        
        public string Comments { get; set; }

        public int? SortOrder { get; set; }
    }
}
