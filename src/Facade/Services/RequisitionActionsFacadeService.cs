namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;

    public class RequisitionActionsFacadeService : IRequisitionActionsFacadeService
    {
        private readonly IRequisitionService requisitionService;

        private readonly IRepository<RequisitionHeader, int> requisitionRepository;

        public RequisitionActionsFacadeService(
            IRequisitionService requisitionService,
            IRepository<RequisitionHeader, int> requisitionRepository)
        {
            this.requisitionService = requisitionService;
            this.requisitionRepository = requisitionRepository;
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

        public IResult<IEnumerable<ReqMove>> GetMoves(int reqNumber)
        {
            throw new NotImplementedException();
        }
    }
}
