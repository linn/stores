namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class CartonTypesResourceBuilder : IResourceBuilder<IEnumerable<CartonType>>
    {
        private readonly CartonTypeResourceBuilder cartonTypeResourceBuilder = new CartonTypeResourceBuilder();

        public IEnumerable<CartonTypeResource> Build(IEnumerable<CartonType> cartonTypes)
        {
            return cartonTypes
                .Select(a => this.cartonTypeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<CartonType>>.Build(IEnumerable<CartonType> cartonTypes) => this.Build(cartonTypes);

        public string GetLocation(IEnumerable<CartonType> cartonTypes)
        {
            throw new NotImplementedException();
        }
    }
}
