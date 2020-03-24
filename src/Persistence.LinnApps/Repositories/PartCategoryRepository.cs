namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartCategoryRepository : IQueryRepository<PartCategory>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartCategoryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartCategory FindBy(Expression<Func<PartCategory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartCategory> FilterBy(Expression<Func<PartCategory, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartCategory> FindAll()
        {
            return this.serviceDbContext.PartCategories;
        }
    }
}