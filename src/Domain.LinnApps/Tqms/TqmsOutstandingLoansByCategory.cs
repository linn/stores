namespace Linn.Stores.Domain.LinnApps.Tqms
{
    public class TqmsOutstandingLoansByCategory
    {
        public string JobRef { get; set; }

        public string Group { get; set; }
        
        public string Category { get; set; }
        
        public decimal TotalStoresValue { get; set; }

        public decimal TotalSalesValue { get; set; }
    }
}
