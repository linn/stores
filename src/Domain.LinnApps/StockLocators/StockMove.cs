namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class StockMove : ReqMove
    {
        public StockMove(ReqMove reqMove)
        {
            this.ReqNumber = reqMove.ReqNumber;
            this.LineNumber = reqMove.LineNumber;
            this.Sequence = reqMove.Sequence;
            this.Quantity = reqMove.Quantity;
            this.Header = reqMove.Header;
            this.StockLocator = reqMove.StockLocator;
        }
    }
}
