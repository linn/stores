namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;


    public class MechPartSourceWithPartInfoRepository : IMechPartSourceWithPartInfoRepository
    {
        private readonly ServiceDbContext serviceDbContext;

        private readonly IRepository<Part, int> partRepository;

        private readonly IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository;

        public MechPartSourceWithPartInfoRepository(
            ServiceDbContext serviceDbContext, 
            IRepository<Part, int> partRepository,
            IRepository<PartDataSheet, PartDataSheetKey> dataSheetRepository)
        {
            this.serviceDbContext = serviceDbContext;
            this.partRepository = partRepository;
            this.dataSheetRepository = dataSheetRepository;
        }

        public MechPartSourceWithPartInfo FindByPartNumber(string partNumber)
        {
            var source = this.serviceDbContext.MechPartSources.Where(s => s.PartNumber == partNumber)
                .Include(s => s.ProposedBy)
                .ToList().FirstOrDefault();

            var sourceWithPartInfo = new MechPartSourceWithPartInfo(source)
            {
                LinnPart = this.partRepository.FindBy(p => p.PartNumber == source.LinnPartNumber),
                DataSheets = this.dataSheetRepository.FilterBy(s => s.PartNumber == source.PartNumber)
            };

            return sourceWithPartInfo;
        }
    }
}
