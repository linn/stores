namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class ProcessException : DomainException
    {
        public ProcessException(string message)
            : base(message)
        {
        }

        public ProcessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
