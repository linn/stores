namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ConsignmentFacadeService : FacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource>
    {
        public ConsignmentFacadeService(IRepository<Consignment, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Consignment CreateFromResource(ConsignmentResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Consignment entity, ConsignmentUpdateResource updateResource)
        {
            entity.Carrier = updateResource.Carrier;
            entity.Terms = updateResource.Terms;
            entity.HubId = updateResource.HubId;
            entity.ShippingMethod = updateResource.ShippingMethod;
            entity.DespatchLocationCode = updateResource.DespatchLocationCode;
        }

        protected override Expression<Func<Consignment, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
