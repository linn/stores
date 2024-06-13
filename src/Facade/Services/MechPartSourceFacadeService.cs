namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;

    public class MechPartSourceFacadeService : FacadeFilterService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource, MechPartSourceSearchResource>
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IQueryRepository<Department> departmentRepository;

        private readonly IPartRepository partRepository;

        private readonly IQueryRepository<Supplier> supplierRepository;

        private readonly IRepository<Manufacturer, string> manufacturerRepository;

        private readonly IMechPartSourceService domainService;

        private readonly IDatabaseService databaseService;

        public MechPartSourceFacadeService(
            IRepository<MechPartSource, int> repository, 
            ITransactionManager transactionManager,
            IMechPartSourceService domainService,
            IPartRepository partRepository,
            IDatabaseService databaseService,
            IQueryRepository<Supplier> supplierRepository,
            IQueryRepository<RootProduct> rootProductRepository,
            IRepository<Manufacturer, string> manufacturerRepository,
            IRepository<Employee, int> employeeRepository,
            IQueryRepository<Department> departmentRepository) : base(repository, transactionManager)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
            this.domainService = domainService;
            this.partRepository = partRepository;
            this.databaseService = databaseService;
            this.supplierRepository = supplierRepository;
            this.manufacturerRepository = manufacturerRepository;
        }

        protected override MechPartSource CreateFromResource(MechPartSourceResource resource)
        {
            var candidate = this.BuildEntityFromResource(resource);

            candidate.Id = this.databaseService.GetIdSequence("MECH_SOURCE_SEQ");
            candidate.PartNumber = resource.PartNumber;
            candidate.PartDescription = resource.Description;
            candidate.DateEntered = resource.DateEntered == null ? (DateTime?)null : DateTime.Parse(resource.DateEntered);
            candidate.PartCreatedBy = resource.PartCreatedBy != null
                                        ? this.employeeRepository.FindById((int)resource.PartCreatedBy) : null;
            candidate.PartCreatedDate = resource.PartCreatedDate != null
                                            ? DateTime.Parse(resource.PartCreatedDate)
                                            : (DateTime?)null;
            candidate.Project = string.IsNullOrWhiteSpace(resource.ProjectCode)
                                    ? null
                                    : this.departmentRepository.FindBy(a => a.DepartmentCode == resource.ProjectCode);
            return this.domainService.Create(candidate, resource.UserPrivileges);
        }

        protected override void UpdateFromResource(MechPartSource entity, MechPartSourceResource resource)
        {
            var candidate = this.BuildEntityFromResource(resource);
            candidate.Part = new Part();

            candidate.Part.DataSheets = resource.Part?.DataSheets.Select(s => new PartDataSheet
                                                                        {
                                                                            Part = candidate.Part,
                                                                            Sequence = s.Sequence ?? -1,
                                                                            PdfFilePath = s.PdfFilePath
                                                                        });
            
            this.domainService.Update(candidate, entity, resource.UserPrivileges);
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            return source => source.PartNumber == searchTerm.ToUpper();
        }

        protected override Expression<Func<MechPartSource, bool>> FilterExpression(MechPartSourceSearchResource searchResource)
        {
            return x => (string.IsNullOrEmpty(searchResource.PartNumber) || x.PartNumber.Contains(searchResource.PartNumber.Trim().ToUpper()))
                && (string.IsNullOrEmpty(searchResource.Description) || x.PartDescription.Contains(searchResource.Description.Trim().ToUpper()))
                && (string.IsNullOrEmpty(searchResource.ProjectDeptCode) || x.ProjectCode.Equals(searchResource.ProjectDeptCode.Trim().ToUpper()))
                && (!searchResource.CreatedBy.HasValue || x.PartCreatedById.Equals(searchResource.CreatedBy.Value));
        }

        private MechPartSource BuildEntityFromResource(MechPartSourceResource resource)
        {
            var entity = new MechPartSource
                             {
                                 AssemblyType = resource.AssemblyType,
                                 DateSamplesRequired = resource.DateSamplesRequired == null
                                                           ? (DateTime?)null
                                                           : DateTime.Parse(resource.DateSamplesRequired),
                                 EmcCritical = resource.EmcCritical,
                                 EstimatedVolume = resource.EstimatedVolume,
                                 LinnPartNumber = resource.LinnPartNumber,
                                 MechanicalOrElectrical = resource.MechanicalOrElectrical,
                                 Notes = resource.Notes,
                                 ProposedBy = resource.ProposedBy == null
                                                  ? null
                                                  : this.employeeRepository.FindById((int)resource.ProposedBy),
                                 PerformanceCritical = resource.PerformanceCritical,
                                 SafetyCritical = resource.SafetyCritical,
                                 SingleSource = resource.SingleSource,
                                 PartType = resource.PartType,
                                 RohsReplace = resource.RohsReplace,
                                 SampleQuantity = resource.SampleQuantity,
                                 SamplesRequired = resource.SamplesRequired,
                                 PartToBeReplaced = resource.LinnPartNumber == null
                                                        ? null
                                                        : this.partRepository.FindBy(p => p.PartNumber == resource.LinnPartNumber),
                                 ProductionDate = resource.ProductionDate == null
                                                      ? (DateTime?)null
                                                      : DateTime.Parse(resource.ProductionDate),
                                 SafetyDataDirectory = resource.SafetyDataDirectory,
                                 MechPartManufacturerAlts = resource.MechPartManufacturerAlts?.Select(
                                     a => new MechPartManufacturerAlt
                                              {
                                                  Sequence = a.Sequence.HasValue ? (int)a.Sequence : -1,
                                                  PartNumber = a.PartNumber,
                                                  DateApproved = a.DateApproved != null
                                                                     ? DateTime.Parse(a.DateApproved) : (DateTime?)null,
                                                  ApprovedBy = a.ApprovedBy != null ? this.employeeRepository.FindById((int)a.ApprovedBy) : null,
                                                  ManufacturerCode = a.ManufacturerCode,
                                                  Preference = a.Preference,
                                                  ReelSuffix = a.ReelSuffix,
                                                  RohsCompliant = a.RohsCompliant
                                              }).ToList(),
                                 MechPartAlts = resource.MechPartAlts?.Select(a => new MechPartAlt
                                                                                       {
                                                                                           PartNumber = a.PartNumber,
                                                                                           Sequence = a.Sequence,
                                                                                           Supplier = a.SupplierId == null ? null :
                                                                                                          new Supplier
                                                                                                              {
                                                                                                                  Id = (int)a.SupplierId,
                                                                                                                  Name = a.SupplierName
                                                                                                              }
                                                                                       }).ToList(),
                                 ApprovedReferenceStandards = resource.ApprovedReferenceStandards,
                                 ApprovedReferencesAvailable = resource.ApprovedReferencesAvailable,
                                 ApprovedReferencesDate = resource.ApprovedReferencesDate != null
                                                              ? DateTime.Parse(resource.ApprovedReferencesDate) : (DateTime?)null,
                                 ChecklistAvailable = resource.ChecklistAvailable,
                                 ChecklistCreated = resource.ChecklistCreated,
                                 ChecklistDate = resource.ChecklistDate != null
                                                     ? DateTime.Parse(resource.ChecklistDate) : (DateTime?)null,
                                 DrawingFile = resource.DrawingFile,
                                 DrawingsPackage = resource.DrawingsPackage,
                                 DrawingsPackageAvailable = resource.DrawingsPackageAvailable,
                                 DrawingsPackageDate = resource.DrawingsPackageDate != null
                                                           ? DateTime.Parse(resource.DrawingsPackageDate) : (DateTime?)null,
                                 PackingAvailable = resource.PackingAvailable,
                                 PackingDate = resource.PackingDate != null
                                                   ? DateTime.Parse(resource.PackingDate) : (DateTime?)null,
                                 PackingRequired = resource.PackingRequired,
                                 ProcessEvaluation = resource.ProcessEvaluation,
                                 ProcessEvaluationAvailable = resource.ProcessEvaluationAvailable,
                                 ProcessEvaluationDate = resource.ProcessEvaluationDate != null
                                                             ? DateTime.Parse(resource.ProcessEvaluationDate) : (DateTime?)null,
                                 ProductKnowledge = resource.ProductKnowledge,
                                 ProductKnowledgeAvailable = resource.ProductKnowledgeAvailable,
                                 ProductKnowledgeDate = resource.ProductKnowledgeDate != null
                                                            ? DateTime.Parse(resource.ProductKnowledgeDate) : (DateTime?)null,
                                 TestEquipment = resource.TestEquipment,
                                 TestEquipmentAvailable = resource.TestEquipmentAvailable,
                                 TestEquipmentDate = resource.TestEquipmentDate != null
                                                         ? DateTime.Parse(resource.TestEquipmentDate) : (DateTime?)null,
                                 CapacitorRippleCurrent = resource.CapacitorRippleCurrent,
                                 Capacitance = resource.Capacitance,
                                 CapacitorVoltageRating = resource.CapacitorVoltageRating,
                                 CapacitorPositiveTolerance = resource.CapacitorPositiveTolerance,
                                 CapacitorNegativeTolerance = resource.CapacitorNegativeTolerance,
                                 CapacitorDielectric = resource.CapacitorDielectric,
                                 Package = resource.PackageName,
                                 CapacitorPitch = resource.CapacitorPitch,
                                 CapacitorLength = resource.CapacitorLength,
                                 CapacitorWidth = resource.CapacitorWidth,
                                 CapacitorHeight = resource.CapacitorHeight,
                                 CapacitorDiameter = resource.CapacitorDiameter,
                                 CapacitanceLetterAndNumeralCode = resource.Capacitance == null
                                                                       ? null : this.domainService.GetCapacitanceLetterAndNumeralCode(
                                                                           resource.CapacitanceUnit,
                                                                           (decimal)resource.Capacitance),
                                 Resistance = resource.Resistance,
                                 ResistorTolerance = resource.ResistorTolerance,
                                 Construction = resource.Construction,
                                 ResistorLength = resource.ResistorLength,
                                 ResistorHeight = resource.ResistorHeight,
                                 ResistorWidth = resource.ResistorWidth,
                                 ResistorPowerRating = resource.ResistorPowerRating,
                                 ResistorVoltageRating = resource.ResistorVoltageRating,
                                 TemperatureCoefficient = resource.TemperatureCoefficient,
                                 TransistorType = resource.TransistorType,
                                 TransistorDeviceName = resource.TransistorDeviceName,
                                 TransistorCurrent = resource.TransistorCurrent,
                                 TransistorVoltage = resource.TransistorVoltage,
                                 TransistorPolarity = resource.TransistorPolarity,
                                 IcType = resource.IcType,
                                 IcFunction = resource.IcFunction,
                                 LibraryName = resource.LibraryName,
                                 LibraryRef = resource.LibraryRef,
                                 FootprintRef = resource.FootprintRef,
                                 FootprintRef2 = resource.FootprintRef2,
                                 FootprintRef3 = resource.FootprintRef3,
                                 ProjectCode = resource.ProjectCode,
                                 RkmCode = resource.Resistance == null ? null :
                                 this.domainService.GetRkmCode(resource.ResistanceUnits, (decimal)resource.Resistance),

                                 ApplyTCodeBy = resource.ApplyTCodeBy != null ?
                                                    this.employeeRepository.FindById((int)resource.ApplyTCodeBy) : null,
                                 ApplyTCodeDate = resource.ApplyTCodeDate != null ?
                                                      DateTime.Parse(resource.ApplyTCodeDate) : (DateTime?)null,

                                 CancelledBy = resource.CancelledBy != null ?
                                                   this.employeeRepository.FindById((int)resource.CancelledBy) : null,
                                 DateCancelled = resource.CancelledDate != null ?
                                                     DateTime.Parse(resource.CancelledDate) : (DateTime?)null,

                                 McitVerifiedBy = resource.McitVerifiedBy != null ?
                                                      this.employeeRepository.FindById((int)resource.McitVerifiedBy) : null,
                                 McitVerifiedDate = resource.McitVerifiedDate != null ?
                                                        DateTime.Parse(resource.McitVerifiedDate) : (DateTime?)null,

                                 QualityVerifiedBy = resource.QualityVerifiedBy != null ?
                                                         this.employeeRepository.FindById((int)resource.QualityVerifiedBy) : null,
                                 QualityVerifiedDate = resource.QualityVerifiedDate != null ?
                                                           DateTime.Parse(resource.QualityVerifiedDate) : (DateTime?)null,

                                 RemoveTCodeBy = resource.RemoveTCodeBy != null ?
                                                     this.employeeRepository.FindById((int)resource.RemoveTCodeBy) : null,
                                 RemoveTCodeDate = resource.RemoveTCodeDate != null ?
                                                       DateTime.Parse(resource.RemoveTCodeDate) : (DateTime?)null,

                                 VerifiedBy = resource.VerifiedBy != null ?
                                                  this.employeeRepository.FindById((int)resource.VerifiedBy) : null,
                                 VerifiedDate = resource.VerifiedDate != null ?
                                                    DateTime.Parse(resource.VerifiedDate) : (DateTime?)null,

                                 PurchasingQuotes = resource.PurchasingQuotes?.Select(q => new MechPartPurchasingQuote
                                                                                               {
                                                                                                   LeadTime = q.LeadTime,
                                                                                                   Manufacturer = q.ManufacturerCode != null ?
                                                                                                                      this.manufacturerRepository.FindById(q.ManufacturerCode) : null,
                                                                                                   ManufacturersPartNumber = q.ManufacturersPartNumber,
                                                                                                   Moq = q.Moq,
                                                                                                   RohsCompliant = q.RohsCompliant,
                                                                                                   UnitPrice = q.UnitPrice,
                                                                                                   Supplier = this.supplierRepository.FindBy(s => s.Id == q.SupplierId)
                                                                                               }).ToList(),

                                 Usages = resource.Usages?.Select(u => new MechPartUsage
                                                                           {
                                                                               QuantityUsed = u.QuantityUsed,
                                                                               Product = u.RootProductName
                                                                           }).ToList(),
                                 LifeExpectancyPart = resource.LifeExpectancyPart,
                                 Configuration = resource.Configuration
                             };
            return entity;
        }
    }
}
