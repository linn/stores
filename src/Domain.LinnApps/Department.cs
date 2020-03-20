namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class Department
    {
        public string DepartmentCode { get; set; }

        public string Description { get; set; }

        public DateTime? DateClosed { get; set; }
    }
}