namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Stores.Resources.Scs;

    public class ScsPalletsResourceBuilder : IResourceBuilder<IEnumerable<ScsPallet>>
    {
        private readonly ScsPalletResourceBuilder palletResourceBuilder = new ScsPalletResourceBuilder();

        public IEnumerable<ScsPalletResource> Build(IEnumerable<ScsPallet> pallets)
        {
            return pallets
                .Select(a =>
                    {
                        return this.palletResourceBuilder.Build(a);
                    });
        }

        object IResourceBuilder<IEnumerable<ScsPallet>>.Build(IEnumerable<ScsPallet> pallets) => this.Build(pallets);

        public string GetLocation(IEnumerable<ScsPallet> pallets)
        {
            throw new NotImplementedException();
        }
    }
}
