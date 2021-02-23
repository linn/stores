namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class RsnsResourceBuilder : IResourceBuilder<IEnumerable<Rsn>>
    {
        private readonly RsnResourceBuilder rsnResourceBuilder = new RsnResourceBuilder();

        public IEnumerable<RsnResource> Build(IEnumerable<Rsn> rsns)
        {
            return rsns.Select(r => this.rsnResourceBuilder.Build(r));
        }

        public string GetLocation(IEnumerable<Rsn> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<Rsn>>.Build(IEnumerable<Rsn> rsns) => this.Build(rsns);
    }
}