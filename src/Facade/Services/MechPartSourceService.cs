using System;
using System.Linq.Expressions;
namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartSourceService : FacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
    {
        public MechPartSourceService(IRepository<MechPartSource, int> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
        {
        }

        protected override MechPartSource CreateFromResource(MechPartSourceResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(MechPartSource entity, MechPartSourceResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<MechPartSource, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
