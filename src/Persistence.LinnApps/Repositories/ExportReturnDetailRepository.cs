﻿namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ExportReturnDetailRepository : IRepository<ExportReturnDetail, ExportReturnDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ExportReturnDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ExportReturnDetail FindById(ExportReturnDetailKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExportReturnDetail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ExportReturnDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ExportReturnDetail entity)
        {
            throw new NotImplementedException();
        }

        public ExportReturnDetail FindBy(Expression<Func<ExportReturnDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExportReturnDetail> FilterBy(Expression<Func<ExportReturnDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}