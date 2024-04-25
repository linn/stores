namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;

    public class RequisitionProcessResultResource : HypermediaResource
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
