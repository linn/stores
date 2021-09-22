namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class PortFacadeService : FacadeService<Port, string, PortResource, PortResource>
    {
        public PortFacadeService(
            IRepository<Port, string> portRepository,
            ITransactionManager transactionManager)
            : base(portRepository, transactionManager)
        {
        }

        protected override Port CreateFromResource(PortResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Port entity, PortResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Port, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
