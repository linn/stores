namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartDataSheetValuesResourceBuilder : IResourceBuilder<PartDataSheetValues>
    {
        public PartDataSheetValuesResource Build(PartDataSheetValues model)
        {
            return new PartDataSheetValuesResource
                       {
                           Description = model.Description,
                           AttributeSet = model.AttributeSet,
                           Field = model.Field,
                           Value = model.Value
                       };
        }

        public string GetLocation(PartDataSheetValues model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartDataSheetValues>.Build(PartDataSheetValues model) => this.Build(model);

        private IEnumerable<LinkResource> BuildLinks(PartDataSheetValues model)
        {
            throw new NotImplementedException();
        }
    }
}
