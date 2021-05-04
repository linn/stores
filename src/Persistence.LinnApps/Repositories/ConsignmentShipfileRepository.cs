namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ConsignmentShipfileRepository : IQueryRepository<ConsignmentShipfile>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ConsignmentShipfileRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ConsignmentShipfile FindBy(Expression<Func<ConsignmentShipfile, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ConsignmentShipfile> FilterBy(Expression<Func<ConsignmentShipfile, bool>> expression)
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
    }
}
