namespace Linn.Stores.Resources.RequestResources
{
    public class ExportRsnSearchRequestResource
    {
        public int AccountId { get; set; }

        public int? OutletNumber { get; set; }

        public string SearchTerm { get; set; }
    }
}