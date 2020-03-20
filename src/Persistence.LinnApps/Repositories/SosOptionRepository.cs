namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Sos;

    public class SosOptionRepository : IRepository<SosOption, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SosOptionRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SosOption FindById(int key)
        {
            return this.serviceDbContext
                .SosOptions
                .Where(p => p.JobId == key)
                .ToList()
                .FirstOrDefault();
        }

        public IQueryable<SosOption> FindAll()
        {
            return this.serviceDbContext.SosOptions;
        }

        public void Add(SosOption entity)
        {
            this.serviceDbContext.SosOptions.Add(entity);
        }

        public void Remove(SosOption entity)
        {
            throw new NotImplementedException();
        }

        public SosOption FindBy(Expression<Func<SosOption, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SosOption> FilterBy(Expression<Func<SosOption, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}