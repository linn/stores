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
    }
}
