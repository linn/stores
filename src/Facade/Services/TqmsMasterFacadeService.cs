namespace Linn.Stores.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Resources.Tqms;

    public class TqmsMasterFacadeService : SingleRecordFacadeService<TqmsMaster, TqmsMasterResource>
    {
        private readonly ISingleRecordRepository<TqmsMaster> tqmsMasterRepository;

        public TqmsMasterFacadeService(
            ISingleRecordRepository<TqmsMaster> tqmsMasterRepository,
            ITransactionManager transactionManager)
            : base(tqmsMasterRepository, transactionManager)
        {
            this.tqmsMasterRepository = tqmsMasterRepository;
        }

        protected override void UpdateFromResource(TqmsMaster entity, TqmsMasterResource updateResource)
        {
            throw new NotImplementedException();
        }
    }
}
