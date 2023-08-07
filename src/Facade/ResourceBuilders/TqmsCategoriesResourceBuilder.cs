namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Resources.Tqms;

    public class TqmsCategoriesResourceBuilder : IResourceBuilder<IEnumerable<TqmsCategory>>
    {
        public IEnumerable<TqmsCategoryResource> Build(IEnumerable<TqmsCategory> tqmsCategories)
        {
            return tqmsCategories
                .Select(
                a => new TqmsCategoryResource
                         {
                             Category = a.Category,
                             Description = a.Description,
                             Explanation = a.Explanation,
                             SortOrder = a.SortOrder
                         });
        }

        public string GetLocation(IEnumerable<TqmsCategory> tqmsCategories)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<TqmsCategory>>.Build(IEnumerable<TqmsCategory> categories)
        {
            return this.Build(categories);
        }
    }
}
