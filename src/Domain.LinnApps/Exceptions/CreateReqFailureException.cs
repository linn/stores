namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class CreateReqFailureException : DomainException
    {
        public CreateReqFailureException(string message)
            : base(message)
        {
        }

        public CreateReqFailureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
