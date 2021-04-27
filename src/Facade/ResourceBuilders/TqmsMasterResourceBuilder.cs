namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Resources.Tqms;

    public class TqmsMasterResourceBuilder : IResourceBuilder<TqmsMaster>
    {
        public TqmsMasterResource Build(TqmsMaster master)
        {
            return new TqmsMasterResource
                       {
                           JobRef = master.JobRef,
                           Links = this.BuildLinks(master).ToArray()
                       };
        }

        public string GetLocation(TqmsMaster master)
        {
            return "/inventory/tqms-master";
        }

        object IResourceBuilder<TqmsMaster>.Build(TqmsMaster master) => this.Build(master);

        private IEnumerable<LinkResource> BuildLinks(TqmsMaster master)
        {
            yield return new LinkResource("self", this.GetLocation(master));
        }
    }
}
