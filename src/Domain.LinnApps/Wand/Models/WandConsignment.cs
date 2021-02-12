namespace Linn.Stores.Domain.LinnApps.Wand.Models
{
    public class WandConsignment
    {
        public int ConsignmentId { get; set; }

        public string Addressee { get; set; }

        public string IsDone { get; set; }

        public string CountryCode { get; set; }
    }
}
