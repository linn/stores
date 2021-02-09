namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class StockLocatorException : DomainException
    {
        public StockLocatorException(string message)
            : base(message)
        {
        }

        public StockLocatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
