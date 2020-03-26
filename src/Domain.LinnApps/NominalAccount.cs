namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public class NominalAccount
    {
        public int NominalAccountId { get; set; }

        public string Department { get; set; }

        public Nominal Nominal { get; set; }


        public List<Part> Parts { get; set; }
    }
}