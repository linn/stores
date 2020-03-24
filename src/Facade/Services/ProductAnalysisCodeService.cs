namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ProductAnalysisCodeService : IProductAnalysisCodeService
    {
        private readonly IQueryRepository<ProductAnalysisCode> repository;

        public ProductAnalysisCodeService(IQueryRepository<ProductAnalysisCode> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<ProductAnalysisCode>> GetProductAnalysisCodes()
        {
            return new SuccessResult<IEnumerable<ProductAnalysisCode>>(
                this.repository.FindAll());
        }
    }
}