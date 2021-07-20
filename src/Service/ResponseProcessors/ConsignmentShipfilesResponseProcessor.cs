namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    public class ConsignmentShipfilesResponseProcessor : JsonResponseProcessor<IEnumerable<ConsignmentShipfile>>
    {
        public ConsignmentShipfilesResponseProcessor(IResourceBuilder<IEnumerable<ConsignmentShipfile>> resourceBuilder)
            : base(resourceBuilder, "consignment-shipfiles", 1)
        {
        }
    }
}
