﻿namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class NominalAccountRepository : IQueryRepository<NominalAccount>
    {
        private readonly ServiceDbContext serviceDbContext;

        public NominalAccountRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public NominalAccount FindBy(Expression<Func<NominalAccount, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<NominalAccount> FilterBy(Expression<Func<NominalAccount, bool>> expression)
        {
            return this.serviceDbContext.NominalAccounts.Where(expression);
        }

        public IQueryable<NominalAccount> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}