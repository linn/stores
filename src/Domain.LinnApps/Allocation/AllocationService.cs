namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Sos;

    public class AllocationService : IAllocationService
    {
        private readonly IAllocPack allocPack;

        private readonly IRepository<SosOption, int> sosOptionRepository;

        private readonly ITransactionManager transactionManager;

        public AllocationService(
            IAllocPack allocPack,
            IRepository<SosOption, int> sosOptionRepository,
            ITransactionManager transactionManager)
        {
            this.allocPack = allocPack;
            this.sosOptionRepository = sosOptionRepository;
            this.transactionManager = transactionManager;
        }

        public AllocationResult StartAllocation(
            string stockPoolCode,
            string despatchLocationCode,
            int? accountId,
            string articleNumber,
            string accountingCompany,
            DateTime? cutOffDate,
            bool excludeUnsuppliableLines,
            bool excludeOnHold,
            bool excludeOverCreditLimit)
        {
            var results = new AllocationResult
                              {
                                  Id = this.allocPack.StartAllocation(
                                      stockPoolCode,
                                      despatchLocationCode,
                                      accountId,
                                      null,
                                      articleNumber,
                                      accountingCompany,
                                      cutOffDate,
                                      null,
                                      true,
                                      true,
                                      true,
                                      false,
                                      out var notes,
                                      out var sosNotes),
                                  SosNotes = sosNotes,
                                  AllocationNotes = notes
                              };

            this.sosOptionRepository.Add(new SosOption
                                             {
                                                 JobId = results.Id,
                                                 ArticleNumber = articleNumber,
                                                 AccountId = accountId,
                                                 DespatchLocationCode = despatchLocationCode,
                                                 StockPoolCode = stockPoolCode,
                                                 AccountingCompany = accountingCompany,
                                                 CutOffDate = cutOffDate
                                             });

            this.transactionManager.Commit();

            return results;
        }

        public AllocationResult FinishAllocation(int jobId)
        {
            this.allocPack.FinishAllocation(jobId, out var notes, out var success);
            if (success != "Y")
            {
                throw new FinishAllocationException(notes);
            }

            return new AllocationResult(jobId) { AllocationNotes = notes };
        }
    }
}
