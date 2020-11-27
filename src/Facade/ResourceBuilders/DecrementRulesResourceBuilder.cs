namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class DecrementRulesResourceBuilder : IResourceBuilder<IEnumerable<DecrementRule>>
    {
        private readonly DecrementRuleResourceBuilder decrementRuleResourceBuilder = new DecrementRuleResourceBuilder();

        public IEnumerable<DecrementRuleResource> Build(IEnumerable<DecrementRule> decrementRules)
        {
            return decrementRules
                .Select(a => this.decrementRuleResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<DecrementRule>>.Build(IEnumerable<DecrementRule> decrementRules) => this.Build(decrementRules);

        public string GetLocation(IEnumerable<DecrementRule> decrementRules)
        {
            throw new NotImplementedException();
        }
    }
}
