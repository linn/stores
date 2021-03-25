namespace Linn.Stores.Domain.LinnApps.Models
{
    public class ProcessResult
    {
        public ProcessResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        public ProcessResult()
        {
        }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
