namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;

    public class ConsignmentConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        public ConsignmentConsignmentShipfileService(
            IQueryRepository<SalesOrder> salesOrderRepository)
        {
            this.salesOrderRepository = salesOrderRepository;
        }

        public IEnumerable<ConsignmentShipfile> GetEmailDetails(int consignmentId)
        {
            var order = this.salesOrderRepository.FilterBy(
                o => o.ConsignmentItems.Any() && o.ConsignmentItems.All(i => i.ConsignmentId == consignmentId)).ToList();
            throw new System.NotImplementedException();
        }
    }
}
