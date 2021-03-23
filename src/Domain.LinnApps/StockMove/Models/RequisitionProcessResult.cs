namespace Linn.Stores.Domain.LinnApps.StockMove.Models
{
    public class RequisitionProcessResult
    {
        public RequisitionProcessResult()
        {
        }

        public RequisitionProcessResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public int ReqNumber { get; set; }

        public string State { get; set; }
    }
}
