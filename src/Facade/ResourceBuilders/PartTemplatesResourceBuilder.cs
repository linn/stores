namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTemplatesResourceBuilder : IResourceBuilder<IEnumerable<PartTemplate>>
    {
        private readonly PartTemplateResourceBuilder partTemplateResourceBuilder = new PartTemplateResourceBuilder();

        public IEnumerable<PartTemplateResource> Build(IEnumerable<PartTemplate> templates)
        {
            return templates.OrderBy(t => t.PartRoot).Select(p => this.partTemplateResourceBuilder.Build(p));
        }

        object IResourceBuilder<IEnumerable<PartTemplate>>.Build(IEnumerable<PartTemplate> templates) => this.Build(templates);

        public string GetLocation(IEnumerable<PartTemplate> model)
        {
            throw new System.NotImplementedException();
        }
    }
}