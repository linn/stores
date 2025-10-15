namespace Linn.Stores.Proxy
{
    using System;
   
    public class PrintServiceException : Exception
    {
        public PrintServiceException(string message)
            : base(message)
        {
        }

        public PrintServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    } 
}
