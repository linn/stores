namespace Linn.Stores.Resources.Parts
{
    public class MechPartSourceResource
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
    }
}
