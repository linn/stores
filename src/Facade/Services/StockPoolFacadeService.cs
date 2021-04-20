namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.StockLocators;

    public class StockPoolFacadeService : FacadeService<StockPool, int, StockPoolResource, StockPoolResource>
    {
        public StockPoolFacadeService(IRepository<StockPool, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override StockPool CreateFromResource(StockPoolResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(StockPool entity, StockPoolResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StockPool, bool>> SearchExpression(string searchTerm)
        {
            return pool => pool.DateInvalid == null && (pool.StockPoolCode.ToUpper().Contains(searchTerm.ToUpper()) ||
                                                                pool.Description.ToUpper().Contains(searchTerm.ToUpper()));
        }
    }
}
