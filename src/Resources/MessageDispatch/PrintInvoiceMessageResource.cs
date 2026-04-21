namespace Linn.Stores.Resources.MessageDispatch
{
    public class PrintInvoiceMessageResource : MessageBase
    {
        public string DocumentType { get; set; } = "I";

        public int DocumentNumber { get; set; }

        public string CopyType { get; set; } = "COPY DOCUMENT";

        public bool ShowPrices { get; set; }

        public bool ShowTermsAndConditions { get; set; }
    }
}
