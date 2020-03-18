﻿namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;
    using System.Numerics;

    public class Part
    {
        public long Id { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string RootProduct { get; set; }

        public string StockControlled { get; set; }

        public string SafetyCriticalPart { get; set; }

        public ProductAnalysisCode ProductAnalysisCode { get; set; }

        public ParetoClass ParetoClass { get; set; }

        public string AccountingCompany { get; set; }

        public string EmcCriticalPart { get; set; }

        public string SingleSourcePart { get; set; }

        public string PerformanceCriticalPart { get; set; }

        public string SafetyDataDirectory { get; set; }

        public string CccCriticalPart { get; set; }

        public string PsuPart { get; set; }

        public DateTime? SafetyCertificateExpirationDate { get; set; }
    }
}