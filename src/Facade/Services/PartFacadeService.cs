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
                           CccCriticalPart = resource.CccCriticalPart ? "Y" : "N",
                           EmcCriticalPart = resource.EmcCriticalPart ? "Y" : "N",
                           SafetyCriticalPart = resource.SafetyCriticalPart ? "Y" : "N",
                           SingleSourcePart = resource.SingleSourcePart ? "Y" : "N",
                           StockControlled = resource.StockControlled ? "Y" : "N",
                           ParetoClass =
                               resource.ParetoCode != null
                                   ? this.paretoClassRepository.FindById(resource.ParetoCode)
                                   : null,
                           PerformanceCriticalPart = resource.PerformanceCriticalPart ? "Y" : "N",
                           ProductAnalysisCode =
                               resource.ProductAnalysisCode != null
                                   ? this.productAnalysisCodeRepository.FindById(resource.ProductAnalysisCode)
                                   : null,
                           PsuPart = resource.PsuPart ? "Y" : "N",
                           RootProduct = resource.RootProduct ? "Y" : "N",
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
            entity.CccCriticalPart = resource.CccCriticalPart ? "Y" : "N";
            entity.EmcCriticalPart = resource.EmcCriticalPart ? "Y" : "N";
            entity.SafetyCriticalPart = resource.SafetyCriticalPart ? "Y" : "N";
            entity.SingleSourcePart = resource.SingleSourcePart ? "Y" : "N";
            entity.StockControlled = resource.StockControlled ? "Y" : "N";
            entity.ParetoClass = resource.ParetoCode != null
                                     ? this.paretoClassRepository.FindById(resource.ParetoCode)
                                     : null;
            entity.PerformanceCriticalPart = resource.PerformanceCriticalPart ? "Y" : "N";
            entity.ProductAnalysisCode = resource.ProductAnalysisCode != null
                                             ? this.productAnalysisCodeRepository.FindById(resource.ProductAnalysisCode)
                                             : null;
            entity.PsuPart = resource.PsuPart ? "Y" : "N";
            entity.RootProduct = resource.RootProduct ? "Y" : "N";
            entity.SafetyCertificateExpirationDate = string.IsNullOrEmpty(resource.SafetyCertificateExpirationDate)
                                                         ? (DateTime?)null
                                                         : DateTime.Parse(resource.SafetyCertificateExpirationDate);
            entity.SafetyDataDirectory = resource.SafetyDataDirectory;
        }

        protected override Expression<Func<Part, bool>> SearchExpression(string searchTerm)
        {
            return part => part.PartNumber.ToUpper().Equals(searchTerm.ToUpper());
        }
    }
}