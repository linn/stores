﻿namespace Linn.Stores.Resources.Parts
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class PartResource : HypermediaResource
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string RootProduct { get; set; }

        public string RootProductDescription { get; set; }

        public string StockControlled { get; set; }

        public string SafetyCriticalPart { get; set; }

        public string ProductAnalysisCode { get; set; }

        public string ProductAnalysisCodeDescription { get; set; }

        public string ParetoCode { get; set; }

        public string ParetoDescription { get; set; }

        public string AccountingCompany { get; set; }

        public string AccountingCompanyDescription { get; set; }

        public string EmcCriticalPart { get; set; }

        public string SingleSourcePart { get; set; }

        public string PerformanceCriticalPart { get; set; }

        public string SafetyDataDirectory { get; set; }

        public string CccCriticalPart { get; set; }

        public string PsuPart { get; set; }

        public string SafetyCertificateExpirationDate { get; set; }

        public string LinnProduced { get; set; }

        public string DecrementRuleName { get; set; }

        public string DecrementRuleDescription { get; set; }

        public string BomType { get; set; }

        public int? BomVerifyFreqWeeks { get; set; }

        public string OptionSet { get; set; }

        public string DrawingReference { get; set; }

        public string PlannedSurplus { get; set; }

        public int? BomId { get; set; }

        public string OurUnitOfMeasure { get; set; }

        public int? PreferredSupplier { get; set; }

        public string PreferredSupplierName { get; set; }

        public string Currency { get; set; }

        public decimal? CurrencyUnitPrice { get; set; }

        public decimal? BaseUnitPrice { get; set; }

        public decimal? MaterialPrice { get; set; }

        public decimal? LabourPrice { get; set; }

        public decimal? CostingPrice { get; set; }

        public string OrderHold { get; set; }

        public decimal? SparesRequirement { get; set; }

        public decimal? OneOffRequirement { get; set; }

        public decimal? NonForecastRequirement { get; set; }

        public string IgnoreWorkstationStock { get; set; }

        public int? ImdsIdNumber { get; set; }

        public decimal? ImdsWeight { get; set; }

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

        public string PlannerStory { get; set; }

        public int? NominalAccount { get; set; }

        public string Nominal { get; set; }

        public string NominalDescription { get; set; }

        public string Department { get; set; }

        public string DepartmentDescription { get; set; }

        public string SernosSequenceName { get; set; }

        public string SernosSequenceDescription { get; set; }

        public string AssemblyTechnologyName { get; set; }

        public string AssemblyTechnologyDescription { get; set; }

        public IEnumerable<string> UserPrivileges { get; set; }

        public IEnumerable<PartDataSheetResource> DataSheets { get; set; }

        public PartParamDataResource ParamData { get; set; }

        public int? SourceId { get; set; }

        public IEnumerable<MechPartManufacturerAltResource> Manufacturers { get; set; }

        public string SalesArticleNumber { get; set; }

        public bool FromTemplate { get; set; }

        public int? UpdatedBy { get; set; }

        public string LibraryName { get; set; }

        public string LibraryRef { get; set; }

        public string FootprintRef1 { get; set; }

        public string FootprintRef2 { get; set; }

        public string FootprintRef3 { get; set; }

        public string TheirPartNumber { get; set; }

        public string DatasheetPath { get; set; }

        public string AltiumType { get; set; }

        public int? TemperatureCoefficient { get; set; }

        public string Device { get; set; }

        public string AltiumValueRkm { get; set; }

        public string Dielectric { get; set; }

        public string Construction { get; set; }

        public int? CapNegativeTolerance { get; set; }

        public int? CapPositiveTolerance { get; set; }

        public decimal? CapVoltageRating { get; set; }

        public string Frequency { get; set; }

        public string FrequencyLabel { get; set; }

        public string SimKind { get; set; }

        public string SimSubKind { get; set; }

        public string SimModelName { get; set; }

        public string AltiumValue { get; set; }

        public decimal? ResistorTolerance { get; set; }

        public string WhoLastChangedQcFlag { get; set; }

        public string DateQcFlagLastChanged { get; set; }
    }
}
