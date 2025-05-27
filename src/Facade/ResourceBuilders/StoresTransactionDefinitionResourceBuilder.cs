namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    public class StoresTransactionDefinitionResourceBuilder : IResourceBuilder<StoresTransactionDefinition>
    {
        public StoresTransactionDefinitionResource Build(StoresTransactionDefinition storesTransactionDefinition)
        {
            return new StoresTransactionDefinitionResource
            {
                TransactionCode = storesTransactionDefinition.TransactionCode, 
                Description = storesTransactionDefinition.Description,
            };
        }

        object IResourceBuilder<StoresTransactionDefinition>.Build(StoresTransactionDefinition storesTransactionDefinition) => this.Build(storesTransactionDefinition);

        public string GetLocation(StoresTransactionDefinition storesTransactionDefinition)
        {
            throw new NotImplementedException();
        }
    }
}
