namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public class Supplier
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public IEnumerable<Part> PartsPreferredSupplierOf { get; set; }

        public string CountryCode { get; set; }
    }
}