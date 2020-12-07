namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class PickItemsException : DomainException
    {
        public PickItemsException(string message)
            : base(message)
        {
        }

        public PickItemsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
