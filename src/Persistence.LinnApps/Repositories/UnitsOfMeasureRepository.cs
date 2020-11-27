namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class UnitsOfMeasureRepository : IQueryRepository<UnitOfMeasure>
    {
        private readonly ServiceDbContext serviceDbContext;

        public UnitsOfMeasureRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public UnitOfMeasure FindBy(Expression<Func<UnitOfMeasure, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UnitOfMeasure> FilterBy(Expression<Func<UnitOfMeasure, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UnitOfMeasure> FindAll()
        {
            return this.serviceDbContext.UnitsOfMeasure;
        }
    }
}
