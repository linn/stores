namespace Linn.Stores.Domain.LinnApps.ExportBooks
{
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class ExportBook
    {
        public int ExportId { get; set; }

        public int ConsignmentId { get; set; }

        public Consignment Consignment { get; set; }
    }
}
