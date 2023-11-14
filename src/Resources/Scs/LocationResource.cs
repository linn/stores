namespace Linn.Stores.Resources.Scs
{

    public class LocationResource
    {
        public bool Allocated { get; set; }

        public bool CheckedOut { get; set; }

        public bool CheckOutRequested { get; set; }

        public int Area { get; set; }

        public int Column { get; set; }

        public int Level { get; set; }

        public int Side { get; set; }

        public bool Enabled { get; set; }

        public bool Exists { get; set; }

        public int Heat { get; set; }

        public int Height { get; set; }

        public bool Occupied { get; set; }

        public int PalletNumber { get; set; }

        public string UserFriendlyName { get; set; }
    }
}
