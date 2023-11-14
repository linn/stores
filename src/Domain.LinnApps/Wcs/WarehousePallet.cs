namespace Linn.Stores.Domain.LinnApps.Wcs
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Text;

    public class WarehousePallet
    {
        public int PalletId { get; set; }

        public string SizeCode { get; set; }

        public int SpeedFactor { get; set; }

        public decimal? RotationAverage { get; set; }

        public int? Heat { get; set; }

        public WarehouseLocation Location { get; set; }

        public int ScsHeight()
        {
            switch (this.SizeCode)
            {
                case "H":
                    return 2;
                case "L":
                    return 0;
                default:
                    return 1;
            }
        }

        public int ScsHeat()
        {
            // SpeedFactor is too unrelia
            switch (this.SpeedFactor)
            {
                case 1:
                case 2:
                    return 1; // High
                case 3:
                    return 2; // Medium
                default:
                    return 3; // Low
            }
        }
    }
}
