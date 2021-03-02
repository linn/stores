namespace Linn.Stores.Resources
{
    public class ExportReturnDetailResource
    {
        public int ReturnId { get; set; }

        public int RsnNumber { get; set; }

        public string ArticleNumber { get; set; }

        public int LineNo { get; set; }

        public int Qty { get; set; }

        public string Description { get; set; }

        public int? CustomsValue { get; set; }

        public int? BaseCustomsValue { get; set; }

        public int TariffId { get; set; }

        public string ExpinvDocumentType { get; set; }

        public int? ExpinvDocumentNumber { get; set; }

        public string ExpinvDate { get; set; }
    }
}