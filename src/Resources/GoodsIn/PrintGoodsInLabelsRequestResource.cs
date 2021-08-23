namespace Linn.Stores.Resources.GoodsIn
{
    using System.Collections.Generic;

    public class PrintGoodsInLabelsRequestResource
    {
        public string DocumentType { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string DeliveryRef { get; set; }


        public string UnitOfMeasure { get; set; }

        public string QcInformation { get; set; }

        public int Qty { get; set; }

        public int LineNumber { get; set; }

        public string QcDate { get; set; }

        public int ReqNumber { get; set; }

        public IEnumerable<GoodsInLabelLineResource> Lines { get; set; }
    }
}
