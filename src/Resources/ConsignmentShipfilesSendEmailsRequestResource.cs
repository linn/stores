﻿namespace Linn.Stores.Resources
{
    using System.Collections.Generic;

    public class ConsignmentShipfilesSendEmailsRequestResource
    {
        public IEnumerable<ConsignmentShipfileResource> Shipfiles { get; set; }

        public bool Test { get; set; }

        public string TestEmailAddress { get; set; }
    }
}
