namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class TranslateLocationException : DomainException
    {
        public TranslateLocationException(string message)
            : base(message)
        {
        }

        public TranslateLocationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
