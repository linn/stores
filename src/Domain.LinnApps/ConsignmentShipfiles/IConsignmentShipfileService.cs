namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using System.Collections.Generic;

    public interface IConsignmentShipfileService
    {
        IEnumerable<ConsignmentShipfile> SendEmails(
            IEnumerable<ConsignmentShipfile> toSend,
            bool test = false,
            string testEmailAddress = null);
    }
}
