namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SernosSequenceRepository : IQueryRepository<SernosSequence>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SernosSequenceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SernosSequence FindBy(Expression<Func<SernosSequence, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SernosSequence> FilterBy(Expression<Func<SernosSequence, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SernosSequence> FindAll()
        {
            return this.serviceDbContext.SernosSequences;
        }
    }
}