﻿namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ConsignmentRepository : IQueryRepository<Consignment>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ConsignmentRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Consignment FindBy(Expression<Func<Consignment, bool>> expression)
        {
            return this.serviceDbContext.Consignments.Where(expression)
                .Include(c => c.Country)
                .Include(c => c.Invoices)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Consignment> FilterBy(Expression<Func<Consignment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Consignment> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
