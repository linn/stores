namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public class DecrementRule
    {
        public string Rule { get; set; }

        public string Description { get; set; }

        public IEnumerable<Part> Parts { get; set; }
    }
}
