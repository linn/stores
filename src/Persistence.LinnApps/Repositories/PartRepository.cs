﻿namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Microsoft.EntityFrameworkCore;

    public class PartRepository : IPartRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Part FindById(int key)
        {
            var result = this.serviceDbContext.Parts
                .Include(p => p.QcControls).ThenInclude(q => q.Employee)
                .SingleOrDefault(p => p.Id == key);

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
            return this.serviceDbContext.Parts
                .Include(p => p.AccountingCompany).AsNoTracking()
                .Include(p => p.ParetoClass)
                .Include(p => p.ProductAnalysisCode)
                .Include(p => p.DecrementRule)
                .Include(p => p.AssemblyTechnology)
                .Include(p => p.CreatedBy)
                .Include(p => p.MadeLiveBy)
                .Include(p => p.PhasedOutBy)
                .Include(p => p.SernosSequence)
                .Include(p => p.PreferredSupplier)
                .Include(p => p.QcControls).ThenInclude(q => q.Employee)
                .Include(p => p.NominalAccount).ThenInclude(a => a.Department)
                .Include(p => p.NominalAccount).ThenInclude(a => a.Nominal)
                .Include(p => p.DataSheets)
                .Include(p => p.MechPartSource)
                .AsNoTracking()
                .Where(expression)
                .Include(p => p.MechPartSource);
        }

        public IEnumerable<Part> SearchParts(string searchTerm, int? resultLimit)
        {
            var result = this.serviceDbContext.Parts
                .Include(p => p.MechPartSource)
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.PartNumber, $"%{searchTerm}%"))
                .Union(this.serviceDbContext.Parts
                    .Where(x => EF.Functions.Like(x.Description, $"%{searchTerm}%")));

            if (resultLimit.HasValue)
            {
                result = result.Take((int)resultLimit);
            }

            return result;
        }

        public Part GetByIdWithManufacturerData(int id)
        {
            return this.serviceDbContext.Parts.AsNoTracking()
                .Include(p => p.PreferredSupplier)
                .Include(p => p.MechPartSource)
                .ThenInclude(s => s.MechPartManufacturerAlts).ThenInclude(m => m.Manufacturer)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Part> SearchPartsWithWildcard(
            string partNumberSearchTerm, 
            string descriptionSearchTerm,
            string productAnalysisCodeSearchTerm,
            bool newestFirst = false,
            int? limit = null)
        {
            var result = this.serviceDbContext.Parts.Include(x => x.ProductAnalysisCode).AsNoTracking().Where(
                x => (string.IsNullOrEmpty(partNumberSearchTerm) || EF.Functions.Like(
                          x.PartNumber,
                          $"{partNumberSearchTerm.Replace("*", "%")}"))
                     && (string.IsNullOrEmpty(descriptionSearchTerm) || EF.Functions.Like(
                             x.Description,
                             $"{descriptionSearchTerm.Replace("*", "%")}"))
                    
                     && (string.IsNullOrEmpty(productAnalysisCodeSearchTerm)
                         || (x.ProductAnalysisCode != null  
                         && EF.Functions.Like(x.ProductAnalysisCode.ProductCode, $"{productAnalysisCodeSearchTerm.Replace("*", "%")}"))));
            
            if (newestFirst)
            {
                result = result.OrderByDescending(x => x.Id);
            }

            if (limit.HasValue)
            {
                result = result.Take((int)limit);
            }

            return result;
        }
    }
}
