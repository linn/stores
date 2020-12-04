namespace Linn.Stores.Resources.Parts
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class MechPartSourceResource : HypermediaResource
    {
        public int Id { get; set; }

        public int? ProposedBy { get; set; }

        public string ProposedByName { get; set; }

        public string DateEntered { get; set; }

        public string PartNumber { get; set; }

        public string MechanicalOrElectrical { get; set; }

        public string PartType { get; set; }

        public int? EstimatedVolume { get; set; }

        public string SamplesRequired { get; set; }

        public int? SampleQuantity { get; set; }

        public string DateSamplesRequired { get; set; }

        public string RohsReplace { get; set; }

        public string LinnPartNumber { get; set; }

        public string LinnPartDescription { get; set; }

        public string Notes { get; set; }

        public string AssemblyType { get; set; }

        public PartResource Part { get; set; }

        public string EmcCritical { get; set; }

        public string PerformanceCritical { get; set; }

        public string SafetyCritical { get; set; }

        public string SingleSource { get; set; }

        public string SafetyDataDirectory { get; set; }

        public string ProductionDate { get; set; }

        public string DrawingsPackage { get; set; }

        public string DrawingsPackageAvailable { get; set; }

        public string DrawingsPackageDate { get; set; }

        public string DrawingFile { get; set; }

        public string ChecklistCreated { get; set; }

        public string ChecklistAvailable { get; set; }

        public string ChecklistDate { get; set; }

        public string PackingAvailable { get; set; }

        public string PackingRequired { get; set; }

        public string PackingDate { get; set; }

        public string ProductKnowledge { get; set; }

        public string ProductKnowledgeAvailable { get; set; }

        public string ProductKnowledgeDate { get; set; }

        public string TestEquipment { get; set; }

        public string TestEquipmentAvailable { get; set; }

        public string TestEquipmentDate { get; set; }

        public string ApprovedReferenceStandards { get; set; }

        public string ApprovedReferencesAvailable { get; set; }

        public string ApprovedReferencesDate { get; set; }

        public string ProcessEvaluation { get; set; }

        public string ProcessEvaluationAvailable { get; set; }

        public string ProcessEvaluationDate { get; set; }

        public IEnumerable<MechPartAltResource> MechPartAlts { get; set; }

        public IEnumerable<MechPartManufacturerAltResource> MechPartManufacturerAlts { get; set; }

        public int? CapacitorRippleCurrent { get; set; }

        public decimal? Capacitance { get; set; }

        public decimal? CapacitorVoltageRating { get; set; }

        public int? CapacitorPositiveTolerance { get; set; }

        public int? CapacitorNegativeTolerance { get; set; }

        public string CapacitorDielectric { get; set; }

        public string PackageName { get; set; }

        public decimal? CapacitorPitch { get; set; }

        public decimal? CapacitorLength { get; set; }

        public decimal? CapacitorWidth { get; set; }

        public decimal? CapacitorHeight { get; set; }

        public decimal? CapacitorDiameter { get; set; }

        public string CapacitanceUnit { get; set; }

        public decimal? Resistance { get; set; }

        public string ResistanceUnit { get; set; }

        public decimal? ResistorTolerance { get; set; }

        public string Construction { get; set; }

        public decimal? ResistorLength { get; set; }

        public decimal? ResistorWidth { get; set; }

        public decimal? ResistorHeight { get; set; }

        public int? ResistorPowerRating { get; set; }

        public int? ResistorVoltageRating { get; set; }

        public int? TemperatureCoefficient { get; set; }

        public string TransistorType { get; set; }

        public string TransistorDeviceName { get; set; }


        public string TransistorPolarity { get; set; }

        public int? TransistorVoltage { get; set; }

        public decimal? TransistorCurrent { get; set; }

        public string IcType { get; set; }

        public string IcFunction { get; set; }

        public string LibraryRef { get; set; }

        public string FootprintRef { get; set; }
    }
}
