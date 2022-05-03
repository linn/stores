namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTqmsOverridesService : 
        FacadeService<PartTqmsOverride, string, PartTqmsOverrideResource, PartTqmsOverrideResource>
    {
        public PartTqmsOverridesService(IRepository<PartTqmsOverride, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartTqmsOverride CreateFromResource(PartTqmsOverrideResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PartTqmsOverride entity, PartTqmsOverrideResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PartTqmsOverride, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
