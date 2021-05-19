namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    public interface IConsignmentShipfileService
    {
        IEnumerable<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend, bool test = false);
    }
}
