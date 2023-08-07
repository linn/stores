namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class Department
    {
        public string DepartmentCode { get; set; }

        public string Description { get; set; }

        public DateTime? DateClosed { get; set; }

        public string ObseleteInStores { get; set; }

        public IEnumerable<NominalAccount> NominalAccounts { get; set; }
    }
}
