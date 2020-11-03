namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class PartTemplateService : FacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource>
    {
        public PartTemplateService(IRepository<PartTemplate, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartTemplate CreateFromResource(PartTemplateResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PartTemplate entity, PartTemplateResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PartTemplate, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}