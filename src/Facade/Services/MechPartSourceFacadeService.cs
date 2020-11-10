namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Domain.LinnApps;

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class MechPartSourceFacadeService : FacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IRepository<Part, int> partRepository;

        private readonly IMechPartSourceService domainService;

        public MechPartSourceFacadeService(
            IRepository<MechPartSource, int> repository, 
            ITransactionManager transactionManager,
            IMechPartSourceService domainService,
            IRepository<Part, int> partRepository,
            IRepository<Employee, int> employeeRepository) : base(repository, transactionManager)
        {
            this.employeeRepository = employeeRepository;
            this.domainService = domainService;
            this.partRepository = partRepository;
        }

        protected override MechPartSource CreateFromResource(MechPartSourceResource resource)
        {
            return new MechPartSource
            {
                PartNumber = resource.PartNumber,
                AssemblyType = resource.AssemblyType,
                DateEntered = DateTime.Parse(resource.DateEntered),
                DateSamplesRequired = resource.DateSamplesRequired == null ? (DateTime?)null 
                    : DateTime.Parse(resource.DateSamplesRequired),
                EmcCritical = resource.EmcCritical,
                EstimatedVolume = resource.EstimatedVolume,
                LinnPartNumber = resource.LinnPartNumber,
                MechanicalOrElectrical = resource.MechanicalOrElectrical,
                Notes = resource.Notes,
                ProposedBy = resource.ProposedBy == null 
                    ? null : this.employeeRepository.FindById((int)resource.ProposedBy),
                PerformanceCritical = resource.PerformanceCritical,
                SafetyCritical = resource.SafetyCritical,
                SingleSource = resource.SingleSource,
                PartType = resource.PartType,
                RohsReplace = resource.RohsReplace,
                SampleQuantity = resource.SampleQuantity,
                SamplesRequired = resource.SamplesRequired,
                PartToBeReplaced = resource.LinnPartNumber == null 
                    ? null : partRepository.FindBy(p => p.PartNumber == resource.LinnPartNumber)
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
                : partRepository.FindBy(p => p.PartNumber == resource.LinnPartNumber);

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
