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
                    => !n.Department.DateClosed.HasValue && !n.Nominal.DateClosed.HasValue &&
                       n.Department.ObseleteInStores == "N" &&
                       (n.Department.Description.ToUpper().Equals(searchTerm.ToUpper())
                        || n.Department.DepartmentCode.ToUpper().Equals(searchTerm.ToUpper())
                        || n.Nominal.Description.ToUpper().Equals(searchTerm.ToUpper())
                        || n.Nominal.NominalCode.ToUpper().Equals(searchTerm.ToUpper()))).Take(50);
            if (exactMatches.Any())
            {
                return new SuccessResult<IEnumerable<NominalAccount>>(exactMatches);
            }

            var result = this.nominalAccountRepository
                .FilterBy(n
                    => !n.Department.DateClosed.HasValue && !n.Nominal.DateClosed.HasValue &&
                       n.Department.ObseleteInStores == "N" &&
                       (n.Department.DepartmentCode.ContainsIgnoringCase(searchTerm)
                       || n.Department.Description.ContainsIgnoringCase(searchTerm)
                       || n.Nominal.NominalCode.ContainsIgnoringCase(searchTerm)
                       || n.Nominal.Description.ContainsIgnoringCase(searchTerm))).Take(50);
            return new SuccessResult<IEnumerable<NominalAccount>>(result);
        }
    }
}
