namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartRepository : IRepository<Part, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Part FindById(int key)
        {
            return this.serviceDbContext
                .Parts
                .Where(p => p.Id == key)
                .ToList()
                .FirstOrDefault();
        }

        public IQueryable<Part> FindAll()
        {
            return this.serviceDbContext.Parts;
        }

        public void Add(Part entity)
        {
            this.serviceDbContext.Parts.Add(entity);
        }

        public void Remove(Part entity)
        {
            throw new NotImplementedException();
        }

        public Part FindBy(Expression<Func<Part, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Part> FilterBy(Expression<Func<Part, bool>> expression)
        {
            return this.serviceDbContext
                .Parts
                .Where(expression)
                .ToList()
                .AsQueryable();
        }
    }
}