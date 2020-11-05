using System;
using System.Linq.Expressions;
namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartSourceService : FacadeService<MechPartSourceWithPartInfo, int, MechPartSourceResource, MechPartSourceResource>
    {
        public MechPartSourceService(IRepository<MechPartSourceWithPartInfo, int> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
        {
        }

        protected override MechPartSourceWithPartInfo CreateFromResource(MechPartSourceResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(MechPartSourceWithPartInfo entity, MechPartSourceResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<MechPartSourceWithPartInfo, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
