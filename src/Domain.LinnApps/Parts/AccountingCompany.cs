namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections;
    using System.Collections.Generic;

    public class AccountingCompany
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Part> PartsResponsibleFor { get; set; }
    }
}