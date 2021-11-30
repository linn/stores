namespace Linn.Stores.Resources.RequestResources
{
    public class PartTemplateSearchRequestResource : SearchRequestResource
    {
        public string DescriptionSearchTerm { get; set; }

        public string PartRootSearchTerm { get; set; }

        public string AccountingCompanySearchTerm { get; set; }

        public string ProductCodeSearchTerm { get; set; }

        public string AssemblyTechnologySearchTerm { get; set; }

        public string ParetoCodeSearchTerm { get; set; }
    }
}
