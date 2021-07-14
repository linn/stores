namespace Linn.Stores.Resources.RequestResources
{
    public class BookInRequestResource
    {
        public string TransactionType { get; set; }

        public int CreatedBy { get; set; }

        public string PartNumber { get; set; }

        public int Qty { get; set; }

        public int? OrderNumber { get; set; }

        public int? OrderLine { get; set; }

        public int? RsnNumber { get; set; }

        public string StoragePlace { get; set; }

        public string StorageType { get; set; }

        public string DemLocation { get; set; }

        public string State { get; set; }

        public string Comments { get; set; }

        public string Condition { get; set; }

        public string RsnAccessories { get; set; }

        public int? ReqNumber { get; set; }

        public int? LoanNumber { get; set; }

        public int? LoanLine { get; set; }
    }
}
