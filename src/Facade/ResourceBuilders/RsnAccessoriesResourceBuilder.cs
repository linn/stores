namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    public class RsnAccessoriesResourceBuilder : IResourceBuilder<IEnumerable<RsnAccessory>>
    {
        private readonly RsnAccessoryResourceBuilder portResourceBuilder = new RsnAccessoryResourceBuilder();

        public IEnumerable<RsnAccessoryResource> Build(IEnumerable<RsnAccessory> accessories)
        {
            return accessories.OrderBy(b => b.Code).Select(a => this.portResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<RsnAccessory>>.Build(IEnumerable<RsnAccessory> model) => this.Build(model);

        public string GetLocation(IEnumerable<RsnAccessory> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
