namespace Linn.Stores.Resources.Parts
{
    public class MechPartSourceResource
    {
        public int Id { get; set; }

        public int ProposedBy { get; set; }

        public string DateEntered { get; set; }

        public string PartNumber { get; set; }

        public string MechanicalOrElectrical { get; set; }

        public string PartType { get; set; }

        public int? EstimatedVolume { get; set; }

        public string SamplesRequired { get; set; }

        public int SampleQuantity { get; set; }

        public string DateSamplesRequired { get; set; }

        public string RohsReplace { get; set; }

        public string LinnPartNumber { get; set; }

        public string Notes { get; set; }

        public string AssemblyType { get; set; }
    }
}