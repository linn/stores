namespace Linn.Stores.Resources.MessageDispatch
{
    public class PrintInvoiceDocumentMessageBody
    {
        public int DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public bool ShowTermsAndConditions { get; set; }

        public bool ShowPrices { get; set; }

        public string PrinterGroup { get; set; }

        public string JobName { get; set; }
    }
}
