namespace Linn.Stores.Resources.RequestResources
{
    public class SalesOutletRequestResource : SearchRequestResource
    {
        public int[] OrderNumbers { get; set; }

        public int? accountId { get; set; }
    }
}
