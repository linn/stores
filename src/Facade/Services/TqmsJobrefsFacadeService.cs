namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Resources.Tqms;

    public class TqmsJobrefsFacadeService : FacadeService<TqmsJobRef, string, TqmsJobRefResource, TqmsJobRefResource>
    {
        public TqmsJobrefsFacadeService(IRepository<TqmsJobRef, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override TqmsJobRef CreateFromResource(TqmsJobRefResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(TqmsJobRef entity, TqmsJobRefResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<TqmsJobRef, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
