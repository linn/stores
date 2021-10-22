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

    public class MechPartSourceFacadeService : FacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IPartRepository partRepository;

        private readonly IQueryRepository<RootProduct> rootProductRepository;

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
            IRepository<Employee, int> employeeRepository) : base(repository, transactionManager)
        {
            this.employeeRepository = employeeRepository;
            this.domainService = domainService;
            this.partRepository = partRepository;
            this.databaseService = databaseService;
            this.supplierRepository = supplierRepository;
            this.rootProductRepository = rootProductRepository;
            this.manufacturerRepository = manufacturerRepository;
        }

        protected override MechPartSource CreateFromResource(MechPartSourceResource resource)
        {
            var part = resource.PartNumber == null
                ? null
                : this.partRepository.FindBy(p => p.PartNumber == resource.PartNumber);

            var dataSheets = resource.Part?.DataSheets?.Select(
                s => new PartDataSheet { PartNumber = null, PdfFilePath = s.PdfFilePath, Sequence = s.Sequence });

            var candidate = new MechPartSource
            {
                Id = this.databaseService.GetIdSequence("MECH_SOURCE_SEQ"),
                PartNumber = resource.PartNumber,
                PartDescription = resource.Description,
                AssemblyType = resource.AssemblyType,
                DateEntered = resource.DateEntered == null ? (DateTime?)null : DateTime.Parse(resource.DateEntered),
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
                Part = part,
                MechPartManufacturerAlts = resource.MechPartManufacturerAlts?.Select(
                    a => new MechPartManufacturerAlt
                                     {
                                         Sequence = a.Sequence,
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
                LibraryRef = resource.LibraryRef,
                FootprintRef = resource.FootprintRef,
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

                PartCreatedBy = resource.PartCreatedBy != null ?
                                     this.employeeRepository.FindById((int)resource.PartCreatedBy) : null,
                PartCreatedDate = resource.PartCreatedDate != null ?
                                       DateTime.Parse(resource.PartCreatedDate) : (DateTime?)null,

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
                                                              RootProductName = u.RootProductName,
                                                              RootProduct = u.RootProductName == null ? this.rootProductRepository
                                                                                    .FindBy(p => p.Name == u.RootProductName)
                                                                                : null
                                                          }).ToList(),
                LifeExpectancyPart = resource.LifeExpectancyPart,
                Configuration = resource.Configuration
            };

            return this.domainService.Create(candidate, dataSheets);
        }

        protected override void UpdateFromResource(MechPartSource entity, MechPartSourceResource resource)
        {
            entity.AssemblyType = resource.AssemblyType;
            entity.DateSamplesRequired = resource.DateSamplesRequired == null
                ? (DateTime?)null
                : DateTime.Parse(resource.DateSamplesRequired);
            entity.EmcCritical = resource.EmcCritical;
            entity.EstimatedVolume = resource.EstimatedVolume;
            entity.LinnPartNumber = resource.LinnPartNumber;
            entity.MechanicalOrElectrical = resource.MechanicalOrElectrical;
            entity.Notes = resource.Notes;
            entity.ProposedBy = resource.ProposedBy == null
                ? null
                : this.employeeRepository.FindById((int)resource.ProposedBy);
            entity.PerformanceCritical = resource.PerformanceCritical;
            entity.SafetyCritical = resource.SafetyCritical;
            entity.SingleSource = resource.SingleSource;
            entity.PartType = resource.PartType;
            entity.RohsReplace = resource.RohsReplace;
            entity.SampleQuantity = resource.SampleQuantity;
            entity.SamplesRequired = resource.SamplesRequired;
            entity.ProductionDate = resource.ProductionDate == null
                ? (DateTime?)null
                : DateTime.Parse(resource.ProductionDate);
            entity.SafetyDataDirectory = resource.SafetyDataDirectory;
            entity.PartToBeReplaced = resource.LinnPartNumber == null
                ? null
                : this.partRepository.FindBy(p => p.PartNumber == resource.LinnPartNumber);

            var currentDataSheets = entity.Part?.DataSheets;

            var newDataSheets = resource.Part?.DataSheets.Select(s => new PartDataSheet
                                                                        {
                                                                            Part = entity.Part,
                                                                            Sequence = s.Sequence,
                                                                            PdfFilePath = s.PdfFilePath
                                                                        });
            if (entity.Part != null)
            {
                entity.Part.DataSheets = 
                    this.domainService.GetUpdatedDataSheets(currentDataSheets, newDataSheets);
            }

            entity.MechPartManufacturerAlts = resource.MechPartManufacturerAlts?.Select(
                a => new MechPartManufacturerAlt
                         {
                             Sequence = a.Sequence,
                             PartNumber = a.PartNumber,
                             DateApproved = a.DateApproved != null ? DateTime.Parse(a.DateApproved) : (DateTime?)null,
                             ApprovedBy =
                                 a.ApprovedBy != null ? this.employeeRepository.FindById((int)a.ApprovedBy) : null,
                             ManufacturerCode = a.ManufacturerCode,
                             Preference = a.Preference,
                             ReelSuffix = a.ReelSuffix,
                             RohsCompliant = a.RohsCompliant
                         }).ToList();
            entity.MechPartAlts = resource.MechPartAlts?.Select(
                a => new MechPartAlt
                         {
                             PartNumber = a.PartNumber,
                             Sequence = a.Sequence,
                             Supplier = a.SupplierId == null ? null :
                                            this.supplierRepository.FindBy(s => s.Id == (int)a.SupplierId)
                         }).ToList();
            entity.ApprovedReferenceStandards = resource.ApprovedReferenceStandards;
            entity.ApprovedReferencesAvailable = resource.ApprovedReferencesAvailable;
            entity.ApprovedReferencesDate = resource.ApprovedReferencesDate != null
                                                ? DateTime.Parse(resource.ApprovedReferencesDate)
                                                : (DateTime?)null;
            entity.ChecklistAvailable = resource.ChecklistAvailable;
            entity.ChecklistCreated = resource.ChecklistCreated;
            entity.ChecklistDate = resource.ChecklistDate != null
                                       ? DateTime.Parse(resource.ChecklistDate)
                                       : (DateTime?)null;
            entity.DrawingFile = resource.DrawingFile;
            entity.DrawingsPackage = resource.DrawingsPackage;
            entity.DrawingsPackageAvailable = resource.DrawingsPackageAvailable;
            entity.DrawingsPackageDate = resource.DrawingsPackageDate != null
                                             ? DateTime.Parse(resource.DrawingsPackageDate) : (DateTime?)null;
            entity.PackingAvailable = resource.PackingAvailable;
            entity.PackingDate = resource.PackingDate != null
                                     ? DateTime.Parse(resource.PackingDate) : (DateTime?)null;
            entity.PackingRequired = resource.PackingRequired;
            entity.ProcessEvaluation = resource.ProcessEvaluation;
            entity.ProcessEvaluationAvailable = resource.ProcessEvaluationAvailable;
            entity.ProcessEvaluationDate = resource.ProcessEvaluationDate != null
                                               ? DateTime.Parse(resource.ProcessEvaluationDate) : (DateTime?)null;
            entity.ProductKnowledge = resource.ProductKnowledge;
            entity.ProductKnowledgeAvailable = resource.ProductKnowledgeAvailable;
            entity.ProductKnowledgeDate = resource.ProductKnowledgeDate != null
                                              ? DateTime.Parse(resource.ProductKnowledgeDate) : (DateTime?)null;
            entity.TestEquipment = resource.TestEquipment;
            entity.TestEquipmentAvailable = resource.TestEquipmentAvailable;
            entity.TestEquipmentDate = resource.TestEquipmentDate != null
                                           ? DateTime.Parse(resource.TestEquipmentDate) : (DateTime?)null;
            entity.CapacitorRippleCurrent = resource.CapacitorRippleCurrent;
            entity.Capacitance = resource.Capacitance;
            entity.CapacitorVoltageRating = resource.CapacitorVoltageRating;
            entity.CapacitorPositiveTolerance = resource.CapacitorPositiveTolerance;
            entity.CapacitorNegativeTolerance = resource.CapacitorNegativeTolerance;
            entity.CapacitorDielectric = resource.CapacitorDielectric;
            entity.Package = resource.PackageName;
            entity.CapacitorPitch = resource.CapacitorPitch;
            entity.CapacitorLength = resource.CapacitorLength;
            entity.CapacitorWidth = resource.CapacitorWidth;
            entity.CapacitorHeight = resource.CapacitorHeight;
            entity.CapacitorDiameter = resource.CapacitorDiameter;
            entity.Resistance = resource.Resistance;
            entity.ResistorWidth = resource.ResistorWidth;
            entity.ResistorTolerance = resource.ResistorTolerance;
            entity.Construction = resource.Construction;
            entity.ResistorLength = resource.ResistorLength;
            entity.ResistorHeight = resource.ResistorHeight;
            entity.ResistorPowerRating = resource.ResistorPowerRating;
            entity.ResistorVoltageRating = resource.ResistorVoltageRating;
            entity.TemperatureCoefficient = resource.TemperatureCoefficient;
            entity.TransistorType = resource.TransistorType;
            entity.TransistorDeviceName = resource.TransistorDeviceName;
            entity.TransistorCurrent = resource.TransistorCurrent;
            entity.TransistorVoltage = resource.TransistorVoltage;
            entity.TransistorPolarity = resource.TransistorPolarity;
            entity.IcType = resource.IcType;
            entity.IcFunction = resource.IcFunction;
            entity.LibraryRef = resource.LibraryRef;
            entity.FootprintRef = resource.FootprintRef;
            entity.ApplyTCodeBy = resource.ApplyTCodeBy != null
                               ? this.employeeRepository.FindById((int)resource.ApplyTCodeBy)
                               : null;
            entity.ApplyTCodeDate = resource.ApplyTCodeDate != null
                                        ? DateTime.Parse(resource.ApplyTCodeDate)
                                        : (DateTime?)null;
            entity.CancelledBy = resource.CancelledBy != null
                                     ? this.employeeRepository.FindById((int)resource.CancelledBy)
                                     : null;
            entity.DateCancelled = resource.CancelledDate != null ? DateTime.Parse(resource.CancelledDate) : (DateTime?)null;
            entity.McitVerifiedBy = resource.McitVerifiedBy != null
                                        ? this.employeeRepository.FindById((int)resource.McitVerifiedBy)
                                        : null;
            entity.McitVerifiedDate = resource.McitVerifiedDate != null
                                          ? DateTime.Parse(resource.McitVerifiedDate)
                                          : (DateTime?)null;

            entity.PartCreatedBy = resource.PartCreatedBy != null
                                       ? this.employeeRepository.FindById((int)resource.PartCreatedBy)
                                       : null;
            entity.PartCreatedDate = resource.PartCreatedDate != null
                                         ? DateTime.Parse(resource.PartCreatedDate)
                                         : (DateTime?)null;
            entity.QualityVerifiedBy = resource.QualityVerifiedBy != null
                                           ? this.employeeRepository.FindById((int)resource.QualityVerifiedBy) : null;
            entity.QualityVerifiedDate = resource.QualityVerifiedDate != null
                                             ? DateTime.Parse(resource.QualityVerifiedDate)
                                             : (DateTime?)null;
            entity.RemoveTCodeBy = resource.RemoveTCodeBy != null
                                       ? this.employeeRepository.FindById((int)resource.RemoveTCodeBy) : null;
            entity.RemoveTCodeDate = resource.RemoveTCodeDate != null
                                         ? DateTime.Parse(resource.RemoveTCodeDate)
                                         : (DateTime?)null;
            entity.VerifiedBy = resource.VerifiedBy != null
                                    ? this.employeeRepository.FindById((int)resource.VerifiedBy)
                                    : null;
            entity.VerifiedDate = resource.VerifiedDate != null ? DateTime.Parse(resource.VerifiedDate) : (DateTime?)null;
            entity.PurchasingQuotes = resource.PurchasingQuotes?.Select(
                q => new MechPartPurchasingQuote
                         {
                             LeadTime = q.LeadTime,
                             Manufacturer = q.ManufacturerCode != null ?
                                       this.manufacturerRepository.FindById(q.ManufacturerCode) : null,
                             ManufacturersPartNumber = q.ManufacturersPartNumber,
                             Moq = q.Moq,
                             RohsCompliant = q.RohsCompliant,
                             UnitPrice = q.UnitPrice,
                             Supplier = this.supplierRepository.FindBy(s => s.Id == q.SupplierId)
                         }).ToList();

            entity.Usages = resource.Usages?.Select(
                u => new MechPartUsage
                         {
                             QuantityUsed = u.QuantityUsed,
                             RootProductName = u.RootProductName,
                             RootProduct = u.RootProductName != null
                                               ? this.rootProductRepository.FindBy(p => p.Name == u.RootProductName)
                                               : null
                         }).ToList();
            entity.LifeExpectancyPart = resource.LifeExpectancyPart;
            entity.Configuration = resource.Configuration;
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            return source => source.PartNumber == searchTerm.ToUpper();
        }
    }
}
