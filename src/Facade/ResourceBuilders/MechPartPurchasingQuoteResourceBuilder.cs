namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartPurchasingQuoteResourceBuilder : IResourceBuilder<MechPartPurchasingQuote>
    {
        public MechPartPurchasingQuoteResource Build(MechPartPurchasingQuote model)
        {
            return new MechPartPurchasingQuoteResource
                       {
                           SupplierId = model.SupplierId,
                           SupplierName = model.Supplier?.Name,
                           LeadTime = model.LeadTime,
                           ManufacturerCode = model.ManufacturerCode,
                           ManufacturerDescription = model.Manufacturer?.Description,
                           ManufacturersPartNumber = model.ManufacturersPartNumber,
                           Moq = model.Moq,
                           RohsCompliant = model.RohsCompliant,
                           UnitPrice = model.UnitPrice,
                           SourceId = model.SourceId
                       };
        }

        object IResourceBuilder<MechPartPurchasingQuote>.Build(MechPartPurchasingQuote q) => this.Build(q);

        public string GetLocation(MechPartPurchasingQuote model)
        {
            throw new System.NotImplementedException();
        }
    }
}