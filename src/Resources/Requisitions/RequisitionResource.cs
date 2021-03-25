namespace Linn.Stores.Resources.Requisitions
{
    using Linn.Common.Resources;

    public class RequisitionResource : HypermediaResource
    {
        public int ReqNumber { get; set; }

        public int? Document1 { get; set; }
    }
}
