namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Stores.Domain.LinnApps.Exceptions;

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

        public string GetRkmCode(string unit, decimal value)
        {
            var units = new Dictionary<string, decimal>
                            {
                                { "K", 1000m },
                                { "M", 1000000m },
                            };

            if (string.IsNullOrEmpty(unit))
            {
                return value.ToString("G");
            }

            if (value < 1)
            {
                return value.ToString("G") + unit;
            }

            var result = value / units[unit];
            return result % 1m == 0 ?
                       result.ToString("G") + unit
                       : result.ToString("G").Replace(".", unit);
        }

        public string GetCapacitanceLetterAndNumeralCode(string unit, decimal value)
        {
            var units = new Dictionary<string, decimal>
                            {
                                { "u", 0.000001m },
                                { "n", 0.000000001m },
                                { "p", 0.000000000001m },
                            };

            var result = (value / units[unit]).ToString("G29");

            if (result.Contains("."))
            {
                return result.Replace(".", unit) + "F";
            }

            return result + unit + "F";
        }

        public MechPartSource Create(MechPartSource candidate, IEnumerable<PartDataSheet> dataSheets)
        {
            if (candidate.SafetyCritical == "Y" && string.IsNullOrEmpty(candidate.SafetyDataDirectory))
            {
                throw new CreatePartException("You must enter a EMC/safety data directory for EMC or safety critical parts");
            }

            return candidate;
        }
    }
}
