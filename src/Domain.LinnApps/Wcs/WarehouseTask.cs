namespace Linn.Stores.Domain.LinnApps.Wcs
{
    using System;

    public class WarehouseTask 
    {
        public int TaskNo { get; set; }

        public string TaskSource { get; set; }

        public string Status { get; set; }

        public string TaskType { get; set; }

        public int PalletNumber { get; set; }

        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }

        public string OriginalLocation { get; set; }

        public string CurrentLocation { get; set; }

        public string Destination { get; set; }

        public string CollectiveRef { get; set; }

        public DateTime? TaskCreated { get; set; }

        public DateTime? TaskActivated { get; set; }

        public string Remark { get; set; }

        public int Priority { get; set; }

        public int JobNo { get; set; }
    }
}
