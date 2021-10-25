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
            return this.serviceDbContext.MechPartSources.Include(s => s.ProposedBy)
                .Include(s => s.PartToBeReplaced).Include(s => s.Part).ThenInclude(p => p.DataSheets)
                .Include(s => s.MechPartManufacturerAlts).ThenInclude(m => m.Manufacturer)
                .Include(s => s.Usages).ThenInclude(u => u.RootProduct)
                .Include(s => s.MechPartManufacturerAlts).ThenInclude(m => m.ApprovedBy).Include(s => s.Usages)
                .ThenInclude(u => u.RootProduct).Include(s => s.PartCreatedBy).Include(s => s.VerifiedBy)
                .Include(s => s.McitVerifiedBy).Include(s => s.ApplyTCodeBy).Include(s => s.RemoveTCodeBy)
                .Include(s => s.CancelledBy).Include(s => s.MechPartAlts).ThenInclude(a => a.Supplier)
                .Include(s => s.PurchasingQuotes).ThenInclude(q => q.Supplier)
                .Include(s => s.PurchasingQuotes).ThenInclude(q => q.Manufacturer)
                .Where(s => s.Id == key).FirstOrDefault();
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
