namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;
    using System.Collections.Generic;

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
            string countryCode,
            DateTime? cutOffDate,
            bool excludeUnsuppliableLines,
            bool excludeOnHold,
            bool excludeOverCreditLimit,
            bool excludeNorthAmerica)
        {
            var results = new AllocationResult
                              {
                                  Id = this.allocPack.StartAllocation(
                                      stockPoolCode?.ToUpper(),
                                      despatchLocationCode?.ToUpper(),
                                      accountId,
                                      null,
                                      articleNumber?.ToUpper(),
                                      accountingCompany?.ToUpper(),
                                      cutOffDate,
                                      countryCode?.ToUpper(),
                                      excludeUnsuppliableLines,
                                      excludeOnHold,
                                      excludeOverCreditLimit,
                                      excludeNorthAmerica,
                                      out var notes,
                                      out var sosNotes),
                                  SosNotes = sosNotes,
                                  AllocationNotes = notes
                              };

            this.sosOptionRepository.Add(new SosOption
                                             {
                                                 JobId = results.Id,
                                                 ArticleNumber = articleNumber?.ToUpper(),
                                                 AccountId = accountId,
                                                 DespatchLocationCode = despatchLocationCode?.ToUpper(),
                                                 StockPoolCode = stockPoolCode?.ToUpper(),
                                                 AccountingCompany = accountingCompany?.ToUpper(),
                                                 CountryCode = countryCode?.ToUpper(),
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

        public IEnumerable<SosAllocDetail> PickItems(int jobId, int accountId, int outletNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SosAllocDetail> UnpickItems(int jobId, int accountId, int outletNumber)
        {
            throw new NotImplementedException();
        }
    }
}
