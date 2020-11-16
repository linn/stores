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
            // possibly move MechPartSource creation to domain service
            var part = resource.LinnPartNumber == null
                ? null
                : this.partRepository.FindBy(p => p.PartNumber == resource.PartNumber);

            // might want to throw exception if part = null and user is trying to add data sheets
            if (part != null)
            {
                part.DataSheets = resource.Part.DataSheets.Select(s => new PartDataSheet
                {
                    Part = part,
                    Sequence = s.Sequence,
                    PdfFilePath = s.PdfFilePath
                });
            }

            return new MechPartSource
            {
                Id = this.databaseService.GetIdSequence("MECH_SOURCE_SEQ"),
                PartNumber = resource.PartNumber,
                AssemblyType = resource.AssemblyType,
                DateEntered = DateTime.Parse(resource.DateEntered),
                DateSamplesRequired = resource.DateSamplesRequired == null
                    ? (DateTime?) null
                    : DateTime.Parse(resource.DateSamplesRequired),
                EmcCritical = resource.EmcCritical,
                EstimatedVolume = resource.EstimatedVolume,
                LinnPartNumber = resource.LinnPartNumber,
                MechanicalOrElectrical = resource.MechanicalOrElectrical,
                Notes = resource.Notes,
                ProposedBy = resource.ProposedBy == null
                    ? null
                    : this.employeeRepository.FindById((int) resource.ProposedBy),
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
                ProductionDate = resource.DateSamplesRequired == null
                    ? (DateTime?) null
                    : DateTime.Parse(resource.ProductionDate),
                SafetyDataDirectory = resource.SafetyDataDirectory,
                Part = part
            };
        }

        protected override void UpdateFromResource(MechPartSource entity, MechPartSourceResource resource)
        {
            entity.AssemblyType = resource.AssemblyType;
            entity.DateSamplesRequired = resource.DateSamplesRequired == null
                ? (DateTime?) null
                : DateTime.Parse(resource.DateSamplesRequired);
            entity.EmcCritical = resource.EmcCritical;
            entity.EstimatedVolume = resource.EstimatedVolume;
            entity.LinnPartNumber = resource.LinnPartNumber;
            entity.MechanicalOrElectrical = resource.MechanicalOrElectrical;
            entity.Notes = resource.Notes;
            entity.ProposedBy = resource.ProposedBy == null
                ? null
                : this.employeeRepository.FindById((int) resource.ProposedBy);
            entity.PerformanceCritical = resource.PerformanceCritical;
            entity.SafetyCritical = resource.SafetyCritical;
            entity.SingleSource = resource.SingleSource;
            entity.PartType = resource.PartType;
            entity.RohsReplace = resource.RohsReplace;
            entity.SampleQuantity = resource.SampleQuantity;
            entity.SamplesRequired = resource.SamplesRequired;
            entity.ProductionDate = resource.ProductionDate == null
                ? (DateTime?) null
                : DateTime.Parse(resource.ProductionDate);
            entity.SafetyDataDirectory = resource.SafetyDataDirectory;
            entity.PartToBeReplaced = resource.LinnPartNumber == null
                ? null
                : this.partRepository.FindBy(p => p.PartNumber == resource.LinnPartNumber);

            var currentDataSheets = entity.Part.DataSheets;

            var newDataSheets = resource.Part.DataSheets.Select(s => new PartDataSheet
                                                                        {
                                                                            Part = entity.Part,
                                                                            Sequence = s.Sequence,
                                                                            PdfFilePath = s.PdfFilePath
                                                                        });

            entity.Part.DataSheets = this.domainService.GetUpdatedDataSheets(currentDataSheets, newDataSheets);
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            return source => source.PartNumber == searchTerm.ToUpper();
        }
    }
}
