namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ProductUpgradeRulesRepository : IQueryRepository<ProductUpgradeRule>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductUpgradeRulesRepository(ServiceDbContext serviceDbContext)
        {
           this.serviceDbContext = serviceDbContext; 
        }

        public ProductUpgradeRule FindBy(Expression<Func<ProductUpgradeRule, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductUpgradeRule> FilterBy(Expression<Func<ProductUpgradeRule, bool>> expression)
        {
            return this.serviceDbContext.ProductUpgradeRules.Where(expression);
        }

        public IQueryable<ProductUpgradeRule> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
