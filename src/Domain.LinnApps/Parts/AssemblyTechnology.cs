namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public class AssemblyTechnology
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Part> Parts { get; set; }
    }
}