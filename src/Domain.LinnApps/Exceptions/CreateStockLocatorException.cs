namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class CreateStockLocatorException : DomainException
    {
        public CreateStockLocatorException(string message)
            : base(message)
        {
        }

        public CreateStockLocatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
