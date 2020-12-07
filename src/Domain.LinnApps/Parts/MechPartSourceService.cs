namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;
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
            throw new NotImplementedException();
        }
    }
}
