namespace Linn.Stores.Domain.LinnApps.Wcs
{
    using System;
    using System.Diagnostics;

    public class WarehouseLocation
    {
        public string Location { get; set; }

        public int? PalletId { get; set; }

        public WarehousePallet Pallet { get; set; }

        public string AreaCode { get; set; }

        public string Aisle { get; set; }

        public int? XCoord { get; set; }

        public int? YCoord { get; set; }

        public int ScsAreaCode()
        {
            switch (Location.Substring(0, 1))
            {
                case "A":
                    return 5; // AGVs
                case "B":
                    return 6; // Benches
                default:
                    break;
            }

            //             Area (1=System, 2=AisleA, 3=AisleB, 4=AisleC, 5=AGVs, 6=Benches
            switch (Location.Substring(0, 2))
            {
                case "CA":
                case "U1":
                case "U2":
                case "SA":
                    return 2; // AisleA
                case "CB":
                case "U3":
                case "U4":
                case "SB":
                    return 3; // AisleB
                case "CC":
                case "U5":
                case "U6":
                case "SC":
                    return 4; // AisleC
                default:
                    return 0; // Don't know
            }
        }

        public int ScsColumnIndex()
        {
            if (this.Location.Length == 8 && this.XCoord > 0)
            {
                return this.XCoord.Value;
            }

            if (this.Location.StartsWith('B'))
            {
                return Convert.ToInt32(this.Location.Substring(1));
            }

            if (this.Location.Length == 3)
            {
                switch (Location.Substring(0, 2))
                {
                    case "SA":
                    case "SB":
                    case "SC":
                        return 1;
                    default:
                        return 0;
                }
            }

            return 0;
        }

        public int ScsLevelIndex()
        {
            if (this.Location.Length == 8 && this.YCoord > 0)
            {
                return this.YCoord.Value;
            }

            if (this.Location.Length == 3)
            {
                switch (Location.Substring(0, 2))
                {
                    case "SA":
                    case "SB":
                    case "SC":
                        return 1;
                    default:
                        return 0;
                }
            }

            if (this.Location.StartsWith('U'))
            {
                return 5;
            }

            return 0;
        }

        public int ScsSideIndex()
        {
            if (this.Location.Length == 8 && this.Location.EndsWith("R"))
            {
                return 1;
            }

            if (this.Location.Length == 3 && this.Location.EndsWith("B"))
            {
                return 1;
            }

            if (this.Location.Length == 2 && this.Location.StartsWith("U"))
            {
                switch (this.Location)
                {
                    case "U2":
                    case "U4":
                    case "U6":
                        return 1;
                    default:
                        return 0;
                }
            }

            return 0;
        }
    }
}
