namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class RsnService : IRsnService
    {
        private readonly IQueryRepository<Rsn> repository;

        public RsnService(IQueryRepository<Rsn> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Rsn>> Search(string searchTerm)
        {
            return new SuccessResult<IEnumerable<Rsn>>(
                this.repository.FilterBy(
                    x => x.RsnNumber.ToString().Contains(searchTerm) || x.RsnNumber.ToString().Equals(searchTerm)));
        }
    }
}
