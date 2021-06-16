namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ShippingTermResourceBuilder : IResourceBuilder<ShippingTerm>
    {
        public ShippingTermResource Build(ShippingTerm shippingTerm)
        {
            return new ShippingTermResource
            {
                Id = shippingTerm.Id,
                Code = shippingTerm.Code,
                Description = shippingTerm.Description,
                DateInvalid = shippingTerm.DateInvalid?.ToString("o"),
            };
        }

        public string GetLocation(ShippingTerm shippingTerm)
        {
            return $"/logistics/shipping-terms/{shippingTerm.Id}";
        }

        object IResourceBuilder<ShippingTerm>.Build(ShippingTerm shippingTerm) => this.Build(shippingTerm);
    }
}
