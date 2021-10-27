namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    public class RsnAccessoryResourceBuilder : IResourceBuilder<RsnAccessory>
    {
        public RsnAccessoryResource Build(RsnAccessory model)
        {
            return new RsnAccessoryResource
                       {
                           Code = model.Code,
                           Description = model.Description,
                           ExtraInfoRequired = model.ExtraInfoRequired
                       };
        }

        object IResourceBuilder<RsnAccessory>.Build(RsnAccessory model) => this.Build(model);

        public string GetLocation(RsnAccessory model)
        {
            throw new System.NotImplementedException();
        }
    }
}
