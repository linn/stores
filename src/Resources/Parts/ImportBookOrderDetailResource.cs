namespace Linn.Stores.Resources.Parts
{
    using Linn.Common.Resources;

    public class ImportBookOrderDetailResource : HypermediaResource
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public int? OrderNumber { get; set; }

        public int? RsnNumber { get; set; }

        public string OrderDescription { get; set; }

        public int Qty { get; set; }

        public int DutyValue { get; set; }

        public int FreightValue { get; set; }

        public int VatValue { get; set; }

        public int OrderValue { get; set; }

        public int Weight { get; set; }

        public int? LoanNumber { get; set; }

        public string LineType { get; set; }

        public int? CpcNumber { get; set; }

        public string TariffCode { get; set; }

        public int? InsNumber { get; set; }

        public int? VatRate { get; set; }
    }
}