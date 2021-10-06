using System;
using System.Collections.Generic;
using System.Linq;
using Linn.Common.Facade;
using Linn.Stores.Domain.LinnApps;
using Linn.Stores.Resources;

namespace Linn.Stores.Facade.ResourceBuilders
{
    public class RsnsResourceBuilder : IResourceBuilder<IEnumerable<Rsn>>
    {
        private readonly RsnResourceBuilder rsnResourceBuilder = new RsnResourceBuilder();

        object IResourceBuilder<IEnumerable<Rsn>>.Build(IEnumerable<Rsn> rsns)
        {
            return Build(rsns);
        }

        public string GetLocation(IEnumerable<Rsn> rsns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RsnResource> Build(IEnumerable<Rsn> rsns)
        {
            return rsns.Select(a => rsnResourceBuilder.Build(a));
        }
    }
}
