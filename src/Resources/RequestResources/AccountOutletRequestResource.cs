namespace Linn.Stores.Resources.RequestResources
{
    public class AccountOutletRequestResource : JobIdRequestResource
    {
        public int AccountId { get; set; }

        public int OutletNumber { get; set; }
    }
}
