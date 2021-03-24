namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using Microsoft.EntityFrameworkCore;

    public class RequisitionHeaderRepository : IRepository<RequisitionHeader, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public RequisitionHeaderRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public RequisitionHeader FindById(int key)
        {
            return this.serviceDbContext.RequisitionHeaders
                .Where(p => p.ReqNumber == key)
                .Include(a => a.Lines)
                .ThenInclude(b => b.Moves)
                .ThenInclude(c => c.Location)
                .Include(a => a.Lines)
                .ThenInclude(b => b.Moves)
                .ThenInclude(c => c.StockLocator)
                .ThenInclude(d => d.StorageLocation)
                .ToList().FirstOrDefault();
        }

        public IQueryable<RequisitionHeader> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(RequisitionHeader entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(RequisitionHeader entity)
        {
            throw new NotImplementedException();
        }

        public RequisitionHeader FindBy(Expression<Func<RequisitionHeader, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RequisitionHeader> FilterBy(Expression<Func<RequisitionHeader, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
