namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;
    using System.Linq;

    public class MechPartSourceService : IMechPartSourceService
    {
        public IEnumerable<PartDataSheet> GetUpdatedDataSheets(IEnumerable<PartDataSheet> from, IEnumerable<PartDataSheet> to)
        {
            var updated = to.ToList();
            var old = from.ToList();

            foreach (var partDataSheet in updated.Where(n => old.All(o => o.Sequence != n.Sequence)))
            {
                partDataSheet.Sequence = updated.Max(s => s.Sequence) + 1;
            }

            return updated;
        }

        public string CalculateResistanceChar(string unit, decimal value)
        {
            var units = new Dictionary<string, decimal>
                            {
                                { "K", 1000m },
                                { "M", 1000000m },
                            };

            if (string.IsNullOrEmpty(unit))
            {
                return value.ToString("G29");
            }

            if (value < 1)
            {
                return value.ToString("G29") + unit;
            }

            var result = value / units[unit];
            return result % 1m == 0 ? 
                       result.ToString("G29") + unit 
                       : result.ToString("G29").Replace(".", unit);
        }
    }
}
