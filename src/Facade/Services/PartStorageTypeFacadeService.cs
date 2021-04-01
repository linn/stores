namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class PartStorageTypeFacadeService : FacadeService<PartStorageType, int, PartStorageTypeResource, PartStorageTypeResource>
    {
        public PartStorageTypeFacadeService(IRepository<PartStorageType, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PartStorageType CreateFromResource(PartStorageTypeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PartStorageType entity, PartStorageTypeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PartStorageType, bool>> SearchExpression(string searchTerm)
        {
            return partStorageType => partStorageType.PartNumber == searchTerm.ToUpper();
        }
    }
}
