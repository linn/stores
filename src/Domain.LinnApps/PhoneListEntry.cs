﻿namespace Linn.Stores.Domain.LinnApps
{
    public class PhoneListEntry
    {
        public int Id { get; set; }

        public int UserNumber { get; set; }

        public string EmailAddress { get; set; }

        public AuthUser User { get; set; }
    }
}
