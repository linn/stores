﻿namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    public interface IConsignmentShipfileService
    {
        IEnumerable<ConsignmentShipfile> GetEmailDetails(IEnumerable<ConsignmentShipfile> shipfiles);

        IEnumerator<ConsignmentShipfile> SendEmails(IEnumerable<ConsignmentShipfile> toSend);
    }
}
