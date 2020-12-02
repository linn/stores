namespace Linn.Stores.Resources.RequestResources
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class CreateAuditReqsResource : HypermediaResource
    {
        public IEnumerable<string> LocationList { get; set; }

        public string LocationRange { get; set; }

        public string Department { get; set; }
    }
}