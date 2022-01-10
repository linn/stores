namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Domain.LinnApps;

    public class PartTemplateResourceBuilder : IResourceBuilder<ResponseModel<PartTemplate>>
    {

        private readonly IAuthorisationService authorisationService;

        public PartTemplateResourceBuilder(
            IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;
        }

        public PartTemplateResource Build(ResponseModel<PartTemplate> model)
        {
            return new PartTemplateResource
                       {
                           PartRoot = model.ResponseData.PartRoot,
                           Description = model.ResponseData.Description,
                           HasDataSheet = model.ResponseData.HasDataSheet,
                           HasNumberSequence = model.ResponseData.HasNumberSequence,
                           NextNumber = model.ResponseData.NextNumber,
                           AllowVariants = model.ResponseData.AllowVariants,
                           Variants = model.ResponseData.Variants,
                           AccountingCompany = model.ResponseData.AccountingCompany,
                           ProductCode = model.ResponseData.ProductCode,
                           StockControlled = model.ResponseData.StockControlled,
                           LinnProduced = model.ResponseData.LinnProduced,
                           RmFg = model.ResponseData.RmFg,
                           BomType = model.ResponseData.BomType,
                           AssemblyTechnology = model.ResponseData.AssemblyTechnology,
                           AllowPartCreation = model.ResponseData.AllowPartCreation,
                           ParetoCode = model.ResponseData.ParetoCode,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        object IResourceBuilder<ResponseModel<PartTemplate>>.Build(ResponseModel<PartTemplate> template) => this.Build(template);


        public string GetLocation(ResponseModel<PartTemplate> model)
        {
            return $"/inventory/part-templates/{model.ResponseData.PartRoot}";
        }

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<PartTemplate> model)
        {
            if (this.authorisationService.HasPermissionFor(AuthorisedAction.PartAdmin, model.Privileges))
            {
                yield return new LinkResource { Rel = "create", Href = "/inventory/part-templates/" };
                yield return new LinkResource { Rel = "edit", Href = "/inventory/part-templates/" };
            }

            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };
        }
    }
}
