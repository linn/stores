namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

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

        public IResult<IEnumerable<Supplier>> GetSuppliers()
        {
            return new SuccessResult<IEnumerable<Supplier>>(
                this.repository.FindAll());
        }
    }
}