namespace Linn.Stores.Domain.LinnApps
{
    public class PrintResult
    {
        public PrintResult(bool success, string responsePreview, string state = null)
        {
            this.Success = success;
            this.ResponsePreview = responsePreview;
            this.State = state;
        }

        public bool Success { get; set; }

        public string ResponsePreview { get; set; }

        public string State { get; set; }
    }
}
