namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    public class ConsignmentShipfilesResourceBuilder : IResourceBuilder<IEnumerable<ConsignmentShipfile>>
    {
        private readonly ConsignmentShipfileResourceBuilder shipfileResourceBuilder = new ConsignmentShipfileResourceBuilder();

        public IEnumerable<ConsignmentShipfileResource> Build(IEnumerable<ConsignmentShipfile> shipfiles)
        {
            return shipfiles
               .Select(a => this.shipfileResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ConsignmentShipfile>>.Build(IEnumerable<ConsignmentShipfile> shipfiles) => this.Build(shipfiles);

        public string GetLocation(IEnumerable<ConsignmentShipfile> shipfiles)
        {
            throw new NotImplementedException();
        }
    }
}
