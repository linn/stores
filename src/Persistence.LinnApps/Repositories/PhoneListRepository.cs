namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class PhoneListRepository : IQueryRepository<PhoneListEntry>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PhoneListRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PhoneListEntry FindBy(Expression<Func<PhoneListEntry, bool>> expression)
        {
            return this.serviceDbContext.PhoneList
                .Include(p => p.User).FirstOrDefault(expression);
        }

        public IQueryable<PhoneListEntry> FilterBy(Expression<Func<PhoneListEntry, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PhoneListEntry> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
