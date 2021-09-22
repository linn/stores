namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.GoodsIn;

    public interface IGoodsInFacadeService
    {
        IResult<BookInResult> DoBookIn(BookInRequestResource requestResource);

        IResult<IEnumerable<LoanDetail>> GetLoanDetails(int loanNumber);

        IResult<ValidatePurchaseOrderResult> ValidatePurchaseOrder(int orderNumber, int line);

        IResult<ProcessResult> ValidatePurchaseOrderQty(int orderNumber, int orderLine, int qty);

        IResult<ProcessResult> 
            PrintGoodsInLabels(PrintGoodsInLabelsRequestResource requestResource);

        IResult<ValidateStorageTypeResult> 
            ValidateStorageType(ValidateStorageTypeRequestResource requestResource);
    }
}
