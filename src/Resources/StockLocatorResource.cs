namespace Linn.Stores.Resources
{
    using System.Collections.Generic;

    public class StockLocatorResource
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }

        public string LocationName { get; set; }

        public string LocationDescription { get; set; }

        public int? BudgetId { get; set; }

        public string PartNumber { get; set; }

        public int? PartId { get; set; }

        public string PartDescription { get; set; }

        public string PartUnitOfMeasure { get; set; }

        public string UnitOfMeasure { get; set; }

        public int? QuantityAllocated { get; set; }

        public string StockPoolCode { get; set; }

        public string Remarks { get; set; }

        public string StockRotationDate { get; set; }

        public string BatchRef { get; set; }

        public string AuditDepartmentCode { get; set; }

        public string StoragePlaceName { get; set; }

        public string StoragePlaceDescription { get; set; }

        public IEnumerable<string> UserPrivileges { get; set; }

        public string State { get; set; }
    }
}
