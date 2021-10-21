namespace Linn.Stores.Resources.ImportBooks
{
    using System.Collections.Generic;

    public class PostDutyResource
    {
        public int CurrentUserNumber { get; set; }

        public string DatePosted { get; set; }

        public int ImpbookId { get; set; }

        public IEnumerable<ImportBookOrderDetailResource> OrderDetails { get; set; }

        public int SupplierId { get; set; }
    }
}
