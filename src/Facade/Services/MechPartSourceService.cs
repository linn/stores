namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Domain.LinnApps;

    public class MechPartSourceService : FacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
    {
        private readonly IRepository<Employee, int> employeeRepository;
        public MechPartSourceService(
            IRepository<MechPartSource, int> repository, 
            ITransactionManager transactionManager,
            IRepository<Employee, int> employeeRepository) : base(repository, transactionManager)
        {
            this.employeeRepository = employeeRepository;
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

        protected override void UpdateFromResource(MechPartSource entity, MechPartSourceResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
