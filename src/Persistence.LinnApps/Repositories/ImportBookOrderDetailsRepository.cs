namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ImportBookOrderDetailsRepository : IRepository<ImpBookOrderDetail, ImportBookOrderDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookOrderDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImpBookOrderDetail FindById(ImportBookOrderDetailKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImpBookOrderDetail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImpBookOrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImpBookOrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public ImpBookOrderDetail FindBy(Expression<Func<ImpBookOrderDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImpBookOrderDetail> FilterBy(Expression<Func<ImpBookOrderDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}