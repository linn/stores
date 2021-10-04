﻿namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;

    public class InterCompanyInvoiceRepository : IQueryRepository<InterCompanyInvoice>
    {
        private ServiceDbContext serviceDbContext;

        public InterCompanyInvoiceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public InterCompanyInvoice FindBy(Expression<Func<InterCompanyInvoice, bool>> expression)
        {
            return this.serviceDbContext.IntercompanyInvoices.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<InterCompanyInvoice> FilterBy(Expression<Func<InterCompanyInvoice, bool>> expression)
        {
            return this.serviceDbContext.IntercompanyInvoices.Where(expression);
        }

        public IQueryable<InterCompanyInvoice> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
