namespace Linn.Stores.Resources.Workstation
{
    using Linn.Common.Resources;

    public class WorkstationTopUpStatusResource : HypermediaResource
    {
        public string WorkstationTopUpJobRef { get; set; }

        public string ProductionTriggerRunJobRef { get; set; }
    }
}
