namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookTransportCodeRepository : IRepository<ImportBookTransportCode, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookTransportCodeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookTransportCode FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookTransportCode> FindAll()
        {
            return this.serviceDbContext.ImportBookTransportCodes.Where(x => true);
        }

        public void Add(ImportBookTransportCode entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImportBookTransportCode entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookTransportCode FindBy(Expression<Func<ImportBookTransportCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookTransportCode> FilterBy(Expression<Func<ImportBookTransportCode, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}