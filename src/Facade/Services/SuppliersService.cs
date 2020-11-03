namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;

    public class SuppliersService : FacadeService<Supplier, int, SupplierResource, SupplierResource>,
                                   IFacadeWithSearchReturnTen<Supplier, int, SupplierResource, SupplierResource>
    {
        private readonly IRepository<Supplier, int> repository;

        public SuppliersService(IRepository<Supplier, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Supplier>> SearchReturnTen(string searchTerm)
        {
            try
            {
                return new SuccessResult<IEnumerable<Supplier>>(this.repository.FilterBy(this.SearchExpression(searchTerm)).ToList().Take(10));
            }
            catch (NotImplementedException)
            {
                return new BadRequestResult<IEnumerable<Supplier>>("Search is not implemented");
            }
        }

        protected override Supplier CreateFromResource(SupplierResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Supplier entity, SupplierResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Supplier, bool>> SearchExpression(string searchTerm)
        {
            return w => !w.DateClosed.HasValue && (w.Id.ToString().Contains(searchTerm) || w.Name.ToUpper().Contains(searchTerm.ToUpper()));
        }
    }
}
