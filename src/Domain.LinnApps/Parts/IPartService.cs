﻿namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public interface IPartService
    {
        void UpdatePart(Part from, Part to, List<string> privileges, IEnumerable<MechPartManufacturerAlt> manufacturers);

        Part CreatePart(Part partToCreate, List<string> privileges, bool fromTemplate);

        void AddQcControl(string partNumber, int? createdBy, string qcInfo);

        Part CreateFromSource(int sourceId, int createdBy, IEnumerable<PartDataSheet> dataSheets);

        IEnumerable<Part> GetDeptStockPalletParts();
    }
}
