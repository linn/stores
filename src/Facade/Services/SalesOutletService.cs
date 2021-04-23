namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SalesOutletService : ISalesOutletService
    {
        private readonly IQueryRepository<SalesOutlet> repository;

        public SalesOutletService(IQueryRepository<SalesOutlet> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<SalesOutlet>> SearchSalesOutlets(string searchTerm)
        {
            return new SuccessResult<IEnumerable<SalesOutlet>>(
                this.repository.FilterBy(
                    s => (s.Name.ToUpper().Contains(searchTerm.ToUpper())
                          || s.AccountId.ToString().Contains(searchTerm)) && s.DateInvalid == null));
        }
    }
}