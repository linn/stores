namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public interface IPartService
    {
        void UpdatePart(Part from, Part to, List<string> privileges);

        Part CreatePart(Part partToCreate, List<string> privileges);

        void AddQcControl(string partNumber, int? createdBy, string qcInfo);

        Part CreateFromSource(int sourceId, int createdBy);

        IEnumerable<Part> GetDeptStockPalletParts();
    }
}
