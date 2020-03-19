namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ProductAnalysisCodeRepository : IRepository<ProductAnalysisCode, string>
    {
        public ProductAnalysisCode FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductAnalysisCode> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ProductAnalysisCode entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductAnalysisCode entity)
        {
            throw new NotImplementedException();
        }

        public ProductAnalysisCode FindBy(Expression<Func<ProductAnalysisCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductAnalysisCode> FilterBy(Expression<Func<ProductAnalysisCode, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}