namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class LoanHeaderService : ILoanHeaderService
    {
        private readonly IQueryRepository<LoanHeader> repository;

        public LoanHeaderService(IQueryRepository<LoanHeader> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<LoanHeader>> Search(string searchTerm)
        {
            return new SuccessResult<IEnumerable<LoanHeader>>(
                this.repository.FilterBy(
                    x => x.LoanNumber.ToString().Contains(searchTerm) || x.LoanNumber.ToString().Equals(searchTerm)));
        }
    }
}
