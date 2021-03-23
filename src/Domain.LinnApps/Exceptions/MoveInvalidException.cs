namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class MoveInvalidException : DomainException
    {
        public MoveInvalidException(string message)
            : base(message)
        {
        }

        public MoveInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
