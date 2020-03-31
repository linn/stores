namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    public class PartFacadeService : FacadeService<Part, int, PartResource, PartResource>
    {
        private readonly IRepository<ParetoClass, string> paretoClassRepository;

        private readonly IQueryRepository<ProductAnalysisCode> productAnalysisCodeRepository;

        private readonly IQueryRepository<AccountingCompany> accountingCompanyRepository;

        private readonly IQueryRepository<NominalAccount> nominalAccountRepository;

        public PartFacadeService(
            IRepository<Part, int> repository,
            IRepository<ParetoClass, string> paretoClassRepository,
            IQueryRepository<ProductAnalysisCode> productAnalysisCodeRepository,
            IQueryRepository<AccountingCompany> accountingCompanyRepository,
            IQueryRepository<NominalAccount> nominalAccountRepository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.paretoClassRepository = paretoClassRepository;
            this.productAnalysisCodeRepository = productAnalysisCodeRepository;
            this.accountingCompanyRepository = accountingCompanyRepository;
            this.nominalAccountRepository = nominalAccountRepository;
        }

        protected override Part CreateFromResource(PartResource resource)
        {
            return new Part
                       {
                           PartNumber = resource.PartNumber,
                           Description = resource.Description,
                           AccountingCompany = this.accountingCompanyRepository.FindBy(c => c.Name == resource.AccountingCompany),
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
                                   ? this.productAnalysisCodeRepository.FindBy(c => c.ProductCode == resource.ProductAnalysisCode)
                                   : null,
                           PsuPart = this.ToYesOrNoString(resource.PsuPart),
                           RootProduct = resource.RootProduct,
                           SafetyCertificateExpirationDate =
                               string.IsNullOrEmpty(resource.SafetyCertificateExpirationDate)
                                   ? (DateTime?)null
                                   : DateTime.Parse(resource.SafetyCertificateExpirationDate),
                           SafetyDataDirectory = resource.SafetyDataDirectory,
                           NominalAccount = this.nominalAccountRepository.FindBy(
                               a => a.Nominal.NominalCode == resource.Nominal && a.Department == resource.Department)
        };
        }

        protected override void UpdateFromResource(Part entity, PartResource resource)
        {
            entity.Description = resource.Description;
            entity.AccountingCompany =
                this.accountingCompanyRepository.FindBy(c => c.Name == resource.AccountingCompany);
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
                                             ? this.productAnalysisCodeRepository.FindBy(c => c.ProductCode == resource.ProductAnalysisCode)
                                             : null;
            entity.PsuPart = this.ToYesOrNoString(resource.PsuPart);
            entity.RootProduct = resource.RootProduct;
            entity.SafetyCertificateExpirationDate = string.IsNullOrEmpty(resource.SafetyCertificateExpirationDate)
                                                         ? (DateTime?)null
                                                         : DateTime.Parse(resource.SafetyCertificateExpirationDate);
            entity.SafetyDataDirectory = resource.SafetyDataDirectory;
            entity.NominalAccount = this.nominalAccountRepository.FindBy(
                a => a.Nominal.NominalCode == resource.Nominal && a.Department == resource.Department);
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