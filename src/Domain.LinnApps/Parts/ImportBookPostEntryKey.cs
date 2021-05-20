namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class ImportBookPostEntryKey
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public ImportBookPostEntryKey(int importBookId, int lineNumber)
        {
            this.ImportBookId = importBookId;
            this.LineNumber = lineNumber;
        }
    }
}