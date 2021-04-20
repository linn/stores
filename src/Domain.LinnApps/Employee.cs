namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime? DateInvalid { get; set; }

        public IEnumerable<Part> PartsCreated { get; set; }

        public IEnumerable<Part> PartsMadeLive { get; set; }

        public IEnumerable<Part> PartsPhasedOut { get; set; }

        public IEnumerable<MechPartSource> SourcesProposed { get; set; }

        public IEnumerable<MechPartManufacturerAlt> MechPartManufacturerAltsApproved { get; set; }

        public IEnumerable<MechPartSource> PartsCreatedSourceRecords { get; set; }

        public IEnumerable<MechPartSource> SourcesVerified { get; set; }

        public IEnumerable<MechPartSource> SourcesVerifiedMcit { get; set; }

        public IEnumerable<MechPartSource> SourcesQualityVerified { get; set; }

        public IEnumerable<MechPartSource> SourcesTCodeApplied { get; set; }

        public IEnumerable<MechPartSource> SourcesTCodeRemoved { get; set; }

        public IEnumerable<MechPartSource> SourcesCancelled { get; set; }

        public IEnumerable<ExportReturn> ExportReturnsCreated { get; set; }
    }
}
