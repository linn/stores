namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SuppliersService : ISuppliersService
    {
        private readonly IQueryRepository<Supplier> repository;

        public SuppliersService(IQueryRepository<Supplier> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Supplier>> GetSuppliers(string searchTerm = null, bool returnClosed = false, bool returnOnlyApprovedCarriers = false)
        {
            if (searchTerm == null)
            {
                return new SuccessResult<IEnumerable<Supplier>>(this.repository.FilterBy(s => returnClosed || !s.DateClosed.HasValue));
            }

            return new SuccessResult<IEnumerable<Supplier>>(
                this.repository.FilterBy(
                    s => (returnClosed || !s.DateClosed.HasValue)
                         && (!returnOnlyApprovedCarriers || s.ApprovedCarrier.ToUpper().Equals("Y"))
                         && (s.Name.ToUpper().Contains(searchTerm.ToUpper()) || s.Id.ToString().Contains(searchTerm))));
        }
    }
}
