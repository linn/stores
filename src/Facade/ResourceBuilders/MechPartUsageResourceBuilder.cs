namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartUsageResourceBuilder : IResourceBuilder<MechPartUsage>
    {
        public MechPartUsageResource Build(MechPartUsage model)
        {
            return new MechPartUsageResource
                       {
                          RootProductName = model.RootProductName,
                          RootProductDescription = model.RootProduct?.Description,
                          QuantityUsed = model.QuantityUsed,
                          SourceId = model.SourceId
                       };
        }

        object IResourceBuilder<MechPartUsage>.Build(MechPartUsage q) => this.Build(q);

        public string GetLocation(MechPartUsage model)
        {
            throw new System.NotImplementedException();
        }
    }
}
