namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using System.Collections.Generic;

    public interface IConsignmentShipfileService
    {
        ConsignmentShipfile SendEmails(ConsignmentShipfile toSend, bool test = false, string testEmailAddress = null);
    }
}
