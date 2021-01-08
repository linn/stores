namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.RequestResources;

    public class SosAllocDetailFacadeService : FacadeFilterService<SosAllocDetail, int, SosAllocDetailResource, SosAllocDetailResource, JobIdRequestResource>
    {
        public SosAllocDetailFacadeService(IRepository<SosAllocDetail, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override SosAllocDetail CreateFromResource(SosAllocDetailResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(SosAllocDetail entity, SosAllocDetailResource updateResource)
        {
            entity.QuantityToAllocate = updateResource.QuantityToAllocate;
        }

        protected override Expression<Func<SosAllocDetail, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<SosAllocDetail, bool>> FilterExpression(JobIdRequestResource searchResource)
        {
            return a => a.JobId == searchResource.JobId;
        }
    }
}
