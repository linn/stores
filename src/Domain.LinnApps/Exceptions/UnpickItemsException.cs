namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class UnpickItemsException : DomainException
    {
        public UnpickItemsException(string message)
            : base(message)
        {
        }

        public UnpickItemsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
