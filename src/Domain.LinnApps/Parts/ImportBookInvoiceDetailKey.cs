namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class ImportBookInvoiceDetailKey
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public ImportBookInvoiceDetailKey(int importBookId, int lineNumber)
        {
            this.ImportBookId = importBookId;
            this.LineNumber = lineNumber;
        }
    }
}