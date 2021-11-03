namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using Linn.Stores.Domain.LinnApps.Models;

    public class ValidateRsnResult : ProcessResult
    {
        public string State { get; set; }

        public string ArticleNumber { get; set; }

        public string Description { get; set; }

        public int? Quantity { get; set; }

        public int? SerialNumber { get; set; }
    }
}
