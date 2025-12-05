namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class ParcelRepository : IRepository<Parcel, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ParcelRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Parcel FindById(int key)
        {
            return this.serviceDbContext.Parcels
                .Where(p => p.ParcelNumber == key)
                .Include(p => p.Impbooks)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Parcel> FindAll()
        {
            return this.serviceDbContext.Parcels;
        }

        public void Add(Parcel entity)
        {
            this.serviceDbContext.Parcels.Add(entity);
        }

        public void Remove(Parcel entity)
        {
            throw new NotImplementedException();
        }

        public Parcel FindBy(Expression<Func<Parcel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Parcel> FilterBy(Expression<Func<Parcel, bool>> expression)
        {
            return this.serviceDbContext
                .Parcels
                .Where(expression)
                .Include(p => p.Impbooks);
        }
    }
}
