namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;

    using Microsoft.EntityFrameworkCore;

    public class CartonTypeRepository : IRepository<CartonType, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public CartonTypeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public CartonType FindById(string key)
        {
            return this.serviceDbContext.CartonTypes
                .Where(p => p.CartonTypeName == key)
                .ToList().FirstOrDefault();
        }

        public IQueryable<CartonType> FindAll()
        {
            return this.serviceDbContext.CartonTypes;
        }

        public void Add(CartonType entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(CartonType entity)
        {
            throw new NotImplementedException();
        }

        public CartonType FindBy(Expression<Func<CartonType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CartonType> FilterBy(Expression<Func<CartonType, bool>> expression)
        {
            return this.serviceDbContext.CartonTypes.AsNoTracking().Where(expression);
        }
    }
}
