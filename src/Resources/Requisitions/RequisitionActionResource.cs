namespace Linn.Stores.Resources.Requisitions
{
    using Linn.Common.Resources;

    public class RequisitionActionResource : HypermediaResource
    {
        public RequisitionResource Requisition { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
