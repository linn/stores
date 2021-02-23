namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    public class WandItemResultResourceBuilder : IResourceBuilder<WandResult>
    {
        public WandItemResultResource Build(WandResult wandResult)
        {
            return new WandItemResultResource
                       {
                           Message = wandResult.Message,
                           Success = wandResult.Success
                       };
        }

        public string GetLocation(WandResult wandResult)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<WandResult>.Build(WandResult wandResult) => this.Build(wandResult);
    }
}
