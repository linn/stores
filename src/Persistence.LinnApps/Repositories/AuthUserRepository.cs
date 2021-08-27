namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class AuthUserRepository : IQueryRepository<AuthUser>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AuthUserRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AuthUser FindBy(Expression<Func<AuthUser, bool>> expression)
        {
            return this.serviceDbContext.AuthUsers
                .Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<AuthUser> FilterBy(Expression<Func<AuthUser, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AuthUser> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
