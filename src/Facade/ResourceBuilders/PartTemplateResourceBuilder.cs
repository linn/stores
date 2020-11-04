namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
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
                           AccountingCompany = model.AccountingCompany,
                           ProductCode = model.ProductCode,
                           StockControlled = model.StockControlled,
                           LinnProduced = model.LinnProduced,
                           BomType = model.BomType,
                           AssemblyTechnology = model.AssemblyTechnology,
                           AllowPartCreation = model.AllowPartCreation,
                           ParetoCode = model.ParetoCode
                       };
        }

        object IResourceBuilder<PartTemplate>.Build(PartTemplate template) => this.Build(template);

        public string GetLocation(PartTemplate model)
        {
            throw new System.NotImplementedException();
        }
    }
}
