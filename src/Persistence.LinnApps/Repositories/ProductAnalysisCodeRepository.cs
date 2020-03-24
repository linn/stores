namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ProductAnalysisCodeRepository : IQueryRepository<ProductAnalysisCode>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ProductAnalysisCodeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ProductAnalysisCode FindBy(Expression<Func<ProductAnalysisCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductAnalysisCode> FilterBy(Expression<Func<ProductAnalysisCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductAnalysisCode> FindAll()
        {
            return this.serviceDbContext.ProductAnalysisCodes;
        }
    }
}