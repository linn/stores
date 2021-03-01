namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    public class RequisitionActionsFacadeService : IRequisitionActionsFacadeService
    {
        private readonly IRequisitionService requisitionService;

        public RequisitionActionsFacadeService(IRequisitionService requisitionService)
        {
            this.requisitionService = requisitionService;
        }

        public IResult<RequisitionActionResult> Unallocate(int reqNumber, int? lineNumber, int? userNumber)
        {
            if (!userNumber.HasValue)
            {
                return new BadRequestResult<RequisitionActionResult>("You must supply a user number");
            }

            return new SuccessResult<RequisitionActionResult>(
                this.requisitionService.Unallocate(reqNumber, lineNumber, userNumber.Value));
        }
    }
}
