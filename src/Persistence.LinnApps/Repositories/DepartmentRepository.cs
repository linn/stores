namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Domain.LinnApps;

    using Linn.Common.Persistence;

    public class DepartmentRepository : IQueryRepository<Department>
    {
        private readonly ServiceDbContext serviceDbContext;

        public DepartmentRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Department FindBy(Expression<Func<Department, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Department> FilterBy(Expression<Func<Department, bool>> expression)
        {
            return this.serviceDbContext.Departments.Where(expression);
        }

        public IQueryable<Department> FindAll()
        {
            return this.serviceDbContext.Departments;
        }
    }
}