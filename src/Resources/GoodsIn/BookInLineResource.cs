namespace Linn.Stores.Resources.GoodsIn
{
    public class BookInLineResource
    {
        public int Id { get; set; }

        public string TransactionType { get; set; }

        public string DateCreated { get; set; }

        public int CreatedBy { get; set; }

        public string WandString { get; set; }

        public string ArticleNumber { get; set; }

        public int LocationId { get; set; }

        public string Location { get; set; }

        public int? Quantity { get; set; }

        public string ManufacturersPartNumber { get; set; }

        public int? SerialNumber { get; set; }

        public int? OrderNumber { get; set; }

        public int? OrderLine { get; set; }

        public int? LoanNumber { get; set; }

        public int? LoanLine { get; set; }

        public int? RsnNumber { get; set; }

        public string StoragePlace { get; set; }

        public int BookInRef { get; set; }

        public string DemLocation { get; set; }

        public string LogCondition { get; set; }

        public string RsnAccessories { get; set; }

        public string Comments { get; set; }

        public string State { get; set; }

        public string Description { get; set; }

        public string StorageType { get; set; }
    }
}
