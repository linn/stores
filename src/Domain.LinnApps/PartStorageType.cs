namespace Linn.Stores.Domain.LinnApps
{
    public class PartStorageType
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public string StorageType { get; set; }

        public string Remarks { get; set; }

        public int Maximum { get; set; }

        public int Increment { get; set; }

        public string Preference { get; set; }
    }
}
