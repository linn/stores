namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandFacadeService : IWandFacadeService
    {
        private readonly IQueryRepository<WandConsignment> wandConsignmentRepository;

        private readonly IQueryRepository<WandItem> wandItemRepository;

        public WandFacadeService(
            IQueryRepository<WandConsignment> wandConsignmentRepository,
            IQueryRepository<WandItem> wandItemRepository)
        {
            this.wandConsignmentRepository = wandConsignmentRepository;
            this.wandItemRepository = wandItemRepository;
        }

        public IResult<IEnumerable<WandConsignment>> GetWandConsignments() =>
            new SuccessResult<IEnumerable<WandConsignment>>(this.wandConsignmentRepository.FindAll());

        public IResult<IEnumerable<WandItem>> GetWandItems(int consignmentId)
        {
            try
            {
                var items = this.wandItemRepository.FilterBy(a => a.ConsignmentId == consignmentId).ToList();
                return new SuccessResult<IEnumerable<WandItem>>(items);
            }
            catch (Exception ex)
            {
                return new BadRequestResult<IEnumerable<WandItem>>(ex.Message);
            }
        }
    }
}
