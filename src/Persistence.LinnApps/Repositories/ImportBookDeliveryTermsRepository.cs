namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookDeliveryTermsRepository : IRepository<ImportBookDeliveryTerm, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookDeliveryTermsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookDeliveryTerm FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookDeliveryTerm> FindAll()
        {
            return this.serviceDbContext.ImportBookDeliveryTerms;
        }

        public void Add(ImportBookDeliveryTerm entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImportBookDeliveryTerm entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookDeliveryTerm FindBy(Expression<Func<ImportBookDeliveryTerm, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookDeliveryTerm> FilterBy(Expression<Func<ImportBookDeliveryTerm, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}