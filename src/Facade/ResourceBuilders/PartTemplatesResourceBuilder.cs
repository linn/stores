namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTemplatesResourceBuilder : IResourceBuilder<IEnumerable<ResponseModel<PartTemplate>>>
    {
        private readonly IAuthorisationService authorisationService;

        private readonly PartTemplateResourceBuilder partTemplateResourceBuilder;

        public PartTemplatesResourceBuilder(
            IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
            this.partTemplateResourceBuilder = new PartTemplateResourceBuilder(authorisationService);
        }


        public IEnumerable<PartTemplateResource> Build(IEnumerable<ResponseModel<PartTemplate>> templates)
        {
            return templates.OrderBy(t => t.ResponseData.PartRoot).Select(p => this.partTemplateResourceBuilder.Build(p));
        }

        object IResourceBuilder<IEnumerable<ResponseModel<PartTemplate>>>.Build(IEnumerable<ResponseModel<PartTemplate>> templates) => this.Build(templates);

        public string GetLocation(IEnumerable<ResponseModel<PartTemplate>> model)
        {
            throw new System.NotImplementedException();
        }
    }
}
