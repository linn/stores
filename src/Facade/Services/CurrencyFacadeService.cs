namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class CurrencyFacadeService : FacadeService<Currency, string, CurrencyResource, CurrencyResource>
    {
        public CurrencyFacadeService(
            IRepository<Currency, string> transactionCodeRepository,
            ITransactionManager transactionManager)
            : base(transactionCodeRepository, transactionManager)
        {
        }

        protected override Currency CreateFromResource(CurrencyResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Currency entity, CurrencyResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Currency, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
