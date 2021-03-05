namespace Linn.Stores.Resources.Tpk
{
    using System.Collections.Generic;

    public class TpkResultResource
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public IEnumerable<TransferredStockResource> Transferred { get; set; }
    }
}
