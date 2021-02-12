namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandFacadeService : IWandFacadeService
    {
        private readonly IQueryRepository<WandConsignment> wandConsignmentRepository;

        public WandFacadeService(IQueryRepository<WandConsignment> wandConsignmentRepository)
        {
            this.wandConsignmentRepository = wandConsignmentRepository;
        }

        public IResult<IEnumerable<WandConsignment>> GetWandConsignments()
        {
            return new SuccessResult<IEnumerable<WandConsignment>>(this.wandConsignmentRepository.FindAll());
        }
    }
}
