namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Resources.Tqms;

    public class TqmsCategoriesFacadeService : FacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource>
    {
        public TqmsCategoriesFacadeService(IRepository<TqmsCategory, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override TqmsCategory CreateFromResource(TqmsCategoryResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(TqmsCategory entity, TqmsCategoryResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<TqmsCategory, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
