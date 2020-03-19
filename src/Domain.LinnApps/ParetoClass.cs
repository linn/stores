namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Domain.LinnApps.Parts;

    public class ParetoClass
    {
        public string ParetoCode { get; set; }

        public string Description { get; set; }

        public IEnumerable<Part> Parts { get; set; }
    }
}