namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartDataSheetValuesListResourceBuilder : IResourceBuilder<IEnumerable<PartDataSheetValues>>
    {
        private readonly PartDataSheetValuesResourceBuilder partDataSheetsResourceBuilderResourceBuilder = new PartDataSheetValuesResourceBuilder();

        public IEnumerable<PartDataSheetValuesResource> Build(IEnumerable<PartDataSheetValues> entities)
        {
            return entities
                .Select(a => this.partDataSheetsResourceBuilderResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartDataSheetValues>>.Build(IEnumerable<PartDataSheetValues> entities) => this.Build(entities);

        public string GetLocation(IEnumerable<PartDataSheetValues> entities)
        {
            throw new NotImplementedException();
        }
    }
}
