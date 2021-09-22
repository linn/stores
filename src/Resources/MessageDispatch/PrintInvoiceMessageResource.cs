namespace Linn.Stores.Resources.MessageDispatch
{
    public class PrintInvoiceMessageResource : MessageBase
    {
        public string DocumentType { get; set; } = "I";

        public int DocumentNumber { get; set; }

        public string CopyType { get; set; } = "COPY DOCUMENT";

        public string ShowPrices { get; set; } = "Y";
    }
}
