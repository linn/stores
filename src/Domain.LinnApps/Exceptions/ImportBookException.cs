namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class ImportBookException : DomainException
    {
        public ImportBookException(string message)
            : base(message)
        {
        }

        public ImportBookException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
