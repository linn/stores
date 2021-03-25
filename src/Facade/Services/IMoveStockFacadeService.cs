namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources.RequestResources;

    public interface IMoveStockFacadeService
    {
        IResult<RequisitionProcessResult> MoveStock(MoveStockRequestResource requestResource);
    }
}
