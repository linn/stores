namespace Linn.Stores.Resources.ImportBooks
{
    using Linn.Common.Resources;

    public class ImportBookDeliveryTermResource : HypermediaResource
    {
        public string DeliveryTermCode { get; set; }

        public string Description { get; set; }

        public string Comments { get; set; }
    }
}
