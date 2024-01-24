namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartFacadeService : FacadeService<Part, int, PartResource, PartResource>, IPartsFacadeService
    {
        private readonly IPartRepository partRepository;

        private readonly IRepository<ParetoClass, string> paretoClassRepository;

        private readonly IRepository<AssemblyTechnology, string> assemblyTechnologyRepository;

        private readonly IRepository<DecrementRule, string> decrementRuleRepository;

        private readonly IQueryRepository<ProductAnalysisCode> productAnalysisCodeRepository;

        private readonly IQueryRepository<AccountingCompany> accountingCompanyRepository;

        private readonly IQueryRepository<NominalAccount> nominalAccountRepository;

        private readonly IQueryRepository<SernosSequence> sernosSequenceRepository;

        private readonly IQueryRepository<Supplier> supplierRepository;

        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IDatabaseService databaseService;

        private readonly IPartService partService;

        private readonly ITransactionManager transactionManager;

        public PartFacadeService(
            IPartRepository partRepository,
            IRepository<ParetoClass, string> paretoClassRepository,
            IQueryRepository<ProductAnalysisCode> productAnalysisCodeRepository,
            IQueryRepository<AccountingCompany> accountingCompanyRepository,
            IQueryRepository<NominalAccount> nominalAccountRepository,
            IRepository<AssemblyTechnology, string> assemblyTechnologyRepository,
            IRepository<DecrementRule, string> decrementRuleRepository,
            IQueryRepository<SernosSequence> sernosSequenceRepository,
            IQueryRepository<Supplier> supplierRepository,
            IRepository<Employee, int> employeeRepository,
            IPartService partService,
            IDatabaseService databaseService,
            ITransactionManager transactionManager)
            : base(partRepository, transactionManager)
        {
            this.partRepository = partRepository;
            
            this.paretoClassRepository = paretoClassRepository;
            this.productAnalysisCodeRepository = productAnalysisCodeRepository;
            this.accountingCompanyRepository = accountingCompanyRepository;
            this.nominalAccountRepository = nominalAccountRepository;
            this.decrementRuleRepository = decrementRuleRepository;
            this.assemblyTechnologyRepository = assemblyTechnologyRepository;
            this.sernosSequenceRepository = sernosSequenceRepository;
            this.supplierRepository = supplierRepository;
            this.employeeRepository = employeeRepository;
            this.partService = partService;
            this.databaseService = databaseService;
            this.transactionManager = transactionManager;
        }

        public IResult<IEnumerable<Part>> GetDeptStockPalletParts()
        {
            return new SuccessResult<IEnumerable<Part>>(this.partService.GetDeptStockPalletParts());
        }

        public void CreatePartFromSource(int sourceId, int proposedById, IEnumerable<PartDataSheetResource> dataSheets)
        {
            this.partService.CreateFromSource(
                sourceId, 
                proposedById, 
                dataSheets.Select(s => 
                new PartDataSheet
                    {
                        PdfFilePath = s.PdfFilePath,
                        Sequence = s.Sequence ?? -1
                    }));
            this.transactionManager.Commit();
        }

        public IResult<IEnumerable<Part>> GetPartByPartNumber(string partNumber)
        {
            var part = this.partRepository.FindBy(a => a.PartNumber == partNumber.ToUpper());
            return part != null
                ? new SuccessResult<IEnumerable<Part>>(new List<Part> { part })
                : new SuccessResult<IEnumerable<Part>>(Enumerable.Empty<Part>());
        }

        public IResult<Part> GetByIdNoTracking(int id)
        {
            var res = this.partRepository.FilterBy(x => x.Id == id).FirstOrDefault();

            if (res == null)
            {
                return new NotFoundResult<Part>("Not Found");
            }

            return new SuccessResult<Part>(res);
        }

        public IResult<Part> GetByIdWithManufacturerData(int id)
        {
            var data = this.partRepository.GetByIdWithManufacturerData(id);
            return new SuccessResult<Part>(data);
        }

        public IResult<IEnumerable<Part>> SearchParts(string searchTerm, int? resultLimit)
        {
            return new SuccessResult<IEnumerable<Part>>(
                this.partRepository.SearchParts(searchTerm.Trim().ToUpper(), resultLimit));
        }

        public IResult<IEnumerable<Part>> SearchPartsWithWildcard(
            string partNumberSearch, 
            string descriptionSearch, 
            string productAnalysisCodeSearch)
        {
            return new SuccessResult<IEnumerable<Part>>(
                this.partRepository.SearchPartsWithWildcard(
                    partNumberSearch?.Trim().ToUpper(), 
                    descriptionSearch?.Trim().ToUpper(),
                    productAnalysisCodeSearch?.Trim().ToUpper()));
        }

        protected override Part CreateFromResource(PartResource resource)
        {
            var partToAdd = new Part
                                {
                                    Id = this.databaseService.GetIdSequence("PARTS_ID_SEQ"),
                                    PartNumber = resource.PartNumber,
                                    Description = resource.Description,
                                    PsuPart = resource.PsuPart,
                                    CreatedBy = resource.CreatedBy != null ?
                                                    this.employeeRepository.FindById((int)resource.CreatedBy) : null,
                                    DateCreated = DateTime.Parse(resource.DateCreated),
                                    StockControlled = resource.StockControlled,
                                    CccCriticalPart = resource.CccCriticalPart,
                                    AccountingCompany =
                                        this.accountingCompanyRepository.FindBy(c => c.Name == resource.AccountingCompany),
                                    ParetoClass = this.paretoClassRepository.FindById(resource.ParetoCode),
                                    BomType = resource.BomType,
                                    BomVerifyFreqWeeks = resource.BomVerifyFreqWeeks,
                                    LinnProduced = resource.LinnProduced,
                                    QcOnReceipt = resource.QcOnReceipt,
                                    EmcCriticalPart = resource.EmcCriticalPart,
                                    SafetyCriticalPart = resource.SafetyCriticalPart,
                                    PlannedSurplus = resource.PlannedSurplus,
                                    OurUnitOfMeasure = resource.OurUnitOfMeasure,
                                    SingleSourcePart = resource.SingleSourcePart,
                                    PerformanceCriticalPart = resource.PerformanceCriticalPart,
                                    ProductAnalysisCode =
                                        resource.ProductAnalysisCode != null
                                            ? this.productAnalysisCodeRepository.FindBy(
                                                c => c.ProductCode == resource.ProductAnalysisCode)
                                            : null,
                                    RootProduct = resource.RootProduct,
                                    SafetyCertificateExpirationDate =
                                        string.IsNullOrEmpty(resource.SafetyCertificateExpirationDate)
                                            ? (DateTime?)null
                                            : DateTime.Parse(resource.SafetyCertificateExpirationDate),
                                    SafetyDataDirectory = resource.SafetyDataDirectory,
                                    NominalAccount =
                                        this.nominalAccountRepository.FindBy(
                                            a => a.Nominal.NominalCode == resource.Nominal
                                                 && a.Department.DepartmentCode == resource.Department),
                                    DecrementRule =
                                        resource.DecrementRuleName != null
                                            ? this.decrementRuleRepository.FindBy(
                                                c => c.Rule == resource.DecrementRuleName)
                                            : null,
                                    AssemblyTechnology =
                                        resource.AssemblyTechnologyName != null
                                            ? this.assemblyTechnologyRepository.FindBy(
                                                c => c.Name == resource.AssemblyTechnologyName)
                                            : null,
                                    OptionSet = resource.OptionSet,
                                    DrawingReference = resource.DrawingReference,
                                    BomId = resource.BomId,
                                    SernosSequence =
                                        resource.SernosSequenceName != null
                                            ? this.sernosSequenceRepository.FindBy(
                                                c => c.Sequence == resource.SernosSequenceName)
                                            : null,
                                    IgnoreWorkstationStock = resource.IgnoreWorkstationStock,
                                    ImdsIdNumber = resource.ImdsIdNumber,
                                    ImdsWeight = resource.ImdsWeight,
                                    OrderHold = resource.OrderHold,
                                    MaterialPrice = resource.MaterialPrice,
                                    SparesRequirement = resource.SparesRequirement,
                                    CurrencyUnitPrice = resource.CurrencyUnitPrice,
                                    NonForecastRequirement = resource.NonForecastRequirement,
                                    BaseUnitPrice = resource.BaseUnitPrice,
                                    OneOffRequirement = resource.OneOffRequirement,
                                    LabourPrice = resource.LabourPrice,
                                    PreferredSupplier =
                                        this.supplierRepository.FindBy(s => s.Id == resource.PreferredSupplier),
                                    QcInformation = resource.QcInformation,
                                    RawOrFinished = resource.RawOrFinished,
                                    OurInspectionWeeks = resource.OurInspectionWeeks,
                                    SafetyWeeks = resource.SafetyWeeks,
                                    RailMethod = resource.RailMethod,
                                    MinStockRail = resource.MinStockRail,
                                    MaxStockRail = resource.MaxStockRail,
                                    PlannerStory = resource.PlannerStory,
                                    SecondStageBoard = resource.SecondStageBoard,
                                    SecondStageDescription = resource.SecondStageDescription,
                                    TqmsCategoryOverride = resource.TqmsCategoryOverride,
                                    StockNotes = resource.StockNotes,
                                    ScrapOrConvert = resource.ScrapOrConvert,
                                    PurchasingPhaseOutType = resource.PurchasingPhaseOutType,
                                    DateDesignObsolete =
                                        string.IsNullOrEmpty(resource.DateDesignObsolete)
                                            ? (DateTime?)null
                                            : DateTime.Parse(resource.DateDesignObsolete),
                                    PhasedOutBy =
                                        resource.PhasedOutBy != null
                                            ? this.employeeRepository.FindById((int)resource.PhasedOutBy)
                                            : null,
                                    DatePhasedOut = string.IsNullOrEmpty(resource.DatePhasedOut)
                                                        ? (DateTime?)null
                                                        : DateTime.Parse(resource.DatePhasedOut),
                                    ReasonPhasedOut = resource.ReasonPhasedOut,
                                    LibraryName = resource.LibraryName,
                                    LibraryRef = resource.LibraryRef,
                                    FootprintRef1 = resource.FootprintRef1,
                                    FootprintRef2 = resource.FootprintRef2,
                                    FootprintRef3 = resource.FootprintRef3,
                                    IcType = resource.IcType,
                                    DataSheetPdfPath = resource.DatasheetPath,
                                    ManufacturersPartNumber = resource.TheirPartNumber
                                };
            return this.partService.CreatePart(partToAdd, resource.UserPrivileges.ToList(), resource.FromTemplate);
        }

        protected override void UpdateFromResource(Part entity, PartResource resource)
        {
            var updatedPart = new Part
                                  {
                                      PartNumber = resource.PartNumber,
                                      Description = resource.Description,
                                      AccountingCompany = 
                                          this.accountingCompanyRepository.FindBy(
                                              c => c.Name == resource.AccountingCompany),
                                      CccCriticalPart = resource.CccCriticalPart,
                                      EmcCriticalPart = resource.EmcCriticalPart,
                                      SafetyCriticalPart = resource.SafetyCriticalPart,
                                      PlannedSurplus = resource.PlannedSurplus,
                                      OurUnitOfMeasure = resource.OurUnitOfMeasure,
                                      SingleSourcePart = resource.SingleSourcePart,
                                      StockControlled = resource.StockControlled,
                                      ParetoClass =
                                          resource.ParetoCode != null
                                              ? this.paretoClassRepository.FindById(resource.ParetoCode)
                                              : null,
                                      PerformanceCriticalPart = resource.PerformanceCriticalPart,
                                      ProductAnalysisCode =
                                          resource.ProductAnalysisCode != null
                                              ? this.productAnalysisCodeRepository.FindBy(
                                                  c => c.ProductCode == resource.ProductAnalysisCode)
                                              : null,
                                      PsuPart = resource.PsuPart,
                                      RootProduct = resource.RootProduct,
                                      SafetyCertificateExpirationDate =
                                          string.IsNullOrEmpty(resource.SafetyCertificateExpirationDate)
                                              ? (DateTime?)null
                                              : DateTime.Parse(resource.SafetyCertificateExpirationDate),
                                      SafetyDataDirectory = resource.SafetyDataDirectory,
                                      NominalAccount =
                                          this.nominalAccountRepository.FindBy(
                                              a => a.Nominal.NominalCode == resource.Nominal
                                                   && a.Department.DepartmentCode == resource.Department),
                                      DecrementRule =
                                          resource.DecrementRuleName != null
                                              ? this.decrementRuleRepository.FindBy(
                                                  c => c.Rule == resource.DecrementRuleName)
                                              : null,
                                      AssemblyTechnology =
                                          resource.AssemblyTechnologyName != null
                                              ? this.assemblyTechnologyRepository.FindBy(
                                                  c => c.Name == resource.AssemblyTechnologyName)
                                              : null,
                                      OptionSet = resource.OptionSet,
                                      DrawingReference = resource.DrawingReference,
                                      BomType = resource.BomType,
                                      BomVerifyFreqWeeks = resource.BomVerifyFreqWeeks,
                                      BomId = resource.BomId,
                                      SernosSequence =
                                          resource.SernosSequenceName != null
                                              ? this.sernosSequenceRepository.FindBy(
                                                  c => c.Sequence == resource.SernosSequenceName)
                                              : null,
                                      IgnoreWorkstationStock = resource.IgnoreWorkstationStock,
                                      ImdsIdNumber = resource.ImdsIdNumber,
                                      ImdsWeight = resource.ImdsWeight,
                                      OrderHold = resource.OrderHold,
                                      MaterialPrice = resource.MaterialPrice,
                                      SparesRequirement = resource.SparesRequirement,
                                      CurrencyUnitPrice = resource.CurrencyUnitPrice,
                                      NonForecastRequirement = resource.NonForecastRequirement,
                                      BaseUnitPrice = resource.BaseUnitPrice,
                                      OneOffRequirement = resource.OneOffRequirement,
                                      LabourPrice = resource.LabourPrice,
                                      LinnProduced = resource.LinnProduced,
                                      PreferredSupplier =
                                          this.supplierRepository.FindBy(s => s.Id == resource.PreferredSupplier),
                                      QcOnReceipt = resource.QcOnReceipt,
                                      QcInformation = resource.QcInformation,
                                      RawOrFinished = resource.RawOrFinished,
                                      PlannerStory = resource.PlannerStory,
                                      OurInspectionWeeks = resource.OurInspectionWeeks,
                                      SafetyWeeks = resource.SafetyWeeks,
                                      RailMethod = resource.RailMethod,
                                      MinStockRail = resource.MinStockRail,
                                      MaxStockRail = resource.MaxStockRail,
                                      SecondStageBoard = resource.SecondStageBoard,
                                      SecondStageDescription = resource.SecondStageDescription,
                                      TqmsCategoryOverride = resource.TqmsCategoryOverride,
                                      StockNotes = resource.StockNotes,
                                      ScrapOrConvert = resource.ScrapOrConvert,
                                      PurchasingPhaseOutType = resource.PurchasingPhaseOutType,
                                      DateDesignObsolete =
                                          string.IsNullOrEmpty(resource.DateDesignObsolete)
                                              ? (DateTime?)null
                                              : DateTime.Parse(resource.DateDesignObsolete),
                                      PhasedOutBy =
                                          resource.PhasedOutBy != null
                                              ? this.employeeRepository.FindById((int)resource.PhasedOutBy)
                                              : null,
                                      DatePhasedOut = string.IsNullOrEmpty(resource.DatePhasedOut)
                                                          ? (DateTime?)null
                                                          : DateTime.Parse(resource.DatePhasedOut),
                                      ReasonPhasedOut = resource.ReasonPhasedOut,
                                      DateLive = string.IsNullOrEmpty(resource.DateLive)
                                                     ? (DateTime?)null
                                                     : DateTime.Parse(resource.DateLive),
                                      MadeLiveBy = 
                                          resource.MadeLiveBy != null
                                              ? this.employeeRepository.FindById((int)resource.MadeLiveBy)
                                              : null,
                                      LibraryName = resource.LibraryName,
                                      LibraryRef = resource.LibraryRef,
                                      FootprintRef1 = resource.FootprintRef1,
                                      FootprintRef2 = resource.FootprintRef2,
                                      FootprintRef3 = resource.FootprintRef3,
                                      IcType = resource.IcType,
                                      DataSheetPdfPath = resource.DatasheetPath,
                                      ManufacturersPartNumber = resource.TheirPartNumber
            };
          
            this.partService.UpdatePart(entity, updatedPart, resource.UserPrivileges.ToList(), resource.UpdatedBy.Value);
        }

        protected override Expression<Func<Part, bool>> SearchExpression(string searchTerm)
        {
            return part => part.PartNumber.ToUpper().Contains(searchTerm.ToUpper())
                           || part.Description.ToUpper().Contains(searchTerm.ToUpper())
                           || part.MechPartSource.MechPartManufacturerAlts
                               .Any(m => m.PartNumber == searchTerm.ToUpper());
        }
    }
}
