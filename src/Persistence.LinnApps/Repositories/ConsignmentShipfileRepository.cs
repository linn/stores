namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ConsignmentShipfileRepository : IRepository<ConsignmentShipfile, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ConsignmentShipfileRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Remove(ConsignmentShipfile entity)
        { 
            this.serviceDbContext.ConsignmentShipfiles.Remove(entity);
            this.serviceDbContext.SaveChanges();
        }

        public ConsignmentShipfile FindBy(Expression<Func<ConsignmentShipfile, bool>> expression)
        {
            return this.serviceDbContext
                .ConsignmentShipfiles
                .Where(expression)
                .Include(s => s.Consignment)
                .ThenInclude(c => c.Invoices)
                .Include(s => s.Consignment)
                .ThenInclude(c => c.SalesAccount)
                .ThenInclude(a => a.ContactDetails)
                .Include(s => s.Consignment)
                .ThenInclude(c => c.Items)
                // .ThenInclude(items => items.SalesOrder)
                // .ThenInclude(o => o.Account)
                // .ThenInclude(a => a.ContactDetails)
                .AsNoTracking()
                .ToList().FirstOrDefault();
        }

        public IQueryable<ConsignmentShipfile> FilterBy(Expression<Func<ConsignmentShipfile, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ConsignmentShipfile FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ConsignmentShipfile> FindAll()
        {
            return this.serviceDbContext
                .ConsignmentShipfiles
                .Include(s => s.Consignment)
                .ThenInclude(c => c.Invoices);
        }

        public void Add(ConsignmentShipfile entity)
        {
            throw new NotImplementedException();
        }
    }
}
