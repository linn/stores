namespace Linn.Stores.Resources.GoodsIn
{
    using System.Collections.Generic;

    public class PrintGoodsInLabelsRequestResource
    {
        public string DocumentType { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string DeliveryRef { get; set; }

        public string QcInformation { get; set; }

        public int Qty { get; set; }

        public int UserNumber { get; set; }
        
        public int OrderNumber { get; set; }
        
        public int NumberOfLines { get; set; }
                    
        public string QcState { get; set; }

        public int ReqNumber { get; set; }

        public string KardexLocation { get; set; }

        public IEnumerable<GoodsInLabelLineResource> Lines { get; set; }
    }
}
