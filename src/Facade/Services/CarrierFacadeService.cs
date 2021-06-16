namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class CarrierFacadeService : FacadeService<Carrier, string, CarrierResource, CarrierResource>
    {
        public CarrierFacadeService(IRepository<Carrier, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Carrier CreateFromResource(CarrierResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Carrier entity, CarrierResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Carrier, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
