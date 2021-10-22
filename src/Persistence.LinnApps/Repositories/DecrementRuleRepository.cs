namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Microsoft.EntityFrameworkCore;

    public class DecrementRuleRepository : IRepository<DecrementRule, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public DecrementRuleRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public DecrementRule FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DecrementRule> FindAll()
        {
            return this.serviceDbContext.DecrementRules.AsNoTracking();
        }

        public void Add(DecrementRule entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(DecrementRule entity)
        {
            throw new NotImplementedException();
        }

        public DecrementRule FindBy(Expression<Func<DecrementRule, bool>> expression)
        {
            return this.serviceDbContext.DecrementRules
                .Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<DecrementRule> FilterBy(Expression<Func<DecrementRule, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
