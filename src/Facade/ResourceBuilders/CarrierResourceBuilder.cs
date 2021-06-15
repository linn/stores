namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class CarrierResourceBuilder : IResourceBuilder<Carrier>
    {
        public CarrierResource Build(Carrier carrier)
        {
            return new CarrierResource
            {
                CarrierCode = carrier.CarrierCode,
                Name = carrier.Name,
                DateInvalid = carrier.DateInvalid?.ToString("o"),
            };
        }

        public string GetLocation(Carrier carrier)
        {
            return $"/logistics/carriers/{carrier.CarrierCode}";
        }

        object IResourceBuilder<Carrier>.Build(Carrier carrier) => this.Build(carrier);
    }
}
