namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;

    using Microsoft.EntityFrameworkCore;

    public class ConsignmentRepository : IRepository<Consignment, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ConsignmentRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Consignment FindBy(Expression<Func<Consignment, bool>> expression)
        {
            return this.serviceDbContext.Consignments.Where(expression)
                .Include(c => c.Pallets)
                .Include(c => c.Items)
                .Include(c => c.Invoices)
                .Include(c => c.ClosedBy)
                .Include(a => a.Address)
                .ThenInclude(c => c.Country)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Consignment> FilterBy(Expression<Func<Consignment, bool>> expression)
        {
            return this.serviceDbContext.Consignments.Where(expression)
                .Include(c => c.Invoices)
                .Include(a => a.Address)
                .ThenInclude(c => c.Country);
        }

        public Consignment FindById(int key)
        {
            return this.serviceDbContext.Consignments
                .Where(c => c.ConsignmentId == key)
                .Include(c => c.Pallets)
                .Include(c => c.Items)
                .Include(e => e.ExportBooks)
                .Include(c => c.Invoices)
                .Include(c => c.ClosedBy)
                .Include(a => a.Address)
                .ThenInclude(c => c.Country)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Consignment> FindAll()
        {
            return this.serviceDbContext.Consignments.Where(c => c.Status == "L")
                .Include(c => c.Invoices)
                .Include(a => a.Address)
                .ThenInclude(c => c.Country);
        }

        public void Add(Consignment entity)
        {
            this.serviceDbContext.Consignments.Add(entity);
        }

        public void Remove(Consignment entity)
        {
            throw new NotImplementedException();
        }
    }
}
