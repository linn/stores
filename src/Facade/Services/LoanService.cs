namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class LoanService : ILoanService
    {
        private readonly IQueryRepository<Loan> repository;

        public LoanService(IQueryRepository<Loan> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Loan>> Search(string searchTerm)
        {
            return new SuccessResult<IEnumerable<Loan>>(
                this.repository.FilterBy(
                    x => x.LoanNumber.ToString().Contains(searchTerm) || x.LoanNumber.ToString().Equals(searchTerm)));
        }
    }
}
