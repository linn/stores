namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    public class ValidateRsnResult 
    {
        public bool Success { get; set; }

        public string State { get; set; }

        public string ArticleNumber { get; set; }

        public string Description { get; set; }

        public int? Quantity { get; set; }

        public int? SerialNumber { get; set; }
    }
}
