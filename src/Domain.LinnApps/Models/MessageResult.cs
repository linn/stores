namespace Linn.Stores.Domain.LinnApps.Models
{
    public class MessageResult
    {
        public MessageResult(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
