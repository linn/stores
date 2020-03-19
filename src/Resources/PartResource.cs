namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;

    public class PartResource : HypermediaResource
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public bool RootProduct { get; set; }

        public bool StockControlled { get; set; }

        public bool SafetyCriticalPart { get; set; }

        public string ProductAnalysisCode { get; set; }

        public string ParetoCode { get; set; }

        public string AccountingCompany { get; set; }

        public bool EmcCriticalPart { get; set; }

        public bool SingleSourcePart { get; set; }

        public bool PerformanceCriticalPart { get; set; }

        public string SafetyDataDirectory { get; set; }

        public bool CccCriticalPart { get; set; }

        public bool PsuPart { get; set; }

        public string SafetyCertificateExpirationDate { get; set; }
    }
}
