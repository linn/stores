namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class DespatchLocationResourceBuilder : IResourceBuilder<DespatchLocation>
    {
        public DespatchLocationResource Build(DespatchLocation despatchLocation)
        {
            return new DespatchLocationResource
                       {
                           Id = despatchLocation.Id,
                           DefaultCarrier = despatchLocation.DefaultCarrier,
                           Sequence = despatchLocation.Sequence,
                           LocationCode = despatchLocation.LocationCode,
                           LocationId = despatchLocation.LocationId,
                           DateInvalid = despatchLocation.DateInvalid?.ToString("o"),
                           UnAllocLocationId = despatchLocation.UnAllocLocationId,
                           Links = this.BuildLinks(despatchLocation).ToArray()
                       };
        }

        public string GetLocation(DespatchLocation despatchLocation)
        {
            return $"/logistics/despatch-locations/{despatchLocation.Id}";
        }

        object IResourceBuilder<DespatchLocation>.Build(DespatchLocation despatchLocation) => this.Build(despatchLocation);

        private IEnumerable<LinkResource> BuildLinks(DespatchLocation despatchLocation)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(despatchLocation) };
        }
    }
}