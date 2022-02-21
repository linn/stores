namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

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
                .ThenInclude(c => c.Person)
                .Include(s => s.Consignment)
                .ThenInclude(c => c.Items)
                .ToList().FirstOrDefault();
        }

        public IQueryable<ConsignmentShipfile> FilterBy(Expression<Func<ConsignmentShipfile, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ConsignmentShipfile FindById(int key)
        {
            return this.serviceDbContext.ConsignmentShipfiles
                .Include(s => s.Consignment)
                .ThenInclude(c => c.Invoices)
                .Include(s => s.Consignment)
                .ThenInclude(c => c.SalesAccount)
                .ThenInclude(a => a.ContactDetails)
                .ThenInclude(c => c.Person)
                .Include(s => s.Consignment)
                .ThenInclude(c => c.Items)
                .Where(x => x.Id == key).ToList().FirstOrDefault();
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
            this.serviceDbContext.Add(entity);
        }
    }
}
