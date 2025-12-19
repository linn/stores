namespace Linn.Stores.Resources.RequestResources
{
    public class PartsSearchRequestResource : SearchRequestResource
    {
        public string DescriptionSearchTerm { get; set; }

        public string PartNumberSearchTerm { get; set; }

        public string ProductAnalysisCodeSearchTerm { get; set; }

        public string ManufacturersPartNumberSearchTerm { get; set; }
    }
}
