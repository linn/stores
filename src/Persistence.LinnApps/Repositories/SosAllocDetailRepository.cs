namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;

    using Microsoft.EntityFrameworkCore;

    public class SosAllocDetailRepository : IRepository<SosAllocDetail, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SosAllocDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Remove(SosAllocDetail entity)
        {
            throw new NotImplementedException();
        }

        public SosAllocDetail FindBy(Expression<Func<SosAllocDetail, bool>> expression)
        {
            return this.serviceDbContext.SosAllocDetails.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SosAllocDetail> FilterBy(Expression<Func<SosAllocDetail, bool>> expression)
        {
            return this.serviceDbContext.SosAllocDetails.Where(expression);
        }

        public SosAllocDetail FindById(int key)
        {
            return this.serviceDbContext.SosAllocDetails.Where(a => a.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<SosAllocDetail> FindAll()
        {
            return this.serviceDbContext.SosAllocDetails.AsNoTracking();
        }

        public void Add(SosAllocDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
