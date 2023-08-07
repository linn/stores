namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class EmptyException : DomainException
    {
        public EmptyException(string message)
            : base(message)
        {
        }

        public EmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
