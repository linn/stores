namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class HubResourceBuilder : IResourceBuilder<Hub>
    {
        public HubResource Build(Hub hub)
        {
            return new HubResource
            {
                HubId = hub.HubId,
                AddressId = hub.AddressId,
                CarrierCode = hub.CarrierCode, 
                CustomStamp = hub.CustomStamp,
                Description = hub.Description,
                EcHub = hub.EcHub,
                OrgId = hub.OrgId,
                ReturnAccountingCompany = hub.ReturnAccountingCompany,
                ReturnAddressId = hub.ReturnAddressId,
                ReturnCustomStamp = hub.ReturnCustomStamp,
                Links = this.BuildLinks(hub).ToArray()
            };
        }

        public string GetLocation(Hub p)
        {
            return $"/logistics/hubs/{p.HubId}";
        }

        object IResourceBuilder<Hub>.Build(Hub hub) => this.Build(hub);

        private IEnumerable<LinkResource> BuildLinks(Hub hub)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(hub) };
        }
    }
}
