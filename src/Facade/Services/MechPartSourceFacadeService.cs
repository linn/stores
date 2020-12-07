namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources.Parts;

    public class MechPartSourceFacadeService : FacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IRepository<Part, int> partRepository;

        private readonly IMechPartSourceService domainService;

        private readonly IDatabaseService databaseService;

        public MechPartSourceFacadeService(
            IRepository<MechPartSource, int> repository, 
            ITransactionManager transactionManager,
            IMechPartSourceService domainService,
            IRepository<Part, int> partRepository,
            IDatabaseService databaseService,
            IRepository<Employee, int> employeeRepository) : base(repository, transactionManager)
        {
            this.employeeRepository = employeeRepository;
            this.domainService = domainService;
            this.partRepository = partRepository;
            this.databaseService = databaseService;
        }

        protected override MechPartSource CreateFromResource(MechPartSourceResource resource)
        {
            var part = resource.LinnPartNumber == null
                ? null
                : this.partRepository.FindBy(p => p.PartNumber == resource.PartNumber);

            if (part != null)
            {
                part.DataSheets = resource.Part.DataSheets.Select(s => new PartDataSheet
                {
                    Part = part,
                    Sequence = s.Sequence,
                    PdfFilePath = s.PdfFilePath
                });
            }
            
            var x = new MechPartSource
            {
                Id = this.databaseService.GetIdSequence("MECH_SOURCE_SEQ"),
                PartNumber = resource.PartNumber,
                AssemblyType = resource.AssemblyType,
                DateEntered = DateTime.Parse(resource.DateEntered),
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
                CapacitanceUnit = resource.CapacitanceUnit,
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
                ResistanceChar = resource.Resistance == null ? null :
                                     this.domainService.CalculateResistanceChar(resource.ResistanceUnits, (decimal)resource.Resistance)
            };

            return x;
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

            var newDataSheets = resource.Part.DataSheets.Select(s => new PartDataSheet
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
                                            new Supplier
                                                {
                                                    Id = (int)a.SupplierId,
                                                    Name = a.SupplierName
                                                }
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
            entity.CapacitanceUnit = resource.CapacitanceUnit;
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
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            return source => source.PartNumber == searchTerm.ToUpper();
        }
    }
}
