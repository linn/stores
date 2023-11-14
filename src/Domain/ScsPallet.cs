namespace Linn.Stores.Domain
{
    public class ScsPallet : StoresAddress
    {
        public int PalletNumber { get; set; }

        public bool Allocated { get; set; }

        public bool Disabled { get; set; }

        public int HeatValue { get; set; }

        public int Height { get; set; }

        // need to run PL/SQL function CALC_PALLET_ROTATIONS to refresh this
        public decimal? RotationAverage { get; set; }
    }
}
