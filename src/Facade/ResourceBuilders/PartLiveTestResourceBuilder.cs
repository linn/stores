namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartLiveTestResourceBuilder : IResourceBuilder<PartLiveTest>
    {
        public PartLiveTestResource Build(PartLiveTest model)
        {
            return new PartLiveTestResource
                        {
                            CanMakeLive = model.Result,
                            Message = model.Message
                        };
        }

        object IResourceBuilder<PartLiveTest>.Build(PartLiveTest test) => this.Build(test);

        public string GetLocation(PartLiveTest model)
        {
            throw new System.NotImplementedException();
        }
    }
}
