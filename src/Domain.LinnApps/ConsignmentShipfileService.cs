namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;

    public class ConsignmentShipfileService : IConsignmentShipfileService
    {
        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        public ConsignmentShipfileService(
            IQueryRepository<SalesOrder> salesOrderRepository)
        {
            this.salesOrderRepository = salesOrderRepository;
        }

        public IEnumerable<ConsignmentShipfile> GetEmailDetails(IEnumerable<ConsignmentShipfile> shipfiles)
        {
            var result = new List<ConsignmentShipfile>();

            foreach (var shipfile in shipfiles)
            {
                var orders = this.salesOrderRepository.FilterBy(
                        o => o.ConsignmentItems.Any() 
                             && o.ConsignmentItems.All(i => i.ConsignmentId == shipfile.Id))
                    .ToList();

                foreach (var salesOrder in orders)
                {
                    if (salesOrder.Account.OrgId == null)
                    {
                        if (salesOrder.Account.ContactId != null)
                        {
                            // we found a contact
                        }
                        else
                        {
                            // we didn't find a contact
                        }
                    }
                }
            }

            return result;
        }
    }
}
