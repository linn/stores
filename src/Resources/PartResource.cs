namespace Linn.Stores.Resources
{
    using System.Numerics;

    using Linn.Common.Resources;

    public class PartResource : HypermediaResource
    {
        public long Id { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string RootProduct { get; set; }

        public string StockControlled { get; set; }

        public string SafetyCriticalPart { get; set; }

        public string ProductAnalysisCode { get; set; }

        public string ParetoCode { get; set; }

        public string AccountingCompany { get; set; }

        public string EmcCriticalPart { get; set; }

        public string SingleSourcePart { get; set; }

        public string PerformanceCriticalPart { get; set; }

        public string SafetyDataDirectory { get; set; }

        public string CccCriticalPart { get; set; }

        public string PsuPart { get; set; }

        public string SafetyCertificateExpirationDate { get; set; }
    }
}
