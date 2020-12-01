namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;

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
    }
}
