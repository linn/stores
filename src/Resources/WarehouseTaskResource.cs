namespace Linn.Stores.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WarehouseTaskResource
    {
        public int TaskNo { get; set; }

        public string TaskSource { get; set; }

        public string Status { get; set; }

        public string TaskType { get; set; }

        public int PalletNumber { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string OriginalLocation { get; set; }

        public string CurrentLocation { get; set; }

        public string Destination { get; set; }

        public string CollectiveRef { get; set; }

        public string TaskCreated { get; set; }

        public string TaskActivated { get; set; }

        public string Remark { get; set; }

        public int Priority { get; set; }

        public int ErrorCode { get; set; }
    }
}
