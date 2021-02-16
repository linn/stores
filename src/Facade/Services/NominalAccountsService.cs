﻿namespace Linn.Stores.Facade.Services
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
            IQueryRepository<Nominal> nominalRepository,
            IQueryRepository<Department> departmentRepository,
            IQueryRepository<NominalAccount> nominalAccountRepository)
        {
            this.nominalAccountRepository = nominalAccountRepository;
        }

        public IResult<IEnumerable<NominalAccount>> GetNominalAccounts(string searchTerm)
        {
            var result = this.nominalAccountRepository
                .FilterBy(n 
                    => n.Department.DepartmentCode.ContainsIgnoringCase(searchTerm)
                    || n.Department.DepartmentCode.ContainsIgnoringCase(searchTerm)
                    || n.Nominal.NominalCode.ContainsIgnoringCase(searchTerm)
                    || n.Nominal.Description.ContainsIgnoringCase(searchTerm));
            return new SuccessResult<IEnumerable<NominalAccount>>(result);
        }
    }
}
