namespace Linn.Stores.Domain.LinnApps
{
    using Parts;

    public class Rsn
    {
        public int RsnNumber { get; set; }

        public string ArticleNumber { get; set; }

        public SalesArticle SalesArticle { get; set; }

        public int Quantity { get; set; }

        public string Ipr { get; set; }
    }
}
