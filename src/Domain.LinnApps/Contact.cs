namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    public class Contact
    {
        public int Id { get; set; }

        public int? OrgId { get; set; }

        public string EmailAddress { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int AddressId { get; set; }

        public IEnumerable<SalesAccount> SalesAccounts { get; set; }

        public IEnumerable<SalesOutlet> SalesOutlets { get; set; }
    }
}
