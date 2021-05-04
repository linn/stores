namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tqms;

    public class TqmsMasterRepository : ISingleRecordRepository<TqmsMaster>
    {
        private readonly ServiceDbContext serviceDbContext;

        public TqmsMasterRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public TqmsMaster GetRecord()
        {
            return this.serviceDbContext.TqmsMaster.ToList().FirstOrDefault();
        }

        public void UpdateRecord(TqmsMaster newValues)
        {
            throw new System.NotImplementedException();
        }
    }
}
