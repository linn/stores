namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;
    using System.Collections.Generic;

    public class Part
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string RootProduct { get; set; }

        public string StockControlled { get; set; }

        public string SafetyCriticalPart { get; set; }

        public NominalAccount NominalAccount { get; set; }

        public ProductAnalysisCode ProductAnalysisCode { get; set; }

        public ParetoClass ParetoClass { get; set; }

        public AccountingCompany AccountingCompany { get; set; }

        public string EmcCriticalPart { get; set; }

        public string SingleSourcePart { get; set; }

        public string PerformanceCriticalPart { get; set; }

        public string SafetyDataDirectory { get; set; }

        public string CccCriticalPart { get; set; }

        public string PsuPart { get; set; }

        public DateTime? SafetyCertificateExpirationDate { get; set; }

        public string LinnProduced { get; set; }

        public string BomType { get; set; }

        public string OptionSet { get; set; }

        public string DrawingReference { get; set; }

        public string PlannedSurplus { get; set; }

        public int? BomId { get; set; }

        public string OurUnitOfMeasure { get; set; }

        public Supplier PreferredSupplier { get; set; }

        public string Currency { get; set; }

        public decimal? CurrencyUnitPrice { get; set; }

        public decimal? BaseUnitPrice { get; set; }

        public decimal? MaterialPrice { get; set; }

        public decimal? LabourPrice { get; set; }

        public decimal? CostingPrice { get; set; }

        public string OrderHold { get; set; }

        public string PartCategory { get; set; }

        public decimal? NonForecastRequirement { get; set; }

        public decimal? OneOffRequirement { get; set; }

        public decimal? SparesRequirement { get; set; }

        public string IgnoreWorkstationStock { get; set; }

        public int? ImdsIdNumber { get; set; }

        public decimal? ImdsWeight { get; set; }

        public string MechanicalOrElectronic { get; set; }

        public string QcOnReceipt { get; set; }

        public string QcInformation { get; set; }

        public string RawOrFinished { get; set; }

        public int? OurInspectionWeeks { get; set; }

        public int? SafetyWeeks { get; set; }

        public string RailMethod { get; set; }

        public decimal? MinStockRail { get; set; }

        public decimal? MaxStockRail { get; set; }

        public string SecondStageBoard { get; set; }

        public string SecondStageDescription { get; set; }

        public string TqmsCategoryOverride { get; set; }

        public string StockNotes { get; set; }

        public DateTime? DateCreated { get; set; }

        public Employee CreatedBy { get; set; }

        public DateTime? DateLive { get; set; }

        public Employee MadeLiveBy { get; set; }

        public DateTime? DatePhasedOut { get; set; }

        public Employee PhasedOutBy { get; set; }

        public string ReasonPhasedOut { get; set; }

        public string ScrapOrConvert { get; set; }

        public string PurchasingPhaseOutType { get; set; }

        public DateTime? DateDesignObsolete { get; set; }

        public SernosSequence SernosSequence { get; set; }

        public AssemblyTechnology AssemblyTechnology { get; set; }

        public DecrementRule DecrementRule { get; set; }

        public MechPartSource MechPartSource { get; set; }
    }
}
