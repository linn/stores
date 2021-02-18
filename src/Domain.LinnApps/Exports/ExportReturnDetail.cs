namespace Linn.Stores.Domain.LinnApps.Exports
{
    public class ExportReturnDetail
    {
        public int ReturnId { get; set; }

        public int LineNo { get; set; }

        public int RSNNumber { get; set; }

        public int Quantity { get; set; }

        public string ArticleNumber { get; set; }

        public string Description { get; set; }

        public decimal CustomsValue { get; set; }

        public decimal BaseCustomsValue { get; set; }

        public TariffCode TariffCode { get; set; }

        public ExportReturn ExportReturn { get; set; }
    }
}

