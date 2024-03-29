﻿namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public class NominalAccount
    {
        public int NominalAccountId { get; set; }

        public Department Department { get; set; }

        public Nominal Nominal { get; set; }

        public string StoresPostsAllowed { get; set; }

        public IEnumerable<Part> Parts { get; set; }
    }
}
