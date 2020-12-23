namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;

    using System.Collections.Generic;

    public class MechPartSource
    {
        public int Id { get; set; }

        public Employee ProposedBy { get; set; }

        public DateTime? DateEntered { get; set; }

        public string PartNumber { get; set; }

        public string MechanicalOrElectrical { get; set; }

        public string PartType { get; set; }

        public int? EstimatedVolume { get; set; }

        public string SamplesRequired { get; set; }

        public int? SampleQuantity { get; set; }

        public DateTime? DateSamplesRequired { get; set; }

        public string RohsReplace { get; set; }

        public string Notes { get; set; }

        public string AssemblyType { get; set; }

        public string SafetyCritical { get; set; }

        public string EmcCritical { get; set; }

        public string PerformanceCritical { get; set; }

        public string SingleSource { get; set; }

        public Part Part { get; set; }

        public Part PartToBeReplaced { get; set; }

        public string LinnPartNumber { get; set; }

        public string SafetyDataDirectory { get; set; }

        public DateTime? ProductionDate { get; set; }

        public string DrawingsPackage { get; set; }

        public string DrawingsPackageAvailable { get; set; }

        public DateTime? DrawingsPackageDate { get; set; }

        public string DrawingFile { get; set; }

        public string ChecklistCreated { get; set; }

        public string ChecklistAvailable { get; set; }

        public DateTime? ChecklistDate { get; set; }

        public string PackingAvailable { get; set; }

        public string PackingRequired { get; set; }

        public DateTime? PackingDate { get; set; }

        public string ProductKnowledge { get; set; }

        public string ProductKnowledgeAvailable { get; set; }

        public DateTime? ProductKnowledgeDate { get; set; }

        public string TestEquipment { get; set; }

        public string TestEquipmentAvailable { get; set; }

        public DateTime? TestEquipmentDate { get; set; }

        public string ApprovedReferenceStandards { get; set; }

        public string ApprovedReferencesAvailable { get; set; }

        public DateTime? ApprovedReferencesDate { get; set; }

        public string ProcessEvaluation { get; set; }

        public string ProcessEvaluationAvailable { get; set; }

        public DateTime? ProcessEvaluationDate { get; set; }

        public ICollection<MechPartAlt> MechPartAlts { get; set; }

        public ICollection<MechPartManufacturerAlt> MechPartManufacturerAlts { get; set; }

        public int? CapacitorRippleCurrent { get; set; } 

        public decimal? Capacitance { get; set; }

        public decimal? CapacitorVoltageRating { get; set; }

        public int? CapacitorPositiveTolerance { get; set; }

        public int? CapacitorNegativeTolerance { get; set; }

        public string CapacitorDielectric { get; set; }

        public string Package { get; set; }

        public decimal? CapacitorPitch { get; set; }

        public decimal? CapacitorLength { get; set; }

        public decimal? CapacitorWidth { get; set; }

        public decimal? CapacitorHeight { get; set; }

        public decimal? CapacitorDiameter { get; set; }

        public string CapacitanceLetterAndNumeralCode { get; set; }

        public decimal? Resistance { get; set; }

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

        public string RkmCode { get; set; }
    }
}
