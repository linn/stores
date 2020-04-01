namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class AssemblyTechnologyRepository : IRepository<AssemblyTechnology, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AssemblyTechnologyRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AssemblyTechnology FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AssemblyTechnology> FindAll()
        {
            return this.serviceDbContext.AssemblyTechnologies;
        }

        public void Add(AssemblyTechnology entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(AssemblyTechnology entity)
        {
            throw new NotImplementedException();
        }

        public AssemblyTechnology FindBy(Expression<Func<AssemblyTechnology, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AssemblyTechnology> FilterBy(Expression<Func<AssemblyTechnology, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}