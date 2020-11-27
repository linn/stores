namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class UnitOfMeasureResourceBuilder : IResourceBuilder<UnitOfMeasure>
    {
        public UnitOfMeasureResource Build(UnitOfMeasure unitOfMeasure)
        {
            return new UnitOfMeasureResource
                       {
                           Unit = unitOfMeasure.Unit,
                       };
        }

        public string GetLocation(UnitOfMeasure unitOfMeasure)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<UnitOfMeasure>.Build(UnitOfMeasure unitOfMeasure) => this.Build(unitOfMeasure);

        private IEnumerable<LinkResource> BuildLinks(UnitOfMeasure unitOfMeasure)
        {
            throw new NotImplementedException();
        }
    }
}
