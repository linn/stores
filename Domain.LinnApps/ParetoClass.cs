namespace Domain.LinnApps
{
    using System.Collections.Generic;

    using Domain.LinnApps.Parts;

    public class ParetoClass
    {
        public string ParetoCode { get; set; }

        public string Description { get; set; }

        public List<Part> Parts { get; set; }
    }
}