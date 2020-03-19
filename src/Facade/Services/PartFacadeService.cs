namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class PartFacadeService : FacadeService<Part, int, PartResource, PartResource>
    {
        private readonly IRepository<ParetoClass, string> paretoClassRepository;

        private readonly IRepository<ProductAnalysisCode, string> productAnalysisCodeRepository;

        public PartFacadeService(
            IRepository<Part, int> repository,
            IRepository<ParetoClass, string> paretoClassRepository,
            IRepository<ProductAnalysisCode, string> productAnalysisCodeRepository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.paretoClassRepository = paretoClassRepository;
            this.productAnalysisCodeRepository = productAnalysisCodeRepository;
        }

        protected override Part CreateFromResource(PartResource resource)
        {
            return new Part
                       {
                           PartNumber = resource.PartNumber,
                           Description = resource.Description,
                           AccountingCompany = resource.AccountingCompany,
                           CccCriticalPart = this.ToYesOrNoString(resource.CccCriticalPart),
                           EmcCriticalPart = this.ToYesOrNoString(resource.EmcCriticalPart),
                           SafetyCriticalPart = this.ToYesOrNoString(resource.SafetyCriticalPart),
                           SingleSourcePart = this.ToYesOrNoString(resource.SingleSourcePart),
                           StockControlled = this.ToYesOrNoString(resource.StockControlled),
                           ParetoClass =
                               resource.ParetoCode != null
                                   ? this.paretoClassRepository.FindById(resource.ParetoCode)
                                   : null,
                           PerformanceCriticalPart = this.ToYesOrNoString(resource.PerformanceCriticalPart),
                           ProductAnalysisCode =
                               resource.ProductAnalysisCode != null
                                   ? this.productAnalysisCodeRepository.FindById(resource.ProductAnalysisCode)
                                   : null,
                           PsuPart = this.ToYesOrNoString(resource.PsuPart),
                           RootProduct = this.ToYesOrNoString(resource.RootProduct),
                           SafetyCertificateExpirationDate =
                               string.IsNullOrEmpty(resource.SafetyCertificateExpirationDate)
                                   ? (DateTime?)null
                                   : DateTime.Parse(resource.SafetyCertificateExpirationDate),
                           SafetyDataDirectory = resource.SafetyDataDirectory
                       };
        }

        protected override void UpdateFromResource(Part entity, PartResource resource)
        {
            entity.Description = resource.Description;
            entity.AccountingCompany = resource.AccountingCompany;
            entity.CccCriticalPart = this.ToYesOrNoString(resource.CccCriticalPart);
            entity.EmcCriticalPart = this.ToYesOrNoString(resource.EmcCriticalPart);
            entity.SafetyCriticalPart = this.ToYesOrNoString(resource.SafetyCriticalPart);
            entity.SingleSourcePart = this.ToYesOrNoString(resource.SingleSourcePart);
            entity.StockControlled = this.ToYesOrNoString(resource.StockControlled);
            entity.ParetoClass = resource.ParetoCode != null
                                     ? this.paretoClassRepository.FindById(resource.ParetoCode)
                                     : null;
            entity.PerformanceCriticalPart = this.ToYesOrNoString(resource.PerformanceCriticalPart);
            entity.ProductAnalysisCode = resource.ProductAnalysisCode != null
                                             ? this.productAnalysisCodeRepository.FindById(resource.ProductAnalysisCode)
                                             : null;
            entity.PsuPart = this.ToYesOrNoString(resource.PsuPart);
            entity.RootProduct = this.ToYesOrNoString(resource.RootProduct);
            entity.SafetyCertificateExpirationDate = string.IsNullOrEmpty(resource.SafetyCertificateExpirationDate)
                                                         ? (DateTime?)null
                                                         : DateTime.Parse(resource.SafetyCertificateExpirationDate);
            entity.SafetyDataDirectory = resource.SafetyDataDirectory;
        }

        protected override Expression<Func<Part, bool>> SearchExpression(string searchTerm)
        {
            return part => part.PartNumber.ToUpper().Equals(searchTerm.ToUpper());
        }

        private string ToYesOrNoString(bool? booleanRepresentation)
        {
            if (booleanRepresentation == null)
            {
                return null;
            }

            return (bool)booleanRepresentation ? "Y" : "N";
        }
    }
}