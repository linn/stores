namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    public class RequisitionService : IRequisitionService
    {
        private readonly IStoresPack storesPack;

        private readonly IRepository<RequisitionHeader, int> requisitionHeaderRepository;

        public RequisitionService(
            IStoresPack storesPack,
            IRepository<RequisitionHeader, int> requisitionHeaderRepository)
        {
            this.storesPack = storesPack;
            this.requisitionHeaderRepository = requisitionHeaderRepository;
        }

        public RequisitionActionResult Unallocate(int reqNumber, int? reqLine, int userNumber)
        {
            var header = this.requisitionHeaderRepository.FindById(reqNumber);
            var result = this.storesPack.UnallocateRequisition(reqNumber, reqLine, userNumber);
            
            return new RequisitionActionResult
                       {
                           RequisitionHeader = header, Message = result.Message, Success = result.Success
                       };
        }
    }
}
