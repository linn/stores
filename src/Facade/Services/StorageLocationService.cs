namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StorageLocationService : FacadeService<StorageLocation, int, StorageLocationResource, StorageLocationResource>
    {
        public StorageLocationService(IRepository<StorageLocation, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override StorageLocation CreateFromResource(StorageLocationResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(StorageLocation entity, StorageLocationResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StorageLocation, bool>> SearchExpression(string searchTerm)
        {
            return l => (l.Description.ToUpper().Contains(searchTerm.ToUpper())
                        || l.LocationCode.ToUpper().Contains(searchTerm.ToUpper())) && l.DateInvalid == null;
        }
    }
}
