namespace Linn.Stores.Resources.Requisitions
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class RequisitionResource : HypermediaResource
    {
        public int ReqNumber { get; set; }

        public int? Document1 { get; set; }

        public IEnumerable<RequisitionLineResource> Lines { get; set; }
    }
}
