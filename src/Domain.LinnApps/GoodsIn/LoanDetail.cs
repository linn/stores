﻿namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    public class LoanDetail
    {
        public int LoanNumber { get; set; }

        public int Line { get; set; }

        public string ArticleNumber { get; set; }

        public int QtyOnLoan { get; set; }

        public int? SerialNumber { get; set; }

        public int? SerialNumber2 { get; set; }

        public int ItemNumber { get; set; }
    }
}
