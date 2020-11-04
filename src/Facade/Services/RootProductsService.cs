namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class RootProductsService : IRootProductService
    {
        private readonly IQueryRepository<RootProduct> repository;

        public RootProductsService(IQueryRepository<RootProduct> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<RootProduct>> GetValid(string searchTerm = null)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return new SuccessResult<IEnumerable<RootProduct>>(
                    this.repository.FilterBy(p => !p.DateInvalid.HasValue 
                                                  && p.Name.ToUpper()
                                                      .Contains(searchTerm.ToUpper())));
            }

            return new SuccessResult<IEnumerable<RootProduct>>(
                this.repository.FilterBy(p => !p.DateInvalid.HasValue));
        }
    }
}
