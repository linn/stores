namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SalesOutletService : ISalesOutletService
    {
        private readonly IQueryRepository<SalesOutlet> repository;

        private readonly IQueryRepository<SalesOrder> salesOrderRepository;


        public SalesOutletService(
            IQueryRepository<SalesOutlet> repository,
            IQueryRepository<SalesOrder> salesOrderRepository)
        {
            this.repository = repository;
            this.salesOrderRepository = salesOrderRepository;
        }

        public IResult<IEnumerable<SalesOutlet>> SearchSalesOutlets(string searchTerm)
        {
            return new SuccessResult<IEnumerable<SalesOutlet>>(
                this.repository.FilterBy(
                    s => (s.Name.ToUpper().Contains(searchTerm.ToUpper())
                          || s.AccountId.ToString().Contains(searchTerm)) && s.DateInvalid == null));
        }

        public IResult<IEnumerable<SalesOutlet>> GetByOrders(IEnumerable<int> orderNumbers)
        {
            var orders = this.salesOrderRepository.FilterBy(x => orderNumbers.Contains(x.OrderNumber));

            var outlets = orders.Select(o => o.SalesOutlet);

            return new SuccessResult<IEnumerable<SalesOutlet>>(outlets);
        }

        public IResult<IEnumerable<SalesOutlet>> GetOutletAddresses(int? accountId, string searchTerm)
        {
            if (accountId == null)
            {
                return new BadRequestResult<IEnumerable<SalesOutlet>>("No account specified");
            }

            var outlets = this.repository.FilterBy(o => o.AccountId == accountId && o.DateInvalid == null).ToList();

            if (outlets.Count == 0)
            {
                return new NotFoundResult<IEnumerable<SalesOutlet>>("Account doesnt exist or have outlets");
            }

            if (string.IsNullOrEmpty(searchTerm))
            {
                return new SuccessResult<IEnumerable<SalesOutlet>>(outlets);
            }

            var filteredOutlets = outlets.Where(s => (s.OutletNumber.ToString() + s.Name).Contains(searchTerm.ToUpper()));

            return new SuccessResult<IEnumerable<SalesOutlet>>(filteredOutlets);
        }
    }
}