namespace Linn.Stores.Resources.Parts
{
    using Linn.Common.Resources;

    public class ImportBookTransportCodeResource : HypermediaResource
    {
        public int TransportId { get; set; }

        public string Description { get; set; }
    }
}
