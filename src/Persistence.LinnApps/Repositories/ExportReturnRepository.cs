namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ExportReturnRepository : IRepository<ExportReturn, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ExportReturnRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ExportReturn FindById(int id)
        {
            return this.serviceDbContext.ExportReturns.Include(e => e.ExportReturnDetails)
                .FirstOrDefault(e => e.ReturnId == id);
        }

        public IQueryable<ExportReturn> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ExportReturn entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ExportReturn entity)
        {
            throw new NotImplementedException();
        }

        public ExportReturn FindBy(Expression<Func<ExportReturn, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExportReturn> FilterBy(Expression<Func<ExportReturn, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}