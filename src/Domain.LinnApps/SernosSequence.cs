namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public class SernosSequence
    {
        public string Sequence { get; set; }

        public string Description { get; set; }

        public IEnumerable<Part> Parts { get; set; }
    }
}
