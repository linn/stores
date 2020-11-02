namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class SosAllocHeadFacadeService : ISosAllocHeadFacadeService
    {
        private readonly IQueryRepository<SosAllocHead> sosAllocHeadRepository;

        public SosAllocHeadFacadeService(IQueryRepository<SosAllocHead> sosAllocHeadRepository)
        {
            this.sosAllocHeadRepository = sosAllocHeadRepository;
        }

        public SuccessResult<IEnumerable<SosAllocHead>> GetAllocHeads(int jobId)
        {
            return new SuccessResult<IEnumerable<SosAllocHead>>(
                this.sosAllocHeadRepository.FilterBy(allocHead => allocHead.JobId == jobId));
        }
    }
}