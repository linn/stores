namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;

    public class PartTemplateService : FacadeFilterService<PartTemplate, string, PartTemplateResource, PartTemplateResource, PartTemplateSearchRequestResource>
    {
        public PartTemplateService(IRepository<PartTemplate, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartTemplate CreateFromResource(PartTemplateResource resource)
        {
            var partTemplate = new PartTemplate
            {
                PartRoot = resource.PartRoot,
                Description = resource.Description,
                HasDataSheet = resource.HasDataSheet,
                HasNumberSequence = resource.HasNumberSequence,
                NextNumber = resource.NextNumber,
                AllowVariants = resource.AllowVariants,
                Variants = resource.Variants,
                AccountingCompany = resource.AccountingCompany,
                ProductCode = resource.ProductCode,
                StockControlled = resource.StockControlled,
                LinnProduced = resource.LinnProduced,
                BomType = resource.BomType,
                RmFg = resource.RmFg,
                AssemblyTechnology = resource.AssemblyTechnology,
                AllowPartCreation = resource.AllowPartCreation,
                ParetoCode = resource.ParetoCode
            };

            return partTemplate;
        }

        protected override void UpdateFromResource(PartTemplate entity, PartTemplateResource updateResource)
        {
            entity.PartRoot = updateResource.PartRoot;
            entity.Description = updateResource.Description;
            entity.HasDataSheet = updateResource.HasDataSheet;
            entity.HasNumberSequence = updateResource.HasNumberSequence;
            entity.NextNumber = updateResource.NextNumber;
            entity.AllowVariants = updateResource.AllowVariants;
            entity.Variants = updateResource.Variants;
            entity.AccountingCompany = updateResource.AccountingCompany;
            entity.ProductCode = updateResource.ProductCode;
            entity.StockControlled = updateResource.StockControlled;
            entity.LinnProduced = updateResource.LinnProduced;
            entity.BomType = updateResource.BomType;
            entity.RmFg = updateResource.RmFg;
            entity.AssemblyTechnology = updateResource.AssemblyTechnology;
            entity.AllowPartCreation = updateResource.AllowPartCreation;
            entity.ParetoCode = updateResource.ParetoCode;
        }

        protected override Expression<Func<PartTemplate, bool>> SearchExpression(string searchTerm)
        {
            return partTemplate => partTemplate.PartRoot.ToUpper().Contains(searchTerm.ToUpper())
                           || partTemplate.Description.ToUpper().Contains(searchTerm.ToUpper())
                           || partTemplate.AccountingCompany.ToUpper().Contains(searchTerm.ToUpper())
                           || partTemplate.ProductCode.ToUpper().Contains(searchTerm.ToUpper())
                           || partTemplate.AssemblyTechnology.ToUpper().Contains(searchTerm.ToUpper())
                           || partTemplate.ParetoCode.ToUpper().Contains(searchTerm.ToUpper());
        }

        protected override Expression<Func<PartTemplate, bool>> FilterExpression(PartTemplateSearchRequestResource searchTerms)
        {
            return x =>
                (string.IsNullOrWhiteSpace(searchTerms.PartRootSearchTerm)
                 || x.PartRoot.ToString().Contains(searchTerms.PartRootSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.AccountingCompanySearchTerm)
                    || x.AccountingCompany.Contains(searchTerms.AccountingCompanySearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.AssemblyTechnologySearchTerm)
                    || x.AssemblyTechnology.Contains(searchTerms.AssemblyTechnologySearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.DescriptionSearchTerm)
                    || x.Description.Contains(searchTerms.DescriptionSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.ParetoCodeSearchTerm)
                    || x.ParetoCode.Contains(searchTerms.ParetoCodeSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.ProductCodeSearchTerm)
                    || x.ProductCode.Contains(searchTerms.ProductCodeSearchTerm));
        }
    }
}
