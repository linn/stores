namespace Linn.Stores.Proxy
{
    using System;
   
    public class PdfServiceException : Exception
    {
        public PdfServiceException(string message)
            : base(message)
        {
        }

        public PdfServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    } 
}
