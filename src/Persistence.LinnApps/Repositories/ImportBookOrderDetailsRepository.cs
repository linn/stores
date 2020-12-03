namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ImportBookOrderDetailsRepository : IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookOrderDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookOrderDetail FindById(ImportBookOrderDetailKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookOrderDetail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImportBookOrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImportBookOrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookOrderDetail FindBy(Expression<Func<ImportBookOrderDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookOrderDetail> FilterBy(Expression<Func<ImportBookOrderDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}