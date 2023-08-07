namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class EuCreditInvoiceRepository : IQueryRepository<EuCreditInvoice>
    {
        private readonly ServiceDbContext serviceDbContext;

        public EuCreditInvoiceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public EuCreditInvoice FindBy(Expression<Func<EuCreditInvoice, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EuCreditInvoice> FilterBy(Expression<Func<EuCreditInvoice, bool>> expression)
        {
            return this.serviceDbContext.EuCreditInvoices.Where(expression);
        }

        public IQueryable<EuCreditInvoice> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
