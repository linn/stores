namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class PortsResourceBuilder : IResourceBuilder<IEnumerable<Port>>
    {
        private readonly PortResourceBuilder portResourceBuilder = new PortResourceBuilder();

        public IEnumerable<PortResource> Build(IEnumerable<Port> ports)
        {
            return ports.OrderBy(b => b.PortCode).Select(a => this.portResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Port>>.Build(IEnumerable<Port> model) =>
            this.Build(model);

        public string GetLocation(IEnumerable<Port> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
