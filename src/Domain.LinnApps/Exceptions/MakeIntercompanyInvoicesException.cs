namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class MakeIntercompanyInvoicesException : DomainException
    {
        public MakeIntercompanyInvoicesException(string message)
            : base(message)
        {
        }

        public MakeIntercompanyInvoicesException(string message, Exception innerException)
            : base(message)
        {
        }
    }
}