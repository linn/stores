namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTemplateResourceBuilder : IResourceBuilder<PartTemplate>
    {
        public PartTemplateResource Build(PartTemplate model)
        {
            return new PartTemplateResource
                       {
                           PartRoot = model.PartRoot,
                           Description = model.Description,
                           HasDataSheet = model.HasDataSheet,
                           HasNumberSequence = model.HasNumberSequence,
                           NextNumber = model.NextNumber,
                           AllowVariants = model.AllowVariants,
                           Variants = model.Variants,
                           AccountingCompany = model.AccountingCompany,
                           ProductCode = model.ProductCode,
                           StockControlled = model.StockControlled,
                           LinnProduced = model.LinnProduced,
                           RmFg = model.RmFg,
                           BomType = model.BomType,
                           AssemblyTechnology = model.AssemblyTechnology,
                           AllowPartCreation = model.AllowPartCreation,
                           ParetoCode = model.ParetoCode
                       };
        }

        object IResourceBuilder<PartTemplate>.Build(PartTemplate template) => this.Build(template);

        public string GetLocation(PartTemplate model)
        {
            return $"/inventory/part-templates/{model.PartRoot}";
        }

        private IEnumerable<LinkResource> BuildLinks(PartTemplate model)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };
        }
    }
}
