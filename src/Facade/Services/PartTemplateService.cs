namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTemplateService : FacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource>
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
            throw new NotImplementedException();
        }
    }
}
