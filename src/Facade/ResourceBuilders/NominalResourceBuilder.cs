namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class NominalResourceBuilder : IResourceBuilder<Nominal>
    {
        public NominalResource Build(Nominal nominal)
        {
            return new NominalResource
                       {
                           NominalCode = nominal.NominalCode,
                           Description = nominal.Description,
                       };
        }

        public string GetLocation(Nominal nominal)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Nominal>.Build(Nominal nominal) => this.Build(nominal);

        private IEnumerable<LinkResource> BuildLinks(Nominal nominal)
        {
            throw new NotImplementedException();
        }
    }
}