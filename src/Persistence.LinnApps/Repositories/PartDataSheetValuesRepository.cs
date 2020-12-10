namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartDataSheetValuesRepository : IQueryRepository<PartDataSheetValues>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartDataSheetValuesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartDataSheetValues FindBy(Expression<Func<PartDataSheetValues, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartDataSheetValues> FilterBy(Expression<Func<PartDataSheetValues, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartDataSheetValues> FindAll()
        {
            return this.serviceDbContext.PartDataSheetValues;
        }
    }
}
