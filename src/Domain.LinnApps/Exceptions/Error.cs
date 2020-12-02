namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    public class Error
    {
        public Error(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}
