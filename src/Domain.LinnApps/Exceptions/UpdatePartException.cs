namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class UpdatePartException : DomainException
    {
        public UpdatePartException(string message)
            : base(message)
        {
        }

        public UpdatePartException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}