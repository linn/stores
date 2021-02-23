namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    public class WandFacadeService : IWandFacadeService
    {
        private readonly IQueryRepository<WandConsignment> wandConsignmentRepository;

        private readonly IQueryRepository<WandItem> wandItemRepository;

        private readonly IWandService wandService;

        public WandFacadeService(
            IQueryRepository<WandConsignment> wandConsignmentRepository,
            IQueryRepository<WandItem> wandItemRepository,
            IWandService wandService)
        {
            this.wandConsignmentRepository = wandConsignmentRepository;
            this.wandItemRepository = wandItemRepository;
            this.wandService = wandService;
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

        public IResult<WandResult> WandItem(WandItemRequestResource resource)
        {
            try
            {
                var result = this.wandService.Wand(resource.WandAction, resource.WandString, resource.ConsignmentId);
                return new SuccessResult<WandResult>(result);
            }
            catch (Exception ex)
            {
                return new BadRequestResult<WandResult>(ex.Message);
            }
        }
    }
}
