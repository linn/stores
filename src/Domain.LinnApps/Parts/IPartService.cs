namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public interface IPartService
    {
        void UpdatePart(Part from, Part to, List<string> privileges, int who);

        Part CreatePart(Part partToCreate, List<string> privileges, bool fromTemplate);

        void AddOnQcControl(string partNumber, int? createdBy, string qcInfo);

        Part CreateFromSource(int sourceId, int createdBy, IEnumerable<PartDataSheet> dataSheets);

        IEnumerable<Part> GetDeptStockPalletParts();
    }
}
