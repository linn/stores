namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

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
                .Include(c => c.Invoices)
                .Include(a => a.Address)
                .ThenInclude(c => c.Country)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Consignment> FilterBy(Expression<Func<Consignment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Consignment FindById(int key)
        {
            return this.serviceDbContext.Consignments
                .Where(c => c.ConsignmentId == key)
                .Include(a => a.Address)
                .ThenInclude(c => c.Country)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Consignment> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Consignment entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Consignment entity)
        {
            throw new NotImplementedException();
        }
    }
}
