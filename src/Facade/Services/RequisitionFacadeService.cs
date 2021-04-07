namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Resources.Requisitions;

    public class RequisitionFacadeService : FacadeService<RequisitionHeader, int, RequisitionResource, RequisitionResource>
    {
        public RequisitionFacadeService(IRepository<RequisitionHeader, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override RequisitionHeader CreateFromResource(RequisitionResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(RequisitionHeader entity, RequisitionResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<RequisitionHeader, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
