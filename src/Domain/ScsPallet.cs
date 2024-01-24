namespace Linn.Stores.Domain.LinnApps.Scs
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

        public string SizeCode()
        {
            switch (this.Height)
            {
                case 2:
                    return "H";
                case 1:
                    return "M";
                default:
                    return "L";
            }
        }

        public string UserFriendlyName()
        {
            if (this.Area == 6)
            {
                // benches
                return $"B{this.Column}";
            }

            if (this.Area == 5)
            {
                // agvs
                return $"AGV{this.Column}";
            }

            var sideStr = this.Side == 0 ? "L" : "R";

            if (this.Area == 2)
            {
                if (this.Column == 0)
                {
                    if (this.Level == 5)
                    {
                        return this.Side == 0 ? "U1" : "U2";
                    }
                    else if (Level == 1)
                    {
                        return this.Side == 0 ? "B5" : "B6";
                    }
                }
                if (this.Column == 1)
                {
                    if (this.Level == 5)
                    {
                        return this.Side == 0 ? "SDA" : "SDB";
                    }
                    else if (Level == 1)
                    {
                        return this.Side == 0 ? "SAA" : "SAB";
                    }
                }
                return $"SA{this.Column.ToString().PadLeft(3, '0')}{this.Level.ToString().PadLeft(2, '0')}{sideStr}";
            }

            if (this.Area == 3)
            {
                if (this.Column == 0)
                {
                    if (this.Level == 5)
                    {
                        return this.Side == 0 ? "U3" : "U4";
                    }
                    else if (Level == 1)
                    {
                        return this.Side == 0 ? "B7" : "B8";
                    }
                }
                if (this.Column == 1)
                {
                    if (this.Level == 5)
                    {
                        return this.Side == 0 ? "SEA" : "SEB";
                    }
                    else if (Level == 1)
                    {
                        return this.Side == 0 ? "SBA" : "SBB";
                    }
                }
                return $"SB{this.Column.ToString().PadLeft(3, '0')}{this.Level.ToString().PadLeft(2, '0')}{sideStr}";
            }

            if (this.Area == 4)
            {
                if (this.Column == 0)
                {
                    if (this.Level == 5)
                    {
                        return this.Side == 0 ? "U5" : "U6";
                    }
                    else if (Level == 1)
                    {
                        return this.Side == 0 ? "B9" : "B10";
                    }
                }
                if (this.Column == 1)
                {
                    if (this.Level == 5)
                    {
                        return this.Side == 0 ? "SEA" : "SEB";
                    }
                    else if (Level == 1)
                    {
                        return this.Side == 0 ? "SCA" : "SCB";
                    }
                }
                return $"SC{this.Column.ToString().PadLeft(3, '0')}{this.Level.ToString().PadLeft(2, '0')}{sideStr}";
            }

            return string.Empty;
        }
    }
}
