namespace Linn.Stores.Resources
{
    using System.Collections.Generic;

    public class ConsignmentShipfileResource
    {
        public int ConsignmentId { get; set; }

        public string DateClosed { get; set; }

        public string CustomerName { get; set; }

        public IEnumerable<int> InvoiceNumbers { get; set; }

        public string Status { get; set; }
    }
}
