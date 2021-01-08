namespace Linn.Stores.Domain.LinnApps
{
    public class StoragePlace
    {
        public string Name { get; set; }

        public string StoragePlaceDescription { get; set; }

        public int? LocationId { get; set; }

        public int? PalletNumber { get; set; }
    }
}