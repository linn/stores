namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;

    using System.Collections.Generic;

    public class MechPartSource
    {
        public int Id { get; set; }

        public Employee ProposedBy { get; set; }

        public DateTime DateEntered { get; set; }

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

        public IEnumerable<MechPartAlt> MechPartAlts { get; set; }

        public ICollection<MechPartManufacturerAlt> MechPartManufacturerAlts { get; set; }
    }
}
