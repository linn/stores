using System;
using System.Linq.Expressions;
using Linn.Common.Facade;
using Linn.Common.Persistence;
using Linn.Stores.Domain.LinnApps;
using Linn.Stores.Resources;

namespace Linn.Stores.Facade.Services
{
    public class RsnFacadeService : FacadeService<Rsn, string, RsnResource, RsnResource>
    {
        public RsnFacadeService(IRepository<Rsn, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }


        protected override Rsn CreateFromResource(RsnResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Rsn entity, RsnResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Rsn, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
