namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime? DateInvalid { get; set; }

        public IEnumerable<Part> PartsCreated { get; set; }

        public IEnumerable<Part> PartsMadeLive { get; set; }

        public IEnumerable<Part> PartsPhasedOut { get; set; }

        public IEnumerable<MechPartSource> SourcesProposed { get; set; }
    }
}
