namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class DespatchLocationFacadeService : FacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource>
    {
        public DespatchLocationFacadeService(IRepository<DespatchLocation, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override DespatchLocation CreateFromResource(DespatchLocationResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(DespatchLocation entity, DespatchLocationResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<DespatchLocation, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
