namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public class ImportBookOrderDetailKey
    {
        public ImportBookOrderDetailKey(int importBookId, int lineNumber)
        {
            this.ImportBookId = importBookId;
            this.LineNumber = lineNumber;
        }

        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }
    }
}
