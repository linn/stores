namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class PostDutyException : DomainException
    {
        public PostDutyException(string message)
            : base(message)
        {
        }

        public PostDutyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
