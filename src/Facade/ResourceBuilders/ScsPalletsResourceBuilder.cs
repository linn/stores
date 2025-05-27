namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Stores.Resources.Scs;
    using Linn.Stores.Domain.LinnApps.Scs;

    public class ScsPalletsResourceBuilder : IResourceBuilder<IEnumerable<ScsPallet>>
    {
        private readonly ScsPalletResourceBuilder palletResourceBuilder = new ScsPalletResourceBuilder();

        public ScsPalletsResource Build(IEnumerable<ScsPallet> pallets)
        {
            return new ScsPalletsResource
                       {
                           data = pallets.Select(
                               a =>
                                   {
                                       return this.palletResourceBuilder.Build(a);
                                   })
                       };
        }

        object IResourceBuilder<IEnumerable<ScsPallet>>.Build(IEnumerable<ScsPallet> pallets) => this.Build(pallets);

        public string GetLocation(IEnumerable<ScsPallet> pallets)
        {
            throw new NotImplementedException();
        }
    }
}
