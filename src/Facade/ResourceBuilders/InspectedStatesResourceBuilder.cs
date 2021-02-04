namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public class InspectedStatesResourceBuilder : IResourceBuilder<IEnumerable<InspectedState>>
    {
        private readonly InspectedStateResourceBuilder stateResourceBuilder = new InspectedStateResourceBuilder();

        public IEnumerable<InspectedStateResource> Build(IEnumerable<InspectedState> states)
        {
            return states
                .Select(a => this.stateResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<InspectedState>>.Build(IEnumerable<InspectedState> states) => this.Build(states);

        public string GetLocation(IEnumerable<InspectedState> states)
        {
            throw new NotImplementedException();
        }
    }
}
