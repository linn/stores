namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Microsoft.EntityFrameworkCore;

    public class MechPartSourceRepository : IRepository<MechPartSource, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public MechPartSourceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public MechPartSource FindById(int key)
        {
            return this.serviceDbContext.MechPartSources.Where(s => s.Id == key)
                .Include(s => s.ProposedBy)
                .Include(s => s.PartToBeReplaced)
                .Include(s => s.Part).ThenInclude(p => p.DataSheets)
                .ToList().FirstOrDefault();
        }

        public IQueryable<MechPartSource> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(MechPartSource entity)
        {
            this.serviceDbContext.Add(entity);
        }

        public void Remove(MechPartSource entity)
        {
            throw new NotImplementedException();
        }

        public MechPartSource FindBy(Expression<Func<MechPartSource, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MechPartSource> FilterBy(Expression<Func<MechPartSource, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
