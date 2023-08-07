namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTemplatesResourceBuilder : IResourceBuilder<ResponseModel<IEnumerable<PartTemplate>>>
    {
        private readonly IAuthorisationService authorisationService;

        private readonly PartTemplateResourceBuilder partTemplateResourceBuilder;

        public PartTemplatesResourceBuilder(
            IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
            this.partTemplateResourceBuilder = new PartTemplateResourceBuilder(authorisationService);
        }

        public IEnumerable<PartTemplateResource> Build(ResponseModel<IEnumerable<PartTemplate>> templates)
        {
            return templates.ResponseData.OrderBy(t => t.PartRoot).Select(p => this.partTemplateResourceBuilder.Build(new ResponseModel<PartTemplate>(p, templates.Privileges)));
        }

        object IResourceBuilder<ResponseModel<IEnumerable<PartTemplate>>>.Build(ResponseModel<IEnumerable<PartTemplate>> templates) => this.Build(templates);

        public string GetLocation(ResponseModel<IEnumerable<PartTemplate>> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
