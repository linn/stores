namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class ConsignmentCloseException : DomainException
    {
        public ConsignmentCloseException(string message)
            : base(message)
        {
        }

        public ConsignmentCloseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
