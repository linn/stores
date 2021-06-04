namespace Linn.Stores.Resources
{
    using System.Collections.Generic;

    public class ConsignmentShipfileSendEmailsRequestResource
    {
        public ConsignmentShipfileResource Shipfile { get; set; }

        public bool Test { get; set; }

        public string TestEmailAddress { get; set; }
    }
}
