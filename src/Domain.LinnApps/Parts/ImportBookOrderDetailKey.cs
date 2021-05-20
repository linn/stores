namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class ImportBookOrderDetailKey
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public ImportBookOrderDetailKey(int importBookId, int lineNumber)
        {
            this.ImportBookId = importBookId;
            this.LineNumber = lineNumber;
        }
    }
}