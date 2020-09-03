namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartCategoriesResourceBuilder : IResourceBuilder<IEnumerable<PartCategory>>
    {
        private readonly PartCategoryResourceBuilder partCategoryResourceBuilder = new PartCategoryResourceBuilder();

        public IEnumerable<PartCategoryResource> Build(IEnumerable<PartCategory> partCategories)
        {
            return partCategories
                .Select(a => this.partCategoryResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartCategory>>.Build(IEnumerable<PartCategory> partCategories) => this.Build(partCategories);

        public string GetLocation(IEnumerable<PartCategory> partCategories)
        {
            throw new NotImplementedException();
        }
    }
}