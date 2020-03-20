namespace Linn.Stores.Persistence.LinnApps
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class DepartmentRepository : IRepository<Department, string>
    {
        public Department FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Department> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Department entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Department entity)
        {
            throw new NotImplementedException();
        }

        public Department FindBy(Expression<Func<Department, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Department> FilterBy(Expression<Func<Department, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}