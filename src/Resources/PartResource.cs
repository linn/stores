namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;

    public class PartResource : HypermediaResource
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string RootProduct { get; set; }

        public string RootProductDescription { get; set; }

        public bool? StockControlled { get; set; }

        public bool? SafetyCriticalPart { get; set; }

        public string ProductAnalysisCode { get; set; }

        public string ProductAnalysisCodeDescription { get; set; }

        public string ParetoCode { get; set; }

        public string ParetoDescription { get; set; }

        public string AccountingCompany { get; set; }

        public string AccountingCompanyDescription { get; set; }

        public bool? EmcCriticalPart { get; set; }

        public bool? SingleSourcePart { get; set; }

        public bool? PerformanceCriticalPart { get; set; }

        public string SafetyDataDirectory { get; set; }

        public bool? CccCriticalPart { get; set; }

        public bool? PsuPart { get; set; }

        public string SafetyCertificateExpirationDate { get; set; }

        public bool? LinnProduced { get; set; }

        public string DecrementRule { get; set; }

        public string BomType { get; set; }

        public string OptionSet { get; set; }

        public string DrawingReference { get; set; }

        public bool? PlannedSurplus { get; set; }

        public int? BomId { get; set; }

        public string UnitOfMeasure { get; set; }

        public int? PreferredSupplier { get; set; }

        public string PreferredSupplierName { get; set; }

        public string Currency { get; set; }

        public decimal? CurrencyUnitPrice { get; set; }

        public decimal? BaseUnitPrice { get; set; }

        public decimal? MaterialPrice { get; set; }

        public decimal? LabourPrice { get; set; }

        public decimal? CostingPrice { get; set; }

        public bool? OrderHold { get; set; }

        public string PartCategory { get; set; }

        public decimal? SparesRequirement { get; set; }

        public bool? IgnoreWorkstationStock { get; set; }

        public int? ImdsIdNumber { get; set; }

        public decimal? ImdsWeight { get; set; }

        public string MechanicalOrElectronic { get; set; }

        public bool? QcOnReceipt { get; set; }

        public string QcInformation { get; set; }

        public string RawOrFinished { get; set; }

        public int? OurInspectionWeeks { get; set; }

        public int? SafetyWeeks { get; set; }

        public string RailMethod { get; set; }

        public decimal? MinStockRail { get; set; }

        public decimal? MaxStockRail { get; set; }

        public bool? SecondStageBoard { get; set; }

        public string SecondStageDescription { get; set; }

        public string TqmsCategoryOverride { get; set; }

        public string StockNotes { get; set; }

        public string DateCreated { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public string DateLive { get; set; }

        public int? MadeLiveBy { get; set; }

        public string MadeLiveByName { get; set; }

        public string DatePhasedOut { get; set; }

        public int? PhasedOutBy { get; set; }

        public string PhasedOutByName { get; set; }

        public string ReasonPhasedOut { get; set; }

        public string ScrapOrConvert { get; set; }

        public string PurchasingPhaseOutType { get; set; }

        public string DateDesignObsolete { get; set; }
    }
}
