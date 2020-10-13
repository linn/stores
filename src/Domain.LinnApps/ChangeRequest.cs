namespace Linn.Stores.Domain.LinnApps
{
    public class ChangeRequest
    {
        public int DocumentNumber { get; set; }

        public string NewPartNumber { get; set; }

        public string OldPartNumber { get; set; }

        public string ChangeState { get; set; }
    }
}