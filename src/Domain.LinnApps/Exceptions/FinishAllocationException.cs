namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class FinishAllocationException : DomainException
    {
        public FinishAllocationException(string message)
            : base(message)
        {
        }

        public FinishAllocationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
