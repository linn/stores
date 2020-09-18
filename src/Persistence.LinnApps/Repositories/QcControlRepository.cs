namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class QcControlRepository : IRepository<QcControl, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public QcControlRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public QcControl FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<QcControl> FindAll()
        {
            return this.serviceDbContext.QcControl;
        }

        public void Add(QcControl entity)
        {
            this.serviceDbContext.QcControl.Add(entity);
            this.serviceDbContext.SaveChanges();
        }

        public void Remove(QcControl entity)
        {
            throw new NotImplementedException();
        }

        public QcControl FindBy(Expression<Func<QcControl, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<QcControl> FilterBy(Expression<Func<QcControl, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}