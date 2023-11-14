namespace Linn.Stores.Resources.Scs
{
    public class ScsPalletResource
    {
        public int PalletNumber { get; set; }

        public bool Allocated { get; set; }
        public bool Disabled { get; set; }

        public LocationResource CurrentLocation { get; set; }

        public int HeatValue { get; set; }

        public int Height { get; set; }

        public decimal RotationAverage { get; set; }

        public int RejectionCode { get; set; }
    }
}
