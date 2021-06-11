namespace Linn.Stores.Resources.Parts
{
    using Linn.Common.Resources;

    public class ImportBookTransactionCodeResource : HypermediaResource
    {
        public int TransactionId { get; set; }

        public string Description { get; set; }
    }
}
