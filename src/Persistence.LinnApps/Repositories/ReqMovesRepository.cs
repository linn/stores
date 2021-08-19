namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class ReqMovesRepository : IRepository<ReqMove, ReqMoveKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ReqMovesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Remove(ReqMove entity)
        {
            throw new NotImplementedException();
        }

        public ReqMove FindBy(Expression<Func<ReqMove, bool>> expression)
        {
            return this.serviceDbContext.ReqMoves.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<ReqMove> FilterBy(Expression<Func<ReqMove, bool>> expression)
        {
            return this.serviceDbContext.ReqMoves.Where(expression);
        }

        public ReqMove FindById(ReqMoveKey key)
        {
            return this.serviceDbContext.ReqMoves.Where(
                x => x.ReqNumber == key.ReqNumber && x.LineNumber == key.LineNumber && x.Sequence == key.Seq)
                .ToList().FirstOrDefault();
        }

        public IQueryable<ReqMove> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ReqMove entity)
        {
            throw new NotImplementedException();
        }
    }
}
