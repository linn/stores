namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class UnitsOfMeasureResourceBuilder : IResourceBuilder<IEnumerable<UnitOfMeasure>>
    {
        private readonly UnitOfMeasureResourceBuilder unitOfMeasureResourceBuilder = new UnitOfMeasureResourceBuilder();

        public IEnumerable<UnitOfMeasureResource> Build(IEnumerable<UnitOfMeasure> unitOfMeasures)
        {
            return unitOfMeasures
                .Select(a => this.unitOfMeasureResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<UnitOfMeasure>>.Build(IEnumerable<UnitOfMeasure> unitOfMeasures) => this.Build(unitOfMeasures);

        public string GetLocation(IEnumerable<UnitOfMeasure> unitOfMeasures)
        {
            throw new NotImplementedException();
        }
    }
}
