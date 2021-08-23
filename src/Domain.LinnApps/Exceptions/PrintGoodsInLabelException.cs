namespace Linn.Stores.Domain.LinnApps.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class PrintGoodsInLabelException : DomainException
    {
        public PrintGoodsInLabelException(string message)
            : base(message)
        {
        }

        public PrintGoodsInLabelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
