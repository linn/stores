namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    public interface IGoodsInFacadeService
    {
        IResult<ProcessResult> DoBookIn(BookInRequestResource requestResource);

        IResult<IEnumerable<LoanDetail>> GetLoanDetails(int loanNumber);

        IResult<ValidatePurchaseOrderResult> ValidatePurchaseOrder(int orderNumber, int line);
    }
}
