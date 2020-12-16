namespace Linn.Stores.Resources.Parts
{
    public class MechPartUsageResource
    {
        public int SourceId { get; set; }

        public int QuantityUsed { get; set; }

        public string RootProductPartNumber { get; set; }

        public string RootProductDescription { get; set; }
    }
}
