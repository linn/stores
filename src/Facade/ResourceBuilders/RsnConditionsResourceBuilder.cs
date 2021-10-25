namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    public class RsnConditionsResourceBuilder : IResourceBuilder<IEnumerable<RsnCondition>>
    {
        private readonly RsnConditionResourceBuilder portResourceBuilder = new RsnConditionResourceBuilder();

        public IEnumerable<RsnConditionResource> Build(IEnumerable<RsnCondition> conditions)
        {
            return conditions.OrderBy(b => b.Code).Select(a => this.portResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<RsnCondition>>.Build(IEnumerable<RsnCondition> model) => this.Build(model);

        public string GetLocation(IEnumerable<RsnCondition> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
