﻿namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Microsoft.EntityFrameworkCore;

    public class PartRepository : IRepository<Part, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Part FindById(int key)
        {
            var result = this.serviceDbContext.Parts.Where(p => p.Id == key)
                .Include(p => p.AccountingCompany)
                .Include(p => p.ParetoClass)
                .Include(p => p.ProductAnalysisCode)
                .Include(p => p.DecrementRule)
                .Include(p => p.AssemblyTechnology)
                .Include(p => p.CreatedBy)
                .Include(p => p.MadeLiveBy)
                .Include(p => p.PhasedOutBy)
                .Include(p => p.SernosSequence)
                .Include(p => p.PreferredSupplier)
                .Include(p => p.NominalAccount).ThenInclude(a => a.Department)
                .Include(p => p.NominalAccount).ThenInclude(a => a.Nominal)
                .Include(p => p.DataSheets)
                .Include(p => p.MechPartSource)
                .ThenInclude(m => m.MechPartManufacturerAlts)
                .ToList().FirstOrDefault();
            
            if (result != null)
            {
                result.SalesArticle =
                    this.serviceDbContext.SalesArticles.Where(a => a.ArticleNumber == result.PartNumber)
                        .ToList().FirstOrDefault();
            }
            
            return result;
        }

        public IQueryable<Part> FindAll()
        {
            return this.serviceDbContext.Parts
            .AsNoTracking()
            .Include(p => p.AccountingCompany)
            .Include(p => p.ParetoClass)
            .Include(p => p.ProductAnalysisCode)
            .Include(p => p.DecrementRule)
            .Include(p => p.AssemblyTechnology)
            .Include(p => p.CreatedBy)
            .Include(p => p.MadeLiveBy)
            .Include(p => p.PhasedOutBy)
            .Include(p => p.SernosSequence)
            .Include(p => p.PreferredSupplier)
            .Include(p => p.NominalAccount).ThenInclude(a => a.Department)
            .Include(p => p.NominalAccount).ThenInclude(a => a.Nominal)
            .Include(p => p.DataSheets)
            .Include(p => p.MechPartSource);
        }

        public void Add(Part entity)
        {
            this.serviceDbContext.Parts.Add(entity);
        }

        public void Remove(Part entity)
        {
            throw new NotImplementedException();
        }

        public Part FindBy(Expression<Func<Part, bool>> expression)
        {
            return this.serviceDbContext.Parts.Where(expression)
                .Include(p => p.AccountingCompany)
                .Include(p => p.ParetoClass)
                .Include(p => p.ProductAnalysisCode)
                .Include(p => p.DecrementRule)
                .Include(p => p.AssemblyTechnology)
                .Include(p => p.CreatedBy)
                .Include(p => p.MadeLiveBy)
                .Include(p => p.PhasedOutBy)
                .Include(p => p.SernosSequence)
                .Include(p => p.PreferredSupplier)
                .Include(p => p.NominalAccount).ThenInclude(a => a.Department)
                .Include(p => p.NominalAccount).ThenInclude(a => a.Nominal)
                .Include(p => p.DataSheets)
                .Include(p => p.MechPartSource)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Part> FilterBy(Expression<Func<Part, bool>> expression)
        {
            return this.serviceDbContext.Parts.Where(expression)
                .AsNoTracking()
                .Include(p => p.MechPartSource);
        }
    }
}
