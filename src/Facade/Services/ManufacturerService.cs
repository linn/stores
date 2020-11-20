namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class ManufacturerService : 
        FacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource>
    {
        public ManufacturerService(IRepository<Manufacturer, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override Manufacturer CreateFromResource(ManufacturerResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Manufacturer entity, ManufacturerResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Manufacturer, bool>> SearchExpression(string searchTerm)
        {
            return m => m.Description.ToUpper().Contains(searchTerm.ToUpper());
        }
    }
}
