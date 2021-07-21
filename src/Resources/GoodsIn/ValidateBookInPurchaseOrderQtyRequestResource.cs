namespace Linn.Stores.Resources.GoodsIn
{
    public class ValidateBookInPurchaseOrderQtyRequestResource
    {
        public int OrderNumber { get; set; }

        public int? OrderLine { get; set; }

        public int Qty { get; set; }
    }
}
