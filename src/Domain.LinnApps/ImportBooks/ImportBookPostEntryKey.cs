namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public class ImportBookPostEntryKey
    {
        public ImportBookPostEntryKey(int importBookId, int lineNumber)
        {
            this.ImportBookId = importBookId;
            this.LineNumber = lineNumber;
        }

        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }
    }
}