namespace Linn.Stores.Resources
{
    public class StoresBudgetPostingResource
    {
        public int BudgetId { get; set; }

        public int Sequence { get; set; }

        public decimal Quantity { get; set; }

        public string DebitOrCredit { get; set; }

        public NominalAccountResource NominalAccount { get; set; }

        public string Product { get; set; }
        
        public string Building { get; set; }
        
        public string Vehicle { get; set; }

        public int? Person { get; set; }
    }
}
