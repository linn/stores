namespace Linn.Stores.Domain.LinnApps
{
    public class ConsignmentItem
    {
        public int ItemNumber { get; set; }

        public int ConsignmentId { get; set; }

        public int? OrderNumber { get; set; } 

        public SalesOrder SalesOrder { get; set; }
    }
}
