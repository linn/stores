namespace Linn.Stores.Resources.Requisitions
{
    using Linn.Common.Resources;

    public class RequisitionActionResource : HypermediaResource
    {
        public RequisitionHeaderResource RequisitionHeader { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
