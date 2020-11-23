namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartAltResourceBuilder : IResourceBuilder<MechPartAlt>
    {
        public MechPartAltResource Build(MechPartAlt model)
        {
            return new MechPartAltResource
                       {
                           Sequence = model.Sequence,
                           SupplierId = model.Supplier.Id,
                           SupplierName = model.Supplier.Name,
                           PartNumber = model.PartNumber
                       };
        }

        object IResourceBuilder<MechPartAlt>.Build(MechPartAlt alt) => this.Build(alt);

        public string GetLocation(MechPartAlt model)
        {
            throw new System.NotImplementedException();
        }
    }
}
