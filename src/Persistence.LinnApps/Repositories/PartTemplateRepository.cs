namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Microsoft.EntityFrameworkCore;

    public class PartTemplateRepository : IRepository<PartTemplate, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartTemplateRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartTemplate FindById(string key)
        {
            return this.serviceDbContext
                .PartTemplates
                .AsNoTracking()
                .Where(p => p.PartRoot == key)
                .ToList()
                .FirstOrDefault();
        }

        public IQueryable<PartTemplate> FindAll()
        {
            return this.serviceDbContext.PartTemplates;
        }

        public void Add(PartTemplate entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PartTemplate entity)
        {
            throw new NotImplementedException();
        }

        public PartTemplate FindBy(Expression<Func<PartTemplate, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartTemplate> FilterBy(Expression<Func<PartTemplate, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
