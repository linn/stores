namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class DecrementRuleResourceBuilder : IResourceBuilder<DecrementRule>
    {
        public DecrementRuleResource Build(DecrementRule decrementRule)
        {
            return new DecrementRuleResource
                       {
                           Rule = decrementRule.Rule,
                           Description = decrementRule.Description,
                       };
        }

        public string GetLocation(DecrementRule decrementRule)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<DecrementRule>.Build(DecrementRule decrementRule) => this.Build(decrementRule);

        private IEnumerable<LinkResource> BuildLinks(DecrementRule decrementRule)
        {
            throw new NotImplementedException();
        }
    }
}