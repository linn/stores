namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class ReqMovesRepository : IQueryRepository<ReqMove>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ReqMovesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ReqMove FindBy(Expression<Func<ReqMove, bool>> expression)
        {
            return this.serviceDbContext.ReqMoves.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ReqMove> FilterBy(Expression<Func<ReqMove, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ReqMove> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
