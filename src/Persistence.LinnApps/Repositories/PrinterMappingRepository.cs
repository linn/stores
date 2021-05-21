namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class PrinterMappingRepository : IRepository<PrinterMapping, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PrinterMappingRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PrinterMapping FindById(int key)
        {
            return this.serviceDbContext.PrinterMappings.FirstOrDefault(p => p.Id == key);
        }

        public IQueryable<PrinterMapping> FindAll()
        {
            return this.serviceDbContext.PrinterMappings;
        }

        public void Add(PrinterMapping entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PrinterMapping entity)
        {
            throw new NotImplementedException();
        }

        public PrinterMapping FindBy(Expression<Func<PrinterMapping, bool>> expression)
        {
            return this.serviceDbContext
                .PrinterMappings
                .Where(expression)
                .ToList().FirstOrDefault();
        }

        public IQueryable<PrinterMapping> FilterBy(Expression<Func<PrinterMapping, bool>> expression)
        {
            return this.serviceDbContext.PrinterMappings.Where(expression);
        }
    }
}
