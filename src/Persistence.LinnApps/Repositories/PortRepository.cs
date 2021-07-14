namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class PortRepository : IRepository<Port, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PortRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Port FindBy(Expression<Func<Port, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Port> FilterBy(Expression<Func<Port, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Port> FindAll()
        {
            return this.serviceDbContext.Ports;
        }

        public Port FindById(string key)
        {
            throw new NotImplementedException();
        }

        public void Add(Port entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Port entity)
        {
            throw new NotImplementedException();
        }
    }
}
