namespace Linn.Stores.Domain.LinnApps.Wand
{
    using System;

    public class WandLog
    {
        public int Id { get; set; }

        public string TransType { get; set; }

        public DateTime DateWanded { get; set; }

        public int EmployeeNumber { get; set; }

        public string WandString { get; set; }

        public string ArticleNumber { get; set; }

        public decimal QtyWanded { get; set; }

        public int? SeriaNumber1 { get; set; }

        public int? SeriaNumber2 { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public int ConsignmentId { get; set; }

        public int? ItemNo { get; set; }

        public int? ContainerNo { get; set; }
    }
}
