namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class ConsignmentFacadeService : FacadeService<Consignment, int, ConsignmentResource, ConsignmentResource>
    {
        public ConsignmentFacadeService(IRepository<Consignment, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Consignment CreateFromResource(ConsignmentResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Consignment entity, ConsignmentResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Consignment, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
