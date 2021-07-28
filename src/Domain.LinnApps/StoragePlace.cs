namespace Linn.Stores.Domain.LinnApps
{
    public class StoragePlace
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? LocationId { get; set; }

        public int? PalletNumber { get; set; }

        public string SiteCode { get; set; }
    }
}
