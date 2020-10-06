namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class CountryFacadeService : FacadeService<Country, string, CountryResource, CountryResource>
    {
        public CountryFacadeService(IRepository<Country, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Country CreateFromResource(CountryResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Country entity, CountryResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Country, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
