namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartCategoryResourceBuilder : IResourceBuilder<PartCategory>
    {
        public PartCategoryResource Build(PartCategory partCategory)
        {
            return new PartCategoryResource
                       {
                           Category = partCategory.Category,
                           Description = partCategory.Description,
                       };
        }

        public string GetLocation(PartCategory partCategory)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PartCategory>.Build(PartCategory partCategory) => this.Build(partCategory);

        private IEnumerable<LinkResource> BuildLinks(PartCategory partCategory)
        {
            throw new NotImplementedException();
        }
    }
}
