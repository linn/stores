namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    public class RsnConditionResourceBuilder : IResourceBuilder<RsnCondition>
    {
        public RsnConditionResource Build(RsnCondition model)
        {
            return new RsnConditionResource
                       {
                           Code = model.Code, 
                           Description = model.Description, 
                           ExtraInfoRequired = model.ExtraInfoRequired
                       };
        }

        object IResourceBuilder<RsnCondition>.Build(RsnCondition model) => this.Build(model);

        public string GetLocation(RsnCondition model)
        {
            throw new System.NotImplementedException();
        }
    }
}
