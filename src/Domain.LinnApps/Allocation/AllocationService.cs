namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Sos;

    public class AllocationService : IAllocationService
    {
        private readonly ISosPack sosPack;

        private readonly IRepository<SosOption, int> sosOptionRepository;

        private readonly ITransactionManager transactionManager;

        public AllocationService(
            ISosPack sosPack,
            IRepository<SosOption, int> sosOptionRepository,
            ITransactionManager transactionManager)
        {
            this.sosPack = sosPack;
            this.sosOptionRepository = sosOptionRepository;
            this.transactionManager = transactionManager;
        }

        public AllocationStart StartAllocation(
            string stockPoolCode,
            string despatchLocationCode,
            int accountId,
            string articleNumber)
        {
            this.sosPack.SetNewJobId();
            var newId = this.sosPack.GetJobId();

            this.sosOptionRepository.Add(new SosOption
                                             {
                                                 JobId = newId,
                                                 ArticleNumber = articleNumber,
                                                 AccountId = accountId,
                                                 DespatchLocationCode = despatchLocationCode,
                                                 StockPoolCode = stockPoolCode
                                             });
            this.transactionManager.Commit();

            return new AllocationStart(newId);
        }
    }
}