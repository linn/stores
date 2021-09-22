namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class ConsignmentEditException : DomainException
    {
        public ConsignmentEditException(string message)
            : base(message)
        {
        }

        public ConsignmentEditException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
