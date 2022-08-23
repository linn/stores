namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Microsoft.EntityFrameworkCore;

    public class MrPartsRepository : IQueryRepository<MrPart>
    {
        private readonly ServiceDbContext serviceDbContext;

        public MrPartsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public MrPart FindBy(Expression<Func<MrPart, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MrPart> FilterBy(Expression<Func<MrPart, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MrPart> FindAll()
        {
            return this.serviceDbContext.MrParts
                    .Include(p => p.Part)
                    .ThenInclude(p => p.PreferredSupplier)
                    .Include(p => p.Part)
                    .ThenInclude(p => p.QcControls)
                    .Include(p => p.Part)
                    .ThenInclude(p => p.ParetoClass)
                    .AsNoTracking();
        }
    }
}
