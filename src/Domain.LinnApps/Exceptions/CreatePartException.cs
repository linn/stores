namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class CreatePartException : DomainException
    {
        public CreatePartException(string message)
            : base(message)
        {
        }

        public CreatePartException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
