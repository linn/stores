namespace Linn.Stores.Domain.LinnApps.Requisitions.Models
{
    public class RequisitionActionResult
    {
        public RequisitionHeader RequisitionHeader { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
