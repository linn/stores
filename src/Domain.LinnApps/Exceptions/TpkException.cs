namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class TpkException : DomainException
    {
        public TpkException(string message)
            : base(message)
        {
        }

        public TpkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
