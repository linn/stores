namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class FootprintRefOptionsRepository : IQueryRepository<FootprintRefOption>
    {
        private readonly ServiceDbContext serviceDbContext;

        public FootprintRefOptionsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public FootprintRefOption FindBy(Expression<Func<FootprintRefOption, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FootprintRefOption> FilterBy(Expression<Func<FootprintRefOption, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FootprintRefOption> FindAll()
        {
            return this.serviceDbContext.FootRefOptions;
        }
    }
}
