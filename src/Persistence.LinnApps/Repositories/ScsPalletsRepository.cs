namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Scs;

    using Microsoft.EntityFrameworkCore;

    public class ScsPalletsRepository : IScsPalletsRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public ScsPalletsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ScsStorePallet FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ScsStorePallet> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ScsStorePallet entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ScsStorePallet entity)
        {
            throw new NotImplementedException();
        }

        public ScsStorePallet FindBy(Expression<Func<ScsStorePallet, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ScsStorePallet> FilterBy(Expression<Func<ScsStorePallet, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void ReplaceAll(IEnumerable<ScsStorePallet> pallets)
        {
            // TODO make the replace only remove pallets that are missing from pallets, add new pallets and update existing ones
            // in this version of EF this is still crippling slow about 20 minutes for 2000 pallets which is why there is a fast
            // cheaty way

            // should replace the existing SCS_PALLETS with this one
            var existingPallets = this.serviceDbContext.ScsPallets.AsNoTracking().ToList();
            if (existingPallets.Any())
            {
                this.serviceDbContext.ScsPallets.RemoveRange(existingPallets);
                // if we don't persist to DB not sure if doing an immediate AddRange would cause issues with primary keys
                this.serviceDbContext.SaveChanges();
            }

            this.serviceDbContext.ScsPallets.AddRange(pallets);

            this.serviceDbContext.SaveChanges(); // this takes 20 minutes with tracking
        }
    }
}
