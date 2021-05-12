namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartDataSheetRepository : IRepository<PartDataSheet, PartDataSheetKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartDataSheetRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartDataSheet FindById(PartDataSheetKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartDataSheet> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PartDataSheet entity)
        {
            this.serviceDbContext.PartDataSheets.Add(entity);
            this.serviceDbContext.SaveChanges();
        }

        public void Remove(PartDataSheet entity)
        {
            throw new NotImplementedException();
        }

        public PartDataSheet FindBy(Expression<Func<PartDataSheet, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartDataSheet> FilterBy(Expression<Func<PartDataSheet, bool>> expression)
        {
            return this.serviceDbContext.PartDataSheets.Where(expression);
        }
    }
}
