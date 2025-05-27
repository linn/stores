namespace Linn.Stores.Domain.LinnApps.Scs
{
    using System;

    public class ScsStorePallet
    {
        public ScsStorePallet()
        {
            // for EF
        }

        public ScsStorePallet(ScsPallet pallet)
        {
            this.PalletNumber = pallet.PalletNumber;
            this.Allocated = pallet.Allocated ? "Y" : "N";
            this.Disabled = pallet.Disabled ? "Y" : "N";
            this.HeatValue = pallet.HeatValue;
            this.Height = pallet.Height;
            this.SizeCode = pallet.SizeCode();
            this.Area = pallet.Area;
            this.Column = pallet.Column;
            this.Level = pallet.Level;
            this.Side = pallet.Side;
            this.LastUpdated = DateTime.Now;
            this.RotationAverage = pallet.RotationAverage;
            this.UserFriendlyName = pallet.UserFriendlyName();
        }

        public int PalletNumber { get; set; }

        public string Allocated { get; set; }

        public string Disabled { get; set; }

        public int HeatValue { get; set; }

        public int Height { get; set; }

        public string SizeCode { get; set; }

        public decimal? RotationAverage { get; set; }

        public int Area { get; set; }

        public int Column { get; set; }

        public int Level { get; set; }

        public int Side { get; set; }

        public string UserFriendlyName { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
