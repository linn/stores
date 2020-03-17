namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Resources;

    using PagedList.Core;

    public class PartFacadeService : FacadeService<Part, int, PartResource, PartResource>
    {
        public PartFacadeService(IRepository<Part, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Part CreateFromResource(PartResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Part entity, PartResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Part, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}