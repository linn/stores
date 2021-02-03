namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public class InspectedStateResourceBuilder : IResourceBuilder<InspectedState>
    {
        public InspectedStateResource Build(InspectedState state)
        {
            return new InspectedStateResource
                       {
                           State = state.State,
                           Description = state.Description,
                       };
        }

        public string GetLocation(InspectedState state)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<InspectedState>.Build(InspectedState state) => this.Build(state);

        private IEnumerable<LinkResource> BuildLinks(InspectedState state)
        {
            throw new NotImplementedException();
        }
    }
}
