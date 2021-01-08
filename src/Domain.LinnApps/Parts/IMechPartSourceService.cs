namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;

    public interface IMechPartSourceService
    {
        IEnumerable<PartDataSheet> GetUpdatedDataSheets(IEnumerable<PartDataSheet> from, IEnumerable<PartDataSheet> to);

        string GetRkmCode(string unit, decimal value);

        string GetCapacitanceLetterAndNumeralCode(string unit, decimal value);
    }
}
