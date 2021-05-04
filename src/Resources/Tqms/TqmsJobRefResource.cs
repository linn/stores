namespace Linn.Stores.Resources.Tqms
{
    using Linn.Common.Resources;

    public class TqmsJobRefResource : HypermediaResource
    {
        public string JobRef { get; set; }

        public string DateOfRun { get; set; }
    }
}
