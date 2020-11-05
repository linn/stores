using Microsoft.EntityFrameworkCore;

namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class MechPartSourcesRepository : IRepository<MechPartSourceWithPartInfo, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        private readonly IRepository<Part, int> partRepository;

        private readonly IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository;

        public MechPartSourcesRepository(
            ServiceDbContext serviceDbContext, 
            IRepository<Part, int> partRepository,
            IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository)
        {
            this.serviceDbContext = serviceDbContext;
            this.partRepository = partRepository;
            this.dataSheetRepository = dataSheetRepository;
        }

        public MechPartSourceWithPartInfo FindById(int key)
        {
             var source = this.serviceDbContext.MechPartSources.Where(s => s.Id == key)
                .Include(s => s.ProposedBy)
                .ToList().FirstOrDefault();

             var sourceWithPartInfo = new MechPartSourceWithPartInfo(source)
             {
                 LinnPart = this.partRepository.FindBy(p => p.PartNumber == source.LinnPartNumber),
                 DataSheets = this.dataSheetRepository.FilterBy(s => s.PartNumber == source.PartNumber)
             };

             return sourceWithPartInfo;
        }

        public IQueryable<MechPartSourceWithPartInfo> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(MechPartSourceWithPartInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(MechPartSourceWithPartInfo entity)
        {
            throw new NotImplementedException();
        }

        public MechPartSourceWithPartInfo FindBy(Expression<Func<MechPartSourceWithPartInfo, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MechPartSourceWithPartInfo> FilterBy(Expression<Func<MechPartSourceWithPartInfo, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
