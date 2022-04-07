namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Extensions;

    public class NominalAccountsService : INominalAccountsService
    {
        private readonly IQueryRepository<NominalAccount> nominalAccountRepository;

        public NominalAccountsService(
            IQueryRepository<NominalAccount> nominalAccountRepository)
        {
            this.nominalAccountRepository = nominalAccountRepository;
        }

        public IResult<IEnumerable<NominalAccount>> GetNominalAccounts(string searchTerm)
        {
            var exactMatches = this.nominalAccountRepository
                .FilterBy(n
                    => n.Department.Description.ToUpper().Equals(searchTerm)
                       || n.Department.DepartmentCode.Equals(searchTerm)
                       || n.Nominal.Description.ToUpper().Equals(searchTerm)
                       || n.Nominal.NominalCode.Equals(searchTerm)).Take(5);
            if (exactMatches.Any())
            {
                return new SuccessResult<IEnumerable<NominalAccount>>(exactMatches);
            }

            var result = this.nominalAccountRepository
                .FilterBy(n
                    => n.Department.DepartmentCode.ContainsIgnoringCase(searchTerm)
                    || n.Department.Description.ContainsIgnoringCase(searchTerm)
                    || n.Nominal.NominalCode.ContainsIgnoringCase(searchTerm)
                    || n.Nominal.Description.ContainsIgnoringCase(searchTerm)).Take(50);
            return new SuccessResult<IEnumerable<NominalAccount>>(result);
        }
    }
}
