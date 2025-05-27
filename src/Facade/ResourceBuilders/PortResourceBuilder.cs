namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class PortResourceBuilder : IResourceBuilder<Port>
    {
        public PortResource Build(Port model)
        {
            return new PortResource
                       {
                           PortCode = model.PortCode, Description = model.Description, SortOrder = model.SortOrder
                       };
        }

        object IResourceBuilder<Port>.Build(Port model) => this.Build(model);

        public string GetLocation(Port model)
        {
            throw new System.NotImplementedException();
        }
    }
}
