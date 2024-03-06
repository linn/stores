namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    public class StoresTransactionDefinitionsResourceBuilder : IResourceBuilder<IEnumerable<StoresTransactionDefinition>>
    {
        private readonly StoresTransactionDefinitionResourceBuilder storesTransactionDefinitionResourceBuilder = new StoresTransactionDefinitionResourceBuilder();

        public IEnumerable<StoresTransactionDefinitionResource> Build(IEnumerable<StoresTransactionDefinition> storesTransactionDefinitions)
        {
            return storesTransactionDefinitions
                .OrderBy(b => b.TransactionCode)
                .Select(a => this.storesTransactionDefinitionResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<StoresTransactionDefinition>>.Build(IEnumerable<StoresTransactionDefinition> storesTransactionDefinitions) => this.Build(storesTransactionDefinitions);

        public string GetLocation(IEnumerable<StoresTransactionDefinition> storesTransactionDefinitions)
        {
            throw new NotImplementedException();
        }
    }
}
