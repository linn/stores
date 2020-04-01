﻿namespace Linn.Stores.Facade.Services
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

        private readonly IRepository<AssemblyTechnology, string> assemblyTechnologyRepository;

        private readonly IRepository<DecrementRule, string> decrementRuleRepository;

        private readonly IQueryRepository<ProductAnalysisCode> productAnalysisCodeRepository;

        private readonly IQueryRepository<AccountingCompany> accountingCompanyRepository;

        private readonly IQueryRepository<NominalAccount> nominalAccountRepository;

        private readonly IQueryRepository<SernosSequence> sernosSequenceRepository;

        public PartFacadeService(
            IRepository<Part, int> repository,
            IRepository<ParetoClass, string> paretoClassRepository,
            IQueryRepository<ProductAnalysisCode> productAnalysisCodeRepository,
            IQueryRepository<AccountingCompany> accountingCompanyRepository,
            IQueryRepository<NominalAccount> nominalAccountRepository,
            IRepository<AssemblyTechnology, string> assemblyTechnologyRepository,
            IRepository<DecrementRule, string> decrementRuleRepository,
            IQueryRepository<SernosSequence> sernosSequenceRepository,
            ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.paretoClassRepository = paretoClassRepository;
            this.productAnalysisCodeRepository = productAnalysisCodeRepository;
            this.accountingCompanyRepository = accountingCompanyRepository;
            this.nominalAccountRepository = nominalAccountRepository;
            this.decrementRuleRepository = decrementRuleRepository;
            this.assemblyTechnologyRepository = assemblyTechnologyRepository;
            this.sernosSequenceRepository = sernosSequenceRepository;
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
                               a => a.Nominal.NominalCode == resource.Nominal && a.Department.DepartmentCode == resource.Department),
                           DecrementRule = resource.DecrementRuleName != null
                                               ? this.decrementRuleRepository.FindBy(c => c.Rule == resource.DecrementRuleName)
                                               : null,
                           AssemblyTechnology = resource.AssemblyTechnologyName != null
                                                    ? this.assemblyTechnologyRepository.FindBy(c => c.Name == resource.AssemblyTechnologyName)
                                                    : null,
                           OptionSet = resource.OptionSet,
                           DrawingReference = resource.DrawingReference,
                           BomType = resource.BomType,
                           BomId = resource.BomId,
                           PlannedSurplus = this.ToYesOrNoString(resource.PlannedSurplus),
                           SernosSequence = resource.SernosSequenceName != null
                                                    ? this.sernosSequenceRepository.FindBy(c => c.Sequence == resource.SernosSequenceName)
                                                    : null,
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
            entity.PlannedSurplus = this.ToYesOrNoString(resource.PlannedSurplus);

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
                a => a.Nominal.NominalCode == resource.Nominal && a.Department.DepartmentCode == resource.Department);
            entity.DecrementRule = resource.DecrementRuleName != null
                                       ? this.decrementRuleRepository.FindBy(c => c.Rule == resource.DecrementRuleName)
                                       : null;
            entity.AssemblyTechnology = resource.AssemblyTechnologyName != null
                                            ? this.assemblyTechnologyRepository.FindBy(
                                                c => c.Name == resource.AssemblyTechnologyName)
                                            : null;
            entity.OptionSet = resource.OptionSet;
            entity.DrawingReference = resource.DrawingReference;
            entity.BomType = resource.BomType;
            entity.BomId = resource.BomId;
            entity.SernosSequence = resource.SernosSequenceName != null
                ? this.sernosSequenceRepository.FindBy(c => c.Sequence == resource.SernosSequenceName)
                : null;
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