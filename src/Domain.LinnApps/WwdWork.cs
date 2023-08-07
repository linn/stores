namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.Parts;

    public class WwdWork
    {
        public int JobId { get; set; }

        public string PartNumber { get; set; }

        public decimal? QuantityKitted { get; set; }

        public decimal? QuantityAtLocation { get; set; }

        public string StoragePlace { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }

        public string Remarks { get; set; }

        public Part Part { get; set; }
    }
}
