namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    public class Person
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<Contact> ContactDetails { get; set; }
    }
}
