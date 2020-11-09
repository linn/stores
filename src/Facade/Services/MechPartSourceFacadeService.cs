using System.Linq;

namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Domain.LinnApps;

    public class MechPartSourceFacadeService : FacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
    {
        private readonly IRepository<Employee, int> employeeRepository;

        private readonly IMechPartSourceService domainService;

        public MechPartSourceFacadeService(
            IRepository<MechPartSource, int> repository, 
            ITransactionManager transactionManager,
            IMechPartSourceService domainService,
            IRepository<Employee, int> employeeRepository) : base(repository, transactionManager)
        {
            this.employeeRepository = employeeRepository;
            this.domainService = domainService;
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
                SamplesRequired = resource.SamplesRequired
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

            var currentDataSheets = entity.Part.DataSheets;

            var newDataSheets = resource.Part.DataSheets.Select(s => new PartDataSheet
                                                                        {
                                                                            Sequence = s.Sequence,
                                                                            PdfFilePath = s.PdfFilePath
                                                                        });

            this.domainService.GetUpdatedDataSheets(currentDataSheets, newDataSheets);
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            return source => source.PartNumber == searchTerm.ToUpper();
        }
    }
}
