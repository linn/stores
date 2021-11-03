namespace Linn.Stores.Resources.GoodsIn
{
    public class ValidateRsnResultResource : ProcessResultResource
    {
        public string State { get; set; }

        public string ArticleNumber { get; set; }

        public string Description { get; set; }

        public int? Quantity { get; set; }

        public int? SerialNumber { get; set; }
    }
}
